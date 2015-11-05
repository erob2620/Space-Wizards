using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Models;

namespace spacewizards.Items
{
    public enum Placement
    {
        Armor, Weapon
    }
    public class Equipment : Item
    {
        private Placement part;
        public Placement WhereWorn
        {
            get
            {
                return part;
            }
        }
        private bool equipped;
        public bool Equipped
        {
            get
            {
                return equipped;
            }
            set
            {
                equipped = value;
            }
        }
        private int addAtk;
        public int AddAtk
        {
            get
            {
                return addAtk;
            }
        }
        private int addDef;
        public int AddDef
        {
            get
            {
                return addDef;
            }
        }
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
        }
        public Equipment(string _name, int a, int d, Placement p, PlayerCharacter pc, string desc)
            : base(_name, pc, desc)
        {
            addAtk = a;
            addDef = d;
            part = p;
            description = desc;
        }
    }
}
