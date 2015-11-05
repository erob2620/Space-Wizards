using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using spacewizards.Controls;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewizards.GameScreens
{
    class DialogState : BaseGameState
    {

        SpriteFont font;
        Vector2 pos;
        private Keys KeyToPress { get; set; }
        private string inputText { get; set; }
        private bool isLeveled { get; set; }


        public DialogState(Game1 game, GameStateManeger manager, String text, bool leveled)
            : base(game, manager)
        {
            inputText = text + "\n" + "\n" + "\n" + "\n" + "Press Space to Continue";
            isLeveled = leveled;
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            ContentManager Content = Game.Content;
            font = Content.Load<SpriteFont>(@"Fonts/ControlFont");
            pos = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        }
        public override void Update(GameTime gameTime)
        {
            if (InputHandler.KeyPressed(Keys.Space) || InputHandler.KeyPressed(Keys.Enter))
            {
                GameRef.ContinueGame(isLeveled);
            }
            base.Update(gameTime);
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GameRef.spriteBatch.Begin();
            Vector2 FontOrigin = font.MeasureString(inputText) / 2;
            GameRef.spriteBatch.DrawString(font, inputText, pos, Color.LightGreen, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);



            GameRef.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
