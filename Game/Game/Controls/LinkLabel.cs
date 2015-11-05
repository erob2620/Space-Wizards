using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewizards.Controls
{
    public class LinkLabel : Control
    {
        private Color selectedColor = Color.Blue;

        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }

        public LinkLabel()
        {
            TabStop = true;
            HasFocus = false;
            Position = Vector2.Zero;
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (hasFocus)
                spriteBatch.DrawString(SpriteFont, Text, Position, selectedColor);
            else
                spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }
        public override void HandleInput(PlayerIndex playerIndex)
        {
            if (!HasFocus)
                return;

            if(InputHandler.KeyPressed(Keys.Space) || InputHandler.KeyPressed(Keys.Enter))
                base.OnSelected(null);
            if (InputHandler.KeyPressed(Keys.Back))
                base.OnClear(null);
        }
    }
}
