using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Items;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace spacewizards.Models.Monsters
{
    public class VioletWing : Enemy
    {

        public VioletWing(int CharacterLV, Game1 _game, PlayerCharacter pc)
            : base(CharacterLV, _game)
        {

            image = Content.Load<Texture2D>(@"Images/secondEnemy");
            name = "Violet Wing";
            ATK_MOD = .6f;
            DEF_MOD = .5f;
            SPD_MOD = 1.1f;
            SP_DEF_MOD = .6f;
            SP_ATK_MOD = .5f;
            HP_MOD = .7f;
            goldAmt = this.level * 12;
            CurrentHP = (int)(hp * HP_MOD);
            Drops.Add(new ManaPot("Small Mana Potion", pc, 10, "+10 Mana\n\nA bubbling blue potion.\nIt seems fizzy.",15));

        }

        public override int GetAttack(out bool isSpecial)
        {
            int damage;
            isSpecial = false;
            int attack = (int)(atk * ATK_MOD);
            if (attack > 3)
                damage = attack;
            else
                damage = attack + r.Next(0, 4);
            return damage;
        }

        public override bool GetStatus(out Status s)
        {
            s = null;
            return false;
        }
    }
}
