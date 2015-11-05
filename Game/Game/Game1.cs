using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using spacewizards.GameScreens;
using spacewizards.Models;
using System.Text;
using spacewizards.Models.Monsters;

namespace spacewizards
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static Random r = new Random();
        public PlayerCharacter Character;
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public CombatState Combat;
        public GameOverScreen gameOver;
        public GameStateManeger stateManeger;
        public WorldMapMenuState inventory;
        public StartMenuScreen StartMenuScreen;
        public GamePlayState gamePlay;
        public CharacterStats statScreen;
        private int screenWidth = 1000;
        private int screenHeight = 1000;
        public Rectangle ScreenRectangle;
        Song song;
        public Mapper mapper { get; set; }
        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            ScreenRectangle = new Rectangle(
                0,
                0,
                screenWidth,
                screenHeight);
            
            Content.RootDirectory = "Content";
            Components.Add(new InputHandler(this));
            stateManeger = new GameStateManeger(this);
            Components.Add(stateManeger);


            StartMenuScreen = new GameScreens.StartMenuScreen(this, stateManeger);

            stateManeger.ChangeState(StartMenuScreen);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


            song = Content.Load<Song>("Music/Vogel im Kafig");


            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            base.Draw(gameTime);
        }

        public void ShowStats(bool fromCombat)
        {
            statScreen = new CharacterStats(this, stateManeger, Character, fromCombat);
            stateManeger.ChangeState(statScreen);
        }

        public void StartCombat(List<Enemy> enemies)
        {
            
            CombatState Combat = new CombatState(this, stateManeger, Character, enemies);
            stateManeger.ChangeState(Combat);
        }

        public void StartCombat()
        {
            List<Enemy> enemies;

                int numOfEnemies = r.Next(1, 3);
                if (numOfEnemies == 1)
                {
                    enemies = new List<Enemy>
                    {
                        new Models.Monsters.ToxWyrm(Character.Level, this, Character)
                    };
                }
                else
                {
                    enemies = new List<Enemy>
                    {
                    new Models.Monsters.ToxWyrm(Character.Level , this, Character),
                    new Models.Monsters.VioletWing(Character.Level , this, Character)
                    };
                }
            
           
            Combat = new CombatState(this,stateManeger, Character, enemies);
            stateManeger.ChangeState(Combat);
        }
        public void StartGame()
        {
            Character = new PlayerCharacter();
            gamePlay = new GamePlayState(this, stateManeger, Character);
            stateManeger.ChangeState(gamePlay);
        }
        public void ShowInventory(bool fromCombat)
        {
            inventory = new WorldMapMenuState(this, stateManeger, Character, fromCombat);
            stateManeger.ChangeState(inventory);
        }
        public void ContinueGame(bool isLeveled)
        {
            stateManeger.ChangeState(gamePlay);
            if (isLeveled)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Leveled Up!");
                sb.Append("\nHP " + (Character.HP - (Character.HP / 6)) + " + " + Character.HP / 6);
                sb.Append("\nAttack " + (Character.Atk - (Character.Atk / 6)) + " + " + Character.Atk / 6);
                sb.Append("\nDefense " + (Character.Defense - (Character.Defense / 6)) + " + " + Character.Defense / 6);
                sb.Append("\nSpeed " + (Character.Speed - (Character.Speed / 6)) + " + " + Character.Speed / 6);
                // current divided by 6 for gained
                pushDialog(sb.ToString(), false);
            }
        }
        public void ContinueCombat()
        {
            Combat.EnemyTurn();
            stateManeger.ChangeState(Combat);
        }
        public void GameOver()
        {
            gameOver = new GameOverScreen(this, stateManeger);
            stateManeger.ChangeState(gameOver);
        }
        public void pushDialog(string text, bool isLeveled)
        {
            stateManeger.PushState(new DialogState(this, stateManeger, text, isLeveled));

        }
        public void ShowGainedItems(string text, bool isLeveled)
        {
            stateManeger.ChangeState(new DialogState(this, stateManeger, text, isLeveled));
        }
        public void Win()
        {
            stateManeger.ChangeState(new WinScreen(this, stateManeger));
        }
        public void openShop()
        {
            stateManeger.ChangeState(new ShopState(this, stateManeger, Character));
        }
    }

}