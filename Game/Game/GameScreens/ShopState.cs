using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Controls;
using spacewizards.Models;
using spacewizards.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace spacewizards.GameScreens
{
    class ShopState : BaseGameState
    {
        public PictureBox Background { get; set; }

        int numKeys = 0;
        PlayerCharacter player;
        List<Item> shopInventory;

        LinkLabel item1;
        LinkLabel item2;


        public Label title { get; set; }
        public Label gold { get; set; }


        public ShopState(Game1 game, GameStateManeger manager, PlayerCharacter player)
            : base(game, manager)
        {
            this.player = player;
            switch (numKeys)
            {
                case 1:
                    shopInventory = new List<Item>
                    {
                        new HealthPot("Small Health Potion", player, "+10% HP\n\nA tiny, red potion.", .10, 10), new ManaPot("Small Mana Potion", game.Character, 10, "+10 Mana\nA bubbling blue potion.\nIt seems fizzy.",10)
                    };
                    break;
                case 2:
                    shopInventory = new List<Item>
                    {
                        new HealthPot("Health Potion", player, "+35% HP\nA small, red potion.\nAs generic as they come.", .35, 30), new ManaPot("Mana Potion", game.Character, 15, "+15 Mana\nA bigger, bluer potion. This\none seems less volatile.",30)
                    };
                    break;
                case 3:
                    shopInventory = new List<Item>
                    {
                        new HealthPot("Freakin' Big(TM) Potion", game.Character, "+50% HP\nBrewed by Effin' Pots, the\nlocal kingdom's number-one potion brewery.", .5, 100), new ManaPot("Large Mana Potion", player, 20, "+20 Mana\n\nA swirly, blue-ish potion.",100)
                    };
                    break;
                case 4:
                    shopInventory = new List<Item>
                    {
                        new HealthPot("Vial of Immortality", game.Character, "+100% HP\nThe potion is silvery, like\nliquid soul.\nYou decide not to question its origins.", 1.0, 5000), new ManaPot("Archmage's Draught", game.Character, 50, "+50 Mana\n\nThis potion glows with a strange radiance.",1000)
                    };
                    break;
                default:
                    shopInventory = new List<Item>
                    {
                        new HealthPot("Small Health Potion", player, "+10% HP\n\nA tiny, red potion.", .1, 10), new ManaPot("Small Mana Potion", player, 5,"+10 Mana\n\nA bubbling blue potion.\nIt seems fizzy.",10)
                    };
                    break;
            }

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            populateLabels(new Vector2(10, 120));
            gold.Text = "Gold:  " + player.Money;
            ControlManager.Update(gameTime, playerIndexInControl);
            if (InputHandler.KeyPressed(Keys.Escape))
            {
                GameRef.ContinueGame(false);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            ContentManager content = Game.Content;

            Background = new PictureBox(GameRef.Content.Load<Texture2D>(@"Backgrounds\MenuBackground"), GameRef.ScreenRectangle);
            ControlManager.Add(Background);

            gold = new Label();
            gold.Text = "Gold:  " + player.Money;
            gold.Position = new Vector2(400, 0);

            title = new Label();
            title.Text = " BulkShop";

            item1 = new LinkLabel();
            item2 = new LinkLabel();


            ControlManager.Add(title);
            ControlManager.Add(gold);
            Vector2 pos = new Vector2(10, 120);
            populateLabels(pos);

            ControlManager.NextControl();
        }
        public void populateLabels(Vector2 pos)
        {


            item1.Text = shopInventory[0].ToString() + "  " + shopInventory[0].Cost;
            item2.Text = shopInventory[1].ToString() + "  " + shopInventory[1].Cost;


            item1.Position = new Vector2(pos.X + 400, pos.Y);
            item2.Position = pos;


            item1.Selected += new EventHandler(selected);
            item2.Selected += new EventHandler(selected);


            ControlManager.Add(item2);

            ControlManager.Add(item1);



        }
        public void clearLabels()
        {
            ControlManager.Remove(item1);
            ControlManager.Remove(item2);

        }
        void selected(Object sender, EventArgs e)
        {
            bool bought = false;
            LinkLabel ll = (LinkLabel)sender;
            if (ll.Text.Equals((shopInventory[0].ToString() + "  " + shopInventory[0].Cost)) && !bought)
            {
                if (player.Money >= shopInventory[0].Cost && !bought)
                {
                    player.Money -= shopInventory[0].Cost;

                    if (shopInventory[0] is Consumable)
                    {
                        bought = true;
                        player.AcquireItem((Consumable)shopInventory[0]);
                    }

                    if (shopInventory[0] is Equipment)
                    {
                        bought = true;
                        player.AcquireItem((Equipment)shopInventory[0]);
                    }



                }




            }
            else if (ll.Text.Equals(shopInventory[1].ToString() + "  " + shopInventory[1].Cost) && !bought)
            {
                if (player.Money >= shopInventory[1].Cost)
                {
                    player.Money -= shopInventory[1].Cost;
                    if (shopInventory[1] is Consumable)
                    {
                        player.AcquireItem((Consumable)shopInventory[1]);
                        bought = true;
                    }
                    if (shopInventory[1] is Equipment)
                    {
                        player.AcquireItem((Equipment)shopInventory[1]);
                        bought = true;
                    }
                }


            }




        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            ControlManager.Draw(GameRef.spriteBatch);
            base.Draw(gameTime);
            GameRef.spriteBatch.End();
        }
    }
}
