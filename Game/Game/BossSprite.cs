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
    class BossSprite : AutomatedSprite
    {
        public Enemy Boss { get; set; }
        public BossSprite(Texture2D textureImage, Vector2 position,
           Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
           Vector2 speed, Game1 game, Enemy boss)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, game)
        {
            this.game = game;
            this.zIndex = .2f;
            passable = false;
            this.isInteractable = true;
            this.interactOnTouch = true;
            this.Boss = boss;
        }

        public override void Interact()
        {
            this.isInteractable = false;
            this.passable = true;
            this.position = new Vector2(-100, -100);
            List<Enemy> enemies = new List<Enemy>();
            enemies.Add(Boss);

            game.StartCombat(enemies);
        }
    }
}
