using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Items;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewizards.Models
{
    public abstract class Enemy : Character
    {

        public Game1 game;
        public ContentManager Content;
        public Texture2D image;

        protected static Random r = new Random();
        protected Status status { get; set; }
        private int expGiven = 150;
        public int goldAmt { get; set; }
        protected bool hasStatus;
        protected float xpModifier
        {
            get
            {
                return (SP_ATK_MOD + SP_DEF_MOD + ATK_MOD + DEF_MOD + HP_MOD) / 5;
            }



        }
        public int EXPGiven
        {
            get
            {

                return expGiven;
            }
        }
        public Enemy(int characterLV, Game1 _game)
        {
            game = _game;
            
            Content = game.Content;

            this.level = characterLV;
            this.CurrentHP = HP;
            expGiven = (int)(40 * (Math.Pow(1.1, this.level)) * xpModifier);
        }
        //public Enemy() { }
        private List<Item> drops = new List<Item>();
        public List<Item> Drops
        {
            get { return drops; }
            private set
            {
                drops = value;
            }
        }
        public abstract int GetAttack(out bool isSpecial);
        public abstract bool GetStatus(out Status s);

    }
}
