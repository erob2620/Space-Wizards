using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spacewizards
{
    class Door : AutomatedSprite
    {
        public string mapLink;
        public Vector2 placementOnMove = new Vector2(0, 0);
        Mapper mapper;


        public Door(Texture2D textureImage, Vector2 position, int collisionOffset, string mapLink, Mapper mapper, Vector2 PlacementOnMove, Game1 game)
            : base(textureImage, position, new Point(32, 32), -25, new Point(0, 0), new Point(0, 0), Vector2.Zero, game)
        {
            this.mapLink = mapLink;
            this.zIndex = .5f;
            this.mapper = mapper;
            this.passable = false;
            this.isInteractable = true;
            this.placementOnMove = PlacementOnMove;
        }

        public override void Interact()
        {
            mapper.doorLoader(mapLink, placementOnMove);
        }
    }
}
