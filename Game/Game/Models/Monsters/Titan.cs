using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Models.Statuses;

namespace spacewizards.Models.Monsters
{
    class Titan : Enemy
    {
        public Titan(int characterLV, Game1 _game)
            : base(characterLV, _game)
        {
            this.hpMod = 1.2f;
            this.DEF_MOD = 1.3f;
            this.ATK_MOD = .9f;
            this.SP_ATK_MOD = .9f;
            this.goldAmt = this.level * 12;
            name = "Earth Titan";

            //if(characterLV > 10)
            //this.name = (characterLV > 14 ? "Crystal Titan" : "Obsidian Titan");

        }
        public override int GetAttack(out bool isSpecial)
        {
            if (r.Next(5) == 4)
            {
                hasStatus = true;
            }

            isSpecial = true;
            return (int)(Atk * 1.2);
        }

        public override bool GetStatus(out Status s)
        {
            s = null;
            if (hasStatus)
            {
                s = new DefenceBreak();
                hasStatus = false;
            }
            return s != null;
        }
    }
}
