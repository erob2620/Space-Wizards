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
    public class Consumable : Item
    {
        public new string Name { get; set; }
        public new PlayerCharacter Player { get; set; }
        public string Description { get; set; }

        public Consumable()
        {

        }

        public Consumable(string name, PlayerCharacter player, string desc) : base(name, player, desc)
        {
            this.Name = name;
            this.Player = player;
            this.Description = desc;
        }

        public virtual void Use()
        {

        }
    }
}
