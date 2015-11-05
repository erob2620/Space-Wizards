using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using spacewizards.Controls;

namespace spacewizards.GameScreens
{
    public class TitleScreen : BaseGameState
    {
        public Texture2D backgroundImage;

        public TitleScreen(Game1 game, GameStateManeger manager) : base(game, manager)
        {
        }
        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;
            backgroundImage = Content.Load<Texture2D>(@"Backgrounds/SpaceWizard");
            base.LoadContent();

            LinkLabel startLabel = new LinkLabel();
            startLabel.Position = new Vector2(500, 500);
            startLabel.Text = "Press ENTER to begin";
            startLabel.Color = Color.White;
            startLabel.TabStop = true;
            startLabel.HasFocus = true;
            startLabel.Selected += new EventHandler(startLabel_Selected);

            LinkLabel quitLabel = new LinkLabel();
            quitLabel.Position = new Vector2(0, 25);
            quitLabel.Text = "Press ESC to quit";
            quitLabel.Color = Color.White;
            quitLabel.TabStop = false;
            quitLabel.HasFocus = false;
            quitLabel.Selected += new EventHandler(quitLabel_Selected);

            ControlManager.Add(startLabel);
        }

        void quitLabel_Selected(object sender, EventArgs e)
        {
            Game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            
            base.Draw(gameTime);

            GameRef.spriteBatch.Draw(
                backgroundImage,
                GameRef.ScreenRectangle,
                Color.White);
            ControlManager.Draw(GameRef.spriteBatch);

            GameRef.spriteBatch.End();
        }
        private void startLabel_Selected(object sender, EventArgs e)
        {
            StateManeger.ChangeState(GameRef.StartMenuScreen);
        }
    }
}
