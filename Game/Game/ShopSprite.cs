using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spacewizards.Models;
using spacewizards.GameScreens;

namespace spacewizards
{
    class ShopSprite : TileSprite
    {
        public Game1 gameRef { get; set; }
        public Mapper mapper { get; set; }
        public ShopSprite(Texture2D textureImage, Vector2 position, int collisionOffset, Mapper mapper, Game1 gameRef)
            : base(textureImage, position, new Point(32, 32), 0, new Point(0, 0), new Point(0, 0), Vector2.Zero)
        {
            this.gameRef = gameRef;
            this.zIndex = .2f;
            passable = false;
            this.isInteractable = true;
            this.interactOnTouch = false;
        }
        public override void Interact()
        {
            this.isInteractable = false;
            gameRef.openShop();

        }
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            if (InputHandler.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                this.isInteractable = true;
            }
            base.Update(gameTime, clientBounds);
        }
    }
}
