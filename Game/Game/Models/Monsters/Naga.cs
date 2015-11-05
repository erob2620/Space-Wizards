using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Models;
using spacewizards.Items;
using Microsoft.Xna.Framework.Graphics;

namespace spacewizards.Models.Monsters
{
    class Naga : Enemy
    {
        public Naga(int characterLV, Game1 _game, PlayerCharacter pc)
            : base(characterLV, _game)
        {
            image = Content.Load<Texture2D>(@"Images/naga");
            name = "Naga";
            ATK_MOD = .4f;
            DEF_MOD = .4f;
            SPD_MOD = 1;
            SP_DEF_MOD = .7f;
            SP_ATK_MOD = .7f;
            HP_MOD = .6f;
            this.CurrentHP = HP;
            this.level = characterLV;
            goldAmt = this.level * 12;
        }

        public override int GetAttack(out bool isSpecial)
        {
            throw new NotImplementedException();
        }

        public override bool GetStatus(out Status s)
        {
            s = null;
            return false;
        }
    }
}
