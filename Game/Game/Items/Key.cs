using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spacewizards.Items
{
    class Key : AutomatedSprite
    {
        public Mapper mapper { get; set; }
        public Key(Texture2D textureImage, Vector2 position, int collisionOffset, Mapper mapper, Game1 game)
            : base(textureImage, position, new Point(32, 32), 0, new Point(0, 0), new Point(0, 0), Vector2.Zero, game)
        {
            this.zIndex = .5f;
            this.isInteractable = false;
            this.interactOnTouch = true;
            this.passable = true;
            this.mapper = mapper;
        }

        public override void Interact()
        {
            game.Character.keys++;

            mapper.activeMap.loadList.Remove(this);
        }
    }
}
