using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using spacewizards.Controls;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewizards.GameScreens
{
    public class StartMenuScreen : BaseGameState
    {
        private PictureBox background;
        public PictureBox Background
        {
            get { return background; }
            set { background = value; }
        }
        private PictureBox arrow;
        public PictureBox Arrow
        {
            get { return arrow; }
            set { arrow = value; }
        }
        private LinkLabel start;
        public LinkLabel Start
        {
            get { return start; }
            set { start = value; }
        }
        private LinkLabel load;
        public LinkLabel Load
        {
            get { return load; }
            set { load = value; }
        }
        private LinkLabel exit;
        public LinkLabel Exit
        {
            get { return exit; }
            set { exit = value; }
        }
        private float maxItemWidth = 0f;
        public float MaxItemWidth
        {
            get { return maxItemWidth; }
            set { maxItemWidth = value; }
        }
        public StartMenuScreen(Game game, GameStateManeger manager) : base(game, manager)
        {

        }
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            ContentManager Content = Game.Content;

            Background = new PictureBox(
                Content.Load<Texture2D>(@"Backgrounds\spacewizardstitle"),
                GameRef.ScreenRectangle);
            ControlManager.Add(Background);

            Texture2D arrowTexture = Content.Load<Texture2D>(@"GUI\leftarrowUp");

            arrow = new PictureBox(
                arrowTexture,
                new Rectangle(0, 0, arrowTexture.Width, arrowTexture.Height));
            ControlManager.Add(arrow);

            start = new LinkLabel();
            start.Text = "Start Game";
            start.Size = start.SpriteFont.MeasureString(start.Text);
            start.Selected += new EventHandler(menu_Selected);
            ControlManager.Add(start);

            load = new LinkLabel();
            load.Text = "Load Game";
            load.Size = load.SpriteFont.MeasureString(load.Text);
            load.Selected += menu_Selected;

            exit = new LinkLabel();
            exit.Text = "Quit Game";
            exit.Size = exit.SpriteFont.MeasureString(exit.Text);
            exit.Selected += menu_Selected;
            ControlManager.Add(exit);

            ControlManager.NextControl();

            ControlManager.FocusedChanged += new EventHandler(ControlManager_FocusedChanged);
            Vector2 position = new Vector2(375, 500);

            foreach (Control c in ControlManager)
            {
                if (c is LinkLabel)
                {
                    if (c.Size.X > maxItemWidth)
                        maxItemWidth = c.Size.X;

                    c.Position = position;
                    position.Y += c.Size.Y + 5f;


                }
            }
            ControlManager_FocusedChanged(start, null);

        }

        public void ControlManager_FocusedChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Vector2 position = new Vector2(control.Position.X + maxItemWidth + 10f,
                control.Position.Y + 10f);
            arrow.SetPosition(position);
        }

        public void menu_Selected(object sender, EventArgs e)
        {
            if (sender == start)
            {
                GameRef.StartGame();
            }
            if (sender == exit)
            {
                Game.Exit();
            }
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ControlManager.Update(gameTime, playerIndexInControl);
            base.Update(gameTime);
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            base.Draw(gameTime);
            ControlManager.Draw(GameRef.spriteBatch);
            GameRef.spriteBatch.End();

        }
       
       
    }

}
