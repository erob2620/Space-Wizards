﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacewizards.Controls
{
    public class Label : Control
    {
        public Label()
        {
            tabStop = false;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, text, Position, Color);
        }
        public override void HandleInput(Microsoft.Xna.Framework.PlayerIndex playerIndex)
        {
        }
    }
}
