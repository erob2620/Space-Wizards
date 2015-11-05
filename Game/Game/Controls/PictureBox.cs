using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace spacewizards.Controls
{
    public class PictureBox : Control
    {
        private Texture2D image;
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }
        private Rectangle sourceRectangle;
        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
            set
            {
                sourceRectangle = value;
            }
        }
        private Rectangle destRectangle;
        public Rectangle DestRectangle
        {
            get { return destRectangle; }
            set
            {
                destRectangle = value;
            }
        }

        public PictureBox(Texture2D image, Rectangle rectangle)
        {
            Image = image;
            DestRectangle = rectangle;

            SourceRectangle = new Rectangle(0, 0, Image.Width, Image.Height);
            Color = Color.White;
        }
        public PictureBox(Texture2D image, Rectangle dest, Rectangle source)
        {
            Image = image;
            DestRectangle = dest;
            SourceRectangle = source;
            Color = Color.White;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, DestRectangle, sourceRectangle, Color);
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void HandleInput(PlayerIndex playerIndex)
        {
        }
        public void SetPosition(Vector2 position)
        {
            destRectangle = new Rectangle(
                (int)position.X,
                (int)position.Y,
                sourceRectangle.Width,
                sourceRectangle.Height);
        }
    }
}
