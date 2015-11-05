using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Models;

namespace spacewizards.Abilities
{
    class DefenseMove : Ability
    {

        private int ranking = 0;
        private int toRankUp = 5;
        public override int Effect(PlayerCharacter pc)
        {
            ranking++;
            if (ranking >= toRankUp)
            {
                pc.Defense++;
                ranking = 0;
                toRankUp += 5;
            }
            if (pc.Defense < 25)
                pc.TempDef += 5;
            else if (pc.Defense < 75)
                pc.TempDef += 25;
            else if (pc.Defense < 140)
                pc.TempDef += 50;
            else if (pc.Defense < 200)
                pc.TempDef += 75;
            else
                pc.TempDef += 100;



            return 0;
        }
        public override int getCost(PlayerCharacter pc)
        {
            if (pc.Defense < 25)
                return 5;
            else if (pc.Defense < 75)
                return 20;
            else if (pc.Defense < 140)
                return 40;
            else if (pc.Defense < 200)
                return 80;
            return 160;
        }
        public override string GetName(PlayerCharacter pc)
        {
            if (pc.Defense < 25)
                return "Block";
            if (pc.Defense < 75)
                return "Entrench";
            if (pc.Defense < 140)
                return "Hold the Line";
            if (pc.Defense < 200)
                return "Iron Hide";
            return "Indestructable";
        }
    }
}
