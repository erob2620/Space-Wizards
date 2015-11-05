using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Models;

namespace spacewizards.Abilities
{
    class SpeedMove : Ability
    {
        private int ranking = 0;
        private int toRankUp = 5;
        public override int Effect(PlayerCharacter pc)
        {
            ranking++;
            if (ranking >= toRankUp)
            {
                pc.Speed++;
                ranking = 0;
                toRankUp += 5;
            }
            if (pc.Speed < 25)
                return pc.Atk * 2;
            if (pc.Speed < 75)
                return pc.Atk * 3;
            if (pc.Speed < 140)
                return pc.Atk * 4;
            if (pc.Speed < 200)
                return pc.Atk * 5;



            pc.GainHP(pc.Atk / 10);
            return pc.Atk * 6;
        }
        public override int getCost(PlayerCharacter pc)
        {
            if (pc.Speed < 25)
                return 5;
            if (pc.Speed < 75)
                return 20;
            if (pc.Speed < 140)
                return 40;
            if (pc.Speed < 200)
                return 80;

            return 160;
        }
        public override string GetName(PlayerCharacter pc)
        {
            if (pc.Speed < 25)
                return "Double Strike";
            if (pc.Speed < 75)
                return "Trinity-Strike";
            if (pc.Speed < 140)
                return "Right in the Quads";
            if (pc.Speed < 200)
                return "HAD five Appendages";
            return "Bloodthirsty Blade";
        }

    }
}
