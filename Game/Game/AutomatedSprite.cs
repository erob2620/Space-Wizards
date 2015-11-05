using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spacewizards
{
    class AutomatedSprite : Sprite
    {
        public Game1 game { get; set; }

        public AutomatedSprite(Texture2D textureImage, Vector2 position,
           Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
           Vector2 speed, Game1 game)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed)
        {
            this.game = game;
            this.zIndex = .2f;
            this.isInteractable = true;
        }

        public AutomatedSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, Game1 game, int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame)
        {
            this.game = game;
            this.zIndex = .2f;
            this.isInteractable = true;
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

        public override void Interact()
        {
            this.isInteractable = false;
            this.passable = true;
            this.position = new Vector2(-100, -100);
        }

        internal void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
