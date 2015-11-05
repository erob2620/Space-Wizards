using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Models.Statuses;
using Microsoft.Xna.Framework.Graphics;
using spacewizards.Items;

namespace spacewizards.Models.Monsters
{
    class ToxWyrm : Enemy
    {
        public ToxWyrm(int characterLV, Game1 _game, PlayerCharacter pc)
            : base(characterLV, _game)
        {
            name = "Toxwyrm";
            image = Content.Load<Texture2D>(@"Images/fireworm");
            ATK_MOD = .5f;
            DEF_MOD = .9f;
            SP_DEF_MOD = .85f;
            atk = 12;
            goldAmt = this.level * 12;
            Drops.Add(new HealthPot("Health Potion", pc, "+35% HP\n\nA small, red potion.\nAs generic as they come.",20));

        }
        public override int GetAttack(out bool isSpecial)
        {
            if (r.Next(5) >= 0)
            {
                hasStatus = true;
            }

            isSpecial = true;
            return (int)(atk * ATK_MOD);
        }

        public override bool GetStatus(out Status s)
        {
            s = null;
            if (hasStatus)
                s = new Poison();
            return s != null;
        }
    }
}
