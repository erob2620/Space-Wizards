using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace spacewizards.GameScreens
{
    public class WinScreen : BaseGameState
    {
        public LinkLabel StartOver { get; private set; }
        public LinkLabel Quit { get; private set; }

        public PictureBox ArrowTexture { get; private set; }
        public PictureBox Background { get; private set; }

        public WinScreen(Game1 game, GameStateManeger manager)
            : base(game, manager)
        {

        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            base.Draw(gameTime);
            ControlManager.Draw(GameRef.spriteBatch);
            GameRef.spriteBatch.End();
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ControlManager.Update(gameTime, playerIndexInControl);
            base.Update(gameTime);
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            ContentManager Content = Game.Content;

            Background = new PictureBox(
                Content.Load<Texture2D>(@"Backgrounds\WinScreen"),
                GameRef.ScreenRectangle);
            ControlManager.Add(Background);

            Texture2D arrow = Content.Load<Texture2D>(@"GUI\leftarrowUp");

            ArrowTexture = new PictureBox(
                arrow,
                new Rectangle(0, 0, arrow.Width, arrow.Height));
            ControlManager.Add(ArrowTexture);

            StartOver = new LinkLabel();
            StartOver.Text = "Play Again";
            StartOver.Size = StartOver.SpriteFont.MeasureString(StartOver.Text);
            StartOver.Selected += new EventHandler(StartOver_Selected);
            ControlManager.Add(StartOver);

            Quit = new LinkLabel();
            Quit.Text = "Quit the game";
            Quit.Size = Quit.SpriteFont.MeasureString(Quit.Text);
            Quit.Selected += new EventHandler(Quit_Selected);
            ControlManager.Add(Quit);

            ControlManager.NextControl();
            ControlManager.FocusedChanged += new EventHandler(ControlManager_FocusedChanged);

            Vector2 position = new Vector2(((GameRef.ScreenRectangle.Width / 2) - StartOver.Size.X),
                 GameRef.ScreenRectangle.Height / 2);

            foreach (Control c in ControlManager)
            {
                if (c is LinkLabel)
                {
                    c.Position = position;
                    position.Y += c.Size.Y + 5f;
                }
            }

            ControlManager_FocusedChanged(StartOver, null);
        }
        private void ControlManager_FocusedChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Vector2 position = new Vector2(control.Position.X + control.Size.X + 5f,
                control.Position.Y + 10f);
            ArrowTexture.SetPosition(position);
        }
        private void Quit_Selected(object sender, EventArgs e)
        {
            GameRef.Exit();
        }
        public void StartOver_Selected(object sender, EventArgs e)
        {
            GameRef.StartGame();
        }
    }
}
