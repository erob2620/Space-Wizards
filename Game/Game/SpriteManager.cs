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
using spacewizards.Models;


namespace spacewizards
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    /// 
    [Serializable]
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent //BaseGameState
    {

        SpriteBatch spriteBatch;
        Game1 GameRef;
        public static Random rand = new Random();
        public UserControlledSprite player;
        public PlayerCharacter pc;
        public List<Sprite> spriteList = new List<Sprite>();
        public Mapper mapper { get; set; }

        public SpriteManager(Game game, PlayerCharacter userCharacter)

            : base(game)
        {
            GameRef = (Game1)game;
            pc = userCharacter;
            // TODO: Construct any child components here
            player = new UserControlledSprite(Game.Content.Load<Texture2D>(@"Images/player"),
            new Vector2(64, 64), new Point(27, 31), 0, new Point(0, 0),
            new Point(0, 0), new Vector2(3, 3), pc, GameRef, mapper, 2f);

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
        }
        public void LoadContent(List<Sprite> loadList, Vector2 playerPosition)
        {
            player = new UserControlledSprite(Game.Content.Load<Texture2D>(@"Images/player"),
                playerPosition, new Point(27, 31), 0, new Point(0, 0),
                new Point(0, 0), new Vector2(3, 3), pc, GameRef, mapper, .1f);
            spriteList = loadList;
            LoadContent();
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            Vector2 previous = player.position;
            //update player
            player.Update(gameTime, Game.Window.ClientBounds);

            //update all sprites
            for (int i = 0; i < spriteList.Count; i++)
            {
                Sprite s = spriteList[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.collisionRect.Intersects(player.collisionRect) && s.interactOnTouch)
                {
                    s.Interact();
                }
                if (s.collisionRect.Intersects(player.collisionRect) && !s.passable)
                {
                    player.position = previous;
                    pc.DecrementEnounter();
                }
                if (s.collisionRect.Intersects(player.interactRect) && s.isInteractable)
                {
                    s.Interact();
                }

            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            //draw the player
            player.Draw(gameTime, spriteBatch);


            //draw all sprites
            foreach (Sprite s in spriteList)
            {
                s.Draw(gameTime, spriteBatch);
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
