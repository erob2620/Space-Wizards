using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using spacewizards.Models;
using spacewizards.Items;

namespace spacewizards.Items
{
    public class HealthPot : Consumable
    {
        //public new string Name { get; set; }
        //public new PlayerCharacter Player { get; set; }
        public int Healed { get; set; }
        //public string Description { get; set; }

        public HealthPot(string name, PlayerCharacter player, string desc, int cost) : base(name, player, desc)
        {
            this.Name = name;
            this.Player = player;
            this.Healed = (int)(player.HP * .35);
            this.Description = desc;
            Cost = cost;
        }

        public HealthPot(string name, PlayerCharacter player, string desc, double healed, int cost)
            : base(name, player, desc)
        {
            this.Name = name;
            this.Player = player;
            this.Healed = (int)(player.HP * .35);
            this.Description = desc;
            Cost = cost;
        }

        public HealthPot(string name, PlayerCharacter player, double healed, string desc) : base(name, player, desc)
        {
            //this.Name = name;
            //this.Player = player;
            this.Healed = (int)(player.HP * healed);
            //this.Description = desc;
        }

        public override void Use()
        {
            Player.GainHP(Healed);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
