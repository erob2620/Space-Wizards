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
    public class Item
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public PlayerCharacter Player { get; set; }
        public string Description { get; set; }

        public Item()
        {

        }

        public Item(string name, PlayerCharacter player, string desc)
        {
            this.Name = name;
            this.Player = player;
            this.Description = desc;
        }
    }
}
