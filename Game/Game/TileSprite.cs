using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace spacewizards
{
    public class TileSprite : Sprite
    {
        public TileSprite(Texture2D textureImage, Vector2 position,
           Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
           Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, 2f)
        {
            this.passable = true;
            this.isInteractable = false;
        }

        public TileSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, float zIndex = .99f)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame)
        {

        }

        public override Vector2 direction
        {
            get { return speed; }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += direction;

            base.Update(gameTime, clientBounds);
        }
    }
}
