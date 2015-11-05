using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewizards.Items
{
    class Treasure : AutomatedSprite
    {
        public Mapper mapper { get; set; }
        public List<Item> treasureList { get; set; }
        public bool textBox = false;
        private string text;
        public Treasure(Texture2D textureImage, Vector2 position, int collisionOffset, Mapper mapper, Game1 game)
            : base(textureImage, position, new Point(32, 32), 0, new Point(0, 0), new Point(0, 0), Vector2.Zero, game)
        {
            this.zIndex = .05f;
            this.isInteractable = true;
            this.interactOnTouch = false;
            this.passable = false;
            this.mapper = mapper;
            treasureList = new List<Item>();
                Equipment staff1 = new Equipment("Pointy Stick", 2, 0, Placement.Weapon, game.Character, "+2 ATK\nA stick, sharpened to a point.");
                Equipment armor1 = new Equipment("Torn Robes", 0, 2, Placement.Armor, game.Character, "+2 DEF\nSome torn, tattered old robes.");
                Consumable pot1 = new HealthPot("Health Potion", game.Character, "+35% HP\nA small, red potion.\nAs generic as they come.", .35, 10);
                Equipment staff2 = new Equipment("Apprentice's Staff", 5, 0, Placement.Weapon, game.Character, "+5 ATK\nA staff that slightly glows\nwith traces of magic.");
                Consumable pot2 = new HealthPot("Health Potion", game.Character, "+35% HP\nA small, red potion.\nAs generic as they come.", .35, 10);
                Equipment armor2 = new Equipment("Magician's Robe", 0, 5, Placement.Armor, game.Character, "+5 DEF\nMore for show than anything.");
                Consumable pot3 = new ManaPot("Small Mana Potion", game.Character, 10, "+10 Mana\nA bubbling blue potion.\nIt seems fizzy.",10);
                Consumable pot4 = new ManaPot("Mana Potion", game.Character, 15, "+15 Mana\nA bigger, bluer potion. This\none seems less volatile.",10);
                Equipment staff3 = new Equipment("Pimp Cane", 9, 0, Placement.Weapon, game.Character, "+9 ATK\nA cane, with \"Zimos\" engraved\non the handle.");
                Equipment armor3 = new Equipment("Robe of Pretty Colors", 0, 10, Placement.Armor, game.Character, "+10 DEF\nSparkles with all the colors\nin the world, and a few\nfrom out of it.");
                Consumable pot5 = new HealthPot("Freakin' Big(TM) Potion", game.Character, .5, "+50% HP\nBrewed by Effin' Pots, the\nlocal kingdom's number-one potion brewery.");
                Equipment staff4 = new Equipment("Rod of Fortification", 10, 4, Placement.Weapon, game.Character, "+10 ATK +4 DEF\nThis rod feels very solid\nin your hands.");
                Consumable pot6 = new ManaPot("Mana Potion", game.Character, 15, "+15 Mana\nA bigger, bluer potion. This\none seems less volatile.",10);
                Equipment armor4 = new Equipment("An Oddly Sharp Robe", 5, 10, Placement.Armor, game.Character, "+5 ATK +10 DEF\nThis robe has more sharp\ncorners than you'd expect.");
                Equipment staff5 = new Equipment("Staff of Nightmares", 18, 0, Placement.Weapon, game.Character, "+18 ATK\nWaves of unsettling darkness\ndrift from this staff.");
                Consumable pot7 = new HealthPot("Freakin' Big(TM) Potion", game.Character, .50, "+50% HP\nBrewed by Effin' Pots, the\nlocal kingdom's number-one potion brewery.");
                Consumable pot8 = new HealthPot("Freakin' Big(TM) Potion", game.Character, .50, "+50% HP\nBrewed by Effin' Pots, the\nlocal kingdom's number-one potion brewery.");
                Equipment armor5 = new Equipment("Cloak of Resistances", 0, 20, Placement.Armor, game.Character, "+20 DEF\nThis cloak seems to ripple\ngently with each of the elements.");
                Equipment staff6 = new Equipment("Staff of Fury", 25, -5, Placement.Weapon, game.Character, "+25 ATK, -5 DEF\nHolding this staff causes\nwaves of hatred to crash\nthrough your mind and body.");
                Consumable pot9 = new ManaPot("Archmage's Draught", game.Character, 50, "+50 Mana\nThis potion glows with a strange radiance.",10);
                Equipment armor6 = new Equipment("Bone Lord's Robe", 0, 25, Placement.Armor, game.Character, "+25 DEF\nOld black robes, still steeped\nin dark magicks.");
                Equipment staff7 = new Equipment("Ancient Staff of Power", 35, 10, Placement.Weapon, game.Character, "+35 ATK\nGreat eldritch powers flow\nthrough this staff, making\nit hum in your hands.");
                Equipment armor7 = new Equipment("Cataclysmic Shell", -10, 40, Placement.Armor, game.Character, "-10 ATK, +40 DEF\nA fragment of shell from Yrkin,\none of the old gods,\nnow long forgotten.");
                Consumable pot10 = new ManaPot("Archmage's Draught", game.Character, 50, "+50 Mana\nThis potion glows with a strange radiance.",10);
                Consumable pot11 = new HealthPot("Vial of Immortality", game.Character, 1.0, "+100% HP\nThe potion is silvery, like\nliquid soul.\nYou decide not to question its origins.");                
                Equipment staff8 = new Equipment("Debugger", 50, 5, Placement.Weapon, game.Character, "+50 ATK, +5 DEF\n65 64 6f 63 27 73 69 6c");
                Equipment armor8 = new Equipment("Neumontium Coat", 0, 50, Placement.Armor, game.Character, "+50 DEF\nLegend says that only\nthe most l337 of players\ncould even bear the weight\nof this coat.");

                treasureList.Add(staff1);
                treasureList.Add(armor1);
                treasureList.Add(pot1);
                treasureList.Add(staff2);
                treasureList.Add(pot2);
                treasureList.Add(armor2);
                treasureList.Add(pot3);
                treasureList.Add(pot4);
                treasureList.Add(staff3);
                treasureList.Add(armor3);
                treasureList.Add(pot5);
                treasureList.Add(staff4);
                treasureList.Add(pot6);
                treasureList.Add(armor4);
                treasureList.Add(staff5);
                treasureList.Add(pot7);
                treasureList.Add(pot8);
                treasureList.Add(armor5);
                treasureList.Add(staff6);
                treasureList.Add(pot9);
                treasureList.Add(armor6);
                treasureList.Add(staff7);
                treasureList.Add(armor7);
                treasureList.Add(pot10);
                treasureList.Add(pot11);
                treasureList.Add(staff8);
                treasureList.Add(armor8);
            //28 total
            //11

        }

        public override void Interact()
        {
            Item i = treasureList[game.Character.treasuresOpened];
            int nulls;
            if (i is Consumable)
            {
                nulls = 0;
                foreach (Item item in game.Character.ConsumablesList)
                {
                    if (item == null)
                    {
                        nulls++;
                    }
                }
                if (nulls <= 0)
                {
                    game.gamePlay.showDialog("Inventory full! Please use or drop something first.");
                }
                else
                {
                    i = ((Consumable)i);
                    game.Character.AcquireItem(i as Consumable);
                }
            }
            else
            {
                nulls = 0;
                foreach (Item item in game.Character.EquipList)
                {
                    if (item == null)
                    {
                        nulls++;
                    }
                }
                if (nulls <= 0)
                {
                    game.gamePlay.showDialog("Inventory full! Please use or drop something first.");
                }
                else
                {
                    i = ((Equipment)i);
                    game.Character.AcquireItem(i as Equipment);
                }
            }
            game.Character.treasuresOpened++;
            this.position = new Vector2(-100, -100);
            this.passable = false;
            this.isInteractable = false;
            game.ShowGainedItems("You Acquired:\n" + i.Name + "\n" + i.Description, false);
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            base.Update(gameTime, clientBounds);
        }
    }
}
