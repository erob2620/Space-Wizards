using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spacewizards.Items
{
    class LockedDoor : TileSprite
    {
        public Game1 gameRef { get; set; }
        public Mapper mapper { get; set; }
        public LockedDoor(Texture2D textureImage, Vector2 position, int collisionOffset, Mapper mapper, Game1 gameRef)
            : base(textureImage, position, new Point(32, 32), 0, new Point(0, 0), new Point(0, 0), Vector2.Zero)
        {
            this.isInteractable = true;
            this.passable = false;
            this.gameRef = gameRef;
            this.mapper = mapper;
            this.zIndex = .1f;
        }

        public override void Interact()
        {
            if (gameRef.Character.keys >= 1)
            {
                gameRef.Character.keys--;
                mapper.activeMap.loadList.Remove(this);
            }
            else
            {
                //throw a YOU DON'T HAVE ANY GODDAMN KEYS, YA FUCKIN' SCRUB
            }
        }
    }
}
