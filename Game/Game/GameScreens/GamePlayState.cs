using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using spacewizards.Models;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewizards.GameScreens
{
    public class GamePlayState : BaseGameState
    {
        SpriteManager SpriteManager;
        PlayerCharacter Character;

        public bool textBox = false;
        private string text;
        private SpriteFont font;
        private Vector2 textPos = new Vector2(500, 500);
        private Texture2D textSprite;
        private string EnemyMoves = "";
        public Mapper mapper { get; set; }

        public GamePlayState(Game1 game, GameStateManeger manager, PlayerCharacter Character)
            : base(game, manager)
        {
            SpriteManager = new SpriteManager(game, Character);
            Mapper mapper = new Mapper(SpriteManager, game);
            SpriteManager.player.mapper = mapper;
            Random r = new Random();
            Map m = null;
            mapper.maps.TryGetValue("01", out m);
            mapper.LoadMap(m);

        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            SpriteManager.Draw(gameTime);
            base.Draw(gameTime);
            if (textBox)
            {
                Vector2 FontOrigin = font.MeasureString(text) / 2;
                GameRef.spriteBatch.Draw(Game.Content.Load<Texture2D>(@"Images/textBox"), new Vector2(textPos.X - 400, textPos.Y - 75),
                new Rectangle(0,
                400, 800, 200),
                Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                GameRef.spriteBatch.DrawString(font, text, textPos, Color.Black, 0, FontOrigin, 1.0f, SpriteEffects.None, 1f);
            }

            GameRef.spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            if (!textBox)
            {
                SpriteManager.Update(gameTime);
                base.UnloadContent();
            }
            if (InputHandler.KeyPressed(Keys.X))
            {
                textBox = false;
            }
        }
        protected override void LoadContent()
        {
            SpriteManager.LoadContent(SpriteManager.spriteList, SpriteManager.player.position);
            base.LoadContent();

            textSprite = Game.Content.Load<Texture2D>(@"Images/textBox");
            font = Game.Content.Load<SpriteFont>(@"Fonts/ControlFont");

            showDialog("Find the 4 Keys, and Defeat the final Boss\n            Press X to Continue.");

        }
        public void showDialog(string text)
        {
            textBox = true;
            this.text = text;
        }
    }
}
