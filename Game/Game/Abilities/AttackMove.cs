using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Models;

namespace spacewizards.Abilities
{
    public class AttackMove : Ability
    {
        private int ranking = 0;
        private int toRankUp = 5;
        public override int Effect(PlayerCharacter pc)
        {
            ranking++;
            if (ranking >= toRankUp)
            {
                pc.Atk++;
                ranking = 0;
                toRankUp += 5;
            }
            if (pc.Atk < 25)
                return pc.Atk * 2;
            if (pc.Atk < 75)
                return pc.Atk * 3;
            if (pc.Atk < 140)
                return pc.Atk * 4;
            if (pc.Atk < 200)
                return pc.Atk * 5;


            return pc.Atk * 6;
        }
        public override int getCost(PlayerCharacter pc)
        {
            if (pc.Atk < 25)
                return 5;
            if (pc.Atk < 75)
                return 20;
            if (pc.Atk < 140)
                return 40;
            if (pc.Atk < 200)
                return 80;

            return 160;
        }
        public override string GetName(PlayerCharacter pc)
        {
            if (pc.Level < 2)
                return "Smack";
            if (pc.Level < 5)
                return "Pound";
            if (pc.Level < 10)
                return "Smash";
            if (pc.Level < 15)
                return "Crush";
            if (pc.Level < 20)
                return "Brutalize";
            return "Obliterate";
        }
    }
}
