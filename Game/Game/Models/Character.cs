using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacewizards.Models
{
    public class Character
    {
        protected int level = 1;
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }
        public String name;
        protected static int hp = 25;

        public int HP
        {
            get
            {
                return (int)((hp * HP_MOD));
            }
            set
            {
                hp = value;
            }
        }
        protected int currenthp;
        public int CurrentHP
        {
            get
            {
                return currenthp;
            }
            set
            {
                currenthp = value;
            }
        }
        protected static int speed = 10;
        public int Speed
        {
            get
            {
                return speed * (int)((SPD_MOD));
            }
            set
            {
                speed = value;
            }
        }
        protected static int atk = 7;
        public int Atk
        {
            get
            {
                return atk * (int)((ATK_MOD));
            }
            set
            {
                atk = value;
            }
        }
        protected static int defense = 7;
        public int Defense
        {
            get
            {
                return defense * (int)((DEF_MOD));
            }
            set
            {
                defense = value;
            }
        }
        protected static int specatk = 7;
        public int SpecAttack
        {
            get
            {
                return specatk * (int)((SP_ATK_MOD));
            }
            set
            {
                specatk = value;
            }
        }
        protected static int specdef = 7;
        public int SpecDefense
        {
            get
            {
                return specdef * (int)((SP_DEF_MOD));
            }
            set
            {
                specdef = value;
            }
        }
        public Character()
        {
            name = "Emerson";
            CurrentHP = hp;
        }
        public Character(string _name)
        {
            name = _name;
        }


        protected float specAtKMod = 1;
        protected float specDefModifier = 1;
        protected float atkModifier = 1;
        protected float defModifier = 1;
        protected float speedModifier = 1.2f;
        protected float specChance = .2f;
        protected float hpMod = 1;

        public float SP_ATK_MOD
        {
            get
            {
                return specAtKMod;
            }
            protected set { specAtKMod = value; }
        }
        public float HP_MOD
        {
            get
            {
                return hpMod;
            }
            protected set { hpMod = value; }
        }
        public float SP_DEF_MOD
        {
            get { return specDefModifier; }
            set { specDefModifier = value; }
        }

        public float ATK_MOD
        {
            get { return atkModifier; }
            protected set { atkModifier = value; }
        }

        public float DEF_MOD
        {
            get { return defModifier; }
            protected set { defModifier = value; }
        }

        public float SPD_MOD
        {
            get { return speedModifier; }
            protected set { speedModifier = value; }
        }

    }
}
