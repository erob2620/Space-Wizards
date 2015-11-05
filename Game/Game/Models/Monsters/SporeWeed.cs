using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Models;
using spacewizards.Items;
using Microsoft.Xna.Framework.Graphics;
namespace spacewizards.Models.Monsters
{
    class SporeWeed : Enemy
    {
        public SporeWeed(int characterLV, Game1 _game, PlayerCharacter pc)
            : base(characterLV, _game)
        {

            image = Content.Load<Texture2D>(@"Images/ThirdEnemy");
            this.SP_ATK_MOD = .60f;
            this.SP_DEF_MOD = .85f;
            this.SPD_MOD = 1.1f;
            this.DEF_MOD = .75f;
            this.ATK_MOD = .7f;
            this.HP_MOD = .8f;
            this.specChance = .3f;
            this.goldAmt = this.level * 12;
            this.name = "Spore Weed";
            this.CurrentHP = (int)(hp * HP_MOD);
            Drops.Add(new HealthPot("Health Potion", pc, "+35% HP\n\nA small, red potion. As generic as they come.",15));
        }

        public override int GetAttack(out bool isSpecial)
        {
            isSpecial = true;
            int attack = (int)(specatk * specAtKMod);
            int special = r.Next(3) + 1;

            if (special * specChance >= 1)
            {
                CurrentHP += 3;
                attack = (int)(attack * (1.3f));
            }
            if (attack < 2)
                attack = 3;
            return attack;
        }


        public override bool GetStatus(out Status s)
        {
            s = null;
            return false;
        }
    }
}
