using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace spacewizards.Models.Monsters
{
    public class JerryPay : Enemy
    {
        public JerryPay(int characterLV, Game1 _game, PlayerCharacter pc)
            : base(characterLV, _game)
        {
            image = Content.Load<Texture2D>(@"Images/pay");
            this.SP_ATK_MOD = 1f;
            this.SP_DEF_MOD = 1f;
            this.SPD_MOD = 1f;
            this.DEF_MOD = 1f;
            this.ATK_MOD = 1f;
            this.HP_MOD = 1.3f;
            this.name = "The Old Man";
            this.level = characterLV + 1;
            this.CurrentHP = HP;
            goldAmt = this.level * 50;
        }
        public override int GetAttack(out bool isSpecial)
        {
            int attack;
            isSpecial = (r.Next(0, 2) == 0) ? false : true;

            attack = (isSpecial) ? (int)(specatk * SP_ATK_MOD) : (int)(atk * ATK_MOD);
            return attack;
        }

        public override bool GetStatus(out Status s)
        {
            s = null;
            return false;
        }
    }
}
