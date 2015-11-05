using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Models;

namespace spacewizards.Items
{
    public class ManaPot : Consumable
    {
        public int ManaGained { get; set; }

        public ManaPot(string name, PlayerCharacter player, int manaGained, string desc, int cost) : base(name, player, desc)
        {
            Name = name;
            Player = player;
            ManaGained = manaGained;
            Cost = cost;
            Description = desc;
        }
        public override void Use()
        {
            Player.GainMana(ManaGained);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
