using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using spacewizards.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using spacewizards.Models;
using spacewizards.Items;
using System.Collections;
using Microsoft.Xna.Framework.Input;

namespace spacewizards.GameScreens
{
    public class WorldMapMenuState : BaseGameState
    {
        public PlayerCharacter player { get; set; }
        public PictureBox Background { get; set; }
        public PictureBox SelectorTexture { get; set; }
        public Label invLabel { get; set; }
        public Label goldLabel { get; set; }
        public Label keyLabel { get; set; }
        public List<LinkLabel> LabelList { get; set; }
        public LinkLabel ConOne { get; set; }
        public LinkLabel ConTwo { get; set; }
        public LinkLabel ConThree { get; set; }
        public LinkLabel ConFour { get; set; }
        public LinkLabel ConFive { get; set; }
        public LinkLabel ConSix { get; set; }
        public LinkLabel ConSeven { get; set; }
        public LinkLabel ConEight { get; set; }
        public LinkLabel ConNine { get; set; }
        public LinkLabel ConTen { get; set; }

        public LinkLabel EquipOne { get; set; }
        public LinkLabel EquipTwo { get; set; }
        public LinkLabel EquipThree { get; set; }
        public LinkLabel EquipFour { get; set; }
        public LinkLabel EquipFive { get; set; }
        public LinkLabel EquipSix { get; set; }
        public LinkLabel EquipSeven { get; set; }
        public LinkLabel EquipEight { get; set; }
        public LinkLabel EquipNine { get; set; }
        public LinkLabel EquipTen { get; set; }
        public Label EquipLabel { get; set; }

        public Label Desclabel { get; set; }

        private float maxItemWidth = 0f;
        public float MaxItemWidth
        {
            get { return maxItemWidth; }
            set { maxItemWidth = value; }
        }
        public bool fromCombat;

        public WorldMapMenuState(Game1 game, GameStateManeger manager, PlayerCharacter player, bool fromCombat)
            : base(game, manager)
        {
            this.player = player;
            this.fromCombat = fromCombat;
            changeMusic = false;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            LabelList = new List<LinkLabel>();

            ContentManager Content = Game.Content;
            Background = new PictureBox(Content.Load<Texture2D>(@"Backgrounds\MenuBackground"), GameRef.ScreenRectangle);
            ControlManager.Add(Background);
            //Texture2D Arrow = Content.Load<Texture2D>(@"GUI\leftarrowUp");
            //SelectorTexture = new PictureBox(Arrow, new Rectangle(0, 0, Arrow.Width, Arrow.Height));
            //ControlManager.Add(SelectorTexture);

            invLabel = new Label();
            invLabel.Text = "Inventory\n---------------";
            goldLabel = new Label();
            goldLabel.Text = "Gold: " + player.Money;
            goldLabel.Position = new Vector2(300, 0);
            keyLabel = new Label();
            keyLabel.Text = "Keys: " + player.keys;
            keyLabel.Position = new Vector2(500, 0);
            EquipLabel = new Label();
            EquipLabel.Text = "Equipment\n---------------";
            EquipLabel.Position = new Vector2(0, 500);

            Desclabel = new Label();
            Desclabel.Text = "-";
            Desclabel.Position = new Vector2(450, 450);
            ControlManager.Add(Desclabel);

            ControlManager.Add(EquipLabel);
            ControlManager.Add(invLabel);
            ControlManager.Add(goldLabel);
            ControlManager.Add(keyLabel);

            while (player.ConsumablesList.Count < 10)
            {
                player.ConsumablesList.Add(null);
            }
            while (player.EquipList.Count < 10)
            {
                player.EquipList.Add(null);
            }
            Vector2 position = new Vector2(0, 0);

            ConOne = new LinkLabel();
            ConTwo = new LinkLabel();
            ConThree = new LinkLabel();
            ConFour = new LinkLabel();
            ConFive = new LinkLabel();
            ConSix = new LinkLabel();
            ConSeven = new LinkLabel();
            ConEight = new LinkLabel();
            ConNine = new LinkLabel();
            ConTen = new LinkLabel();

            populateConLabels(position);

            //if (!fromCombat)
            //{
            EquipOne = new LinkLabel();
            EquipTwo = new LinkLabel();
            EquipThree = new LinkLabel();
            EquipFour = new LinkLabel();
            EquipFive = new LinkLabel();
            EquipSix = new LinkLabel();
            EquipSeven = new LinkLabel();
            EquipEight = new LinkLabel();
            EquipNine = new LinkLabel();
            EquipTen = new LinkLabel();


            populateEquipLabels();

            LabelList.Add(ConOne);
            LabelList.Add(ConTwo);
            LabelList.Add(ConThree);
            LabelList.Add(ConFour);
            LabelList.Add(ConFive);
            LabelList.Add(ConSix);
            LabelList.Add(ConSeven);
            LabelList.Add(ConEight);
            LabelList.Add(ConNine);
            LabelList.Add(ConTen);
            LabelList.Add(EquipOne);
            LabelList.Add(EquipTwo);
            LabelList.Add(EquipThree);
            LabelList.Add(EquipFour);
            LabelList.Add(EquipFive);
            LabelList.Add(EquipSix);
            LabelList.Add(EquipSeven);
            LabelList.Add(EquipEight);
            LabelList.Add(EquipNine);
            LabelList.Add(EquipTen);

            //  }

            ControlManager.NextControl();
            //ControlManager.FocusedChanged += new EventHandler(ControlManager_FocusedChanged);

            //ControlManager_FocusedChanged(LabelOne, null);

            foreach (Control c in ControlManager)
            {
                if (c is LinkLabel)
                {
                    if (c.Size.X > MaxItemWidth)
                        maxItemWidth = c.Size.X;

                    position = c.Position;
                    position.Y += c.Size.Y + 5f;
                }
            }
        }

        void populateConLabels(Vector2 position)
        {
            //LabelOne
            if (player.ConsumablesList[0] != null)
            {
                ConOne.Text = player.ConsumablesList[0].ToString();
            }
            else
            {
                ConOne.Text = "-";
            }
            ConOne.Position = new Vector2(10, 120);
            position = ConOne.Position;
            position.Y += 60;
            ControlManager.Add(ConOne);
            ConOne.Selected += new EventHandler(LabelOne_Selected);
            ConOne.Cleared += new EventHandler(LabelOne_Clear);

            //LabelTwo
            if (player.ConsumablesList[1] != null)
            {
                ConTwo.Text = player.ConsumablesList[1].ToString();
            }
            else
            {
                ConTwo.Text = "-";
            }
            ConTwo.Position = position;
            position.Y += 60;
            ControlManager.Add(ConTwo);
            ConTwo.Selected += new EventHandler(LabelTwo_Selected);
            ConTwo.Cleared += new EventHandler(LabelTwo_Clear);


            //LabelThree
            if (player.ConsumablesList[2] != null)
            {
                ConThree.Text = player.ConsumablesList[2].ToString();
            }
            else
            {
                ConThree.Text = "-";
            }
            ConThree.Position = position;
            position.Y += 60;
            ControlManager.Add(ConThree);
            ConThree.Selected += new EventHandler(LabelThree_Selected);
            ConThree.Cleared += new EventHandler(LabelThree_Clear);


            //LabelFour
            if (player.ConsumablesList[3] != null)
            {
                ConFour.Text = player.ConsumablesList[3].ToString();
            }
            else
            {
                ConFour.Text = "-";
            }
            ConFour.Position = position;
            position.Y += 60;
            ControlManager.Add(ConFour);
            ConFour.Selected += new EventHandler(LabelFour_Selected);
            ConFour.Cleared += new EventHandler(LabelFour_Clear);


            //LabelFive
            if (player.ConsumablesList[4] != null)
            {
                ConFive.Text = player.ConsumablesList[4].ToString();
            }
            else
            {
                ConFive.Text = "-";
            }
            ConFive.Position = position;
            position.Y = 120;
            position.X += 400;
            ControlManager.Add(ConFive);
            ConFive.Selected += new EventHandler(LabelFive_Selected);
            ConFive.Cleared += new EventHandler(LabelFive_Clear);


            //LabelSix
            if (player.ConsumablesList[5] != null)
            {
                ConSix.Text = player.ConsumablesList[5].ToString();
            }
            else
            {
                ConSix.Text = "-";
            }
            ConSix.Position = position;
            position.Y += 60;
            ControlManager.Add(ConSix);
            ConSix.Selected += new EventHandler(LabelSix_Selected);
            ConSix.Cleared += new EventHandler(LabelSix_Clear);


            //LabelSeven
            if (player.ConsumablesList[6] != null)
            {
                ConSeven.Text = player.ConsumablesList[6].ToString();
            }
            else
            {
                ConSeven.Text = "-";
            }
            ConSeven.Position = position;
            position.Y += 60;
            ControlManager.Add(ConSeven);
            ConSeven.Selected += new EventHandler(LabelSeven_Selected);
            ConSeven.Cleared += new EventHandler(LabelSeven_Clear);


            //LabelEight
            if (player.ConsumablesList[7] != null)
            {
                ConEight.Text = player.ConsumablesList[7].ToString();
            }
            else
            {
                ConEight.Text = "-";
            }
            ConEight.Position = position;
            position.Y += 60;
            ControlManager.Add(ConEight);
            ConEight.Selected += new EventHandler(LabelEight_Selected);
            ConEight.Cleared += new EventHandler(LabelEight_Clear);


            //LabelNine
            if (player.ConsumablesList[8] != null)
            {
                ConNine.Text = player.ConsumablesList[8].ToString();
            }
            else
            {
                ConNine.Text = "-";
            }
            ConNine.Position = position;
            position.Y += 60;
            ControlManager.Add(ConNine);
            ConNine.Selected += new EventHandler(LabelNine_Selected);
            ConNine.Cleared += new EventHandler(LabelNine_Clear);


            //LabelTen
            if (player.ConsumablesList[9] != null)
            {
                ConTen.Text = player.ConsumablesList[9].ToString();
            }
            else
            {
                ConTen.Text = "-";
            }
            ConTen.Position = position;
            ControlManager.Add(ConTen);
            ConTen.Selected += new EventHandler(LabelTen_Selected);
            ConTen.Cleared += new EventHandler(LabelTen_Clear);

        }

        void populateEquipLabels()
        {
            //LabelOne
            Vector2 position;
            if (player.EquipList[0] != null)
            {
                if (player.EquipList[0].Equipped)
                {
                    EquipOne.Text = player.EquipList[0].Name + "[E]";
                }
                else
                    EquipOne.Text = player.EquipList[0].Name;

            }
            else
            {
                EquipOne.Text = "-";
            }
            EquipOne.Position = new Vector2(10, 620);
            position = EquipOne.Position;
            position.Y += 60;
            ControlManager.Add(EquipOne);
            EquipOne.Selected += new EventHandler(EquipOne_Selected);
            EquipOne.Cleared += new EventHandler(EquipOne_Clear);


            //LabelTwo
            if (player.EquipList[1] != null)
            {
                if (player.EquipList[1].Equipped)
                {
                    EquipTwo.Text = player.EquipList[1].Name + "[E]";
                }
                else
                    EquipTwo.Text = player.EquipList[1].Name;
            }
            else
            {
                EquipTwo.Text = "-";
            }
            EquipTwo.Position = position;
            position.Y += 60;
            ControlManager.Add(EquipTwo);
            EquipTwo.Selected += new EventHandler(EquipTwo_Selected);
            EquipTwo.Cleared += new EventHandler(EquipTwo_Clear);


            //LabelThree
            if (player.EquipList[2] != null)
            {
                if (player.EquipList[2].Equipped)
                {
                    EquipThree.Text = player.EquipList[2].Name + "[E]";
                }
                else
                    EquipThree.Text = player.EquipList[2].Name;
            }
            else
            {
                EquipThree.Text = "-";
            }
            EquipThree.Position = position;
            position.Y += 60;
            ControlManager.Add(EquipThree);
            EquipThree.Selected += new EventHandler(EquipThree_Selected);
            EquipThree.Cleared += new EventHandler(EquipThree_Clear);


            //LabelFour
            if (player.EquipList[3] != null)
            {
                if (player.EquipList[3].Equipped)
                {
                    EquipFour.Text = player.EquipList[3].Name + "[E]";
                }
                else
                    EquipFour.Text = player.EquipList[3].Name;
            }
            else
            {
                EquipFour.Text = "-";
            }
            EquipFour.Position = position;
            position.Y += 60;
            ControlManager.Add(EquipFour);
            EquipFour.Selected += new EventHandler(EquipFour_Selected);
            EquipFour.Cleared += new EventHandler(EquipFour_Clear);


            //LabelFive
            if (player.EquipList[4] != null)
            {
                if (player.EquipList[4].Equipped)
                {
                    EquipFive.Text = player.EquipList[4].Name + "[E]";
                }
                else
                    EquipFive.Text = player.EquipList[4].Name;
            }
            else
            {
                EquipFive.Text = "-";
            }
            EquipFive.Position = position;
            position.Y = 620;
            position.X += 400;
            ControlManager.Add(EquipFive);
            EquipFive.Selected += new EventHandler(EquipFive_Selected);
            EquipFive.Cleared += new EventHandler(EquipFive_Clear);


            //LabelSix
            if (player.EquipList[5] != null)
            {
                if (player.EquipList[5].Equipped)
                {
                    EquipSix.Text = player.EquipList[5].Name + "[E]";
                }
                else
                    EquipSix.Text = player.EquipList[5].Name;
            }
            else
            {
                EquipSix.Text = "-";
            }
            EquipSix.Position = position;
            position.Y += 60;
            ControlManager.Add(EquipSix);
            EquipSix.Selected += new EventHandler(EquipSix_Selected);
            EquipSix.Cleared += new EventHandler(EquipSix_Clear);


            //LabelSeven
            if (player.EquipList[6] != null)
            {
                if (player.EquipList[6].Equipped)
                {
                    EquipSeven.Text = player.EquipList[6].Name + "[E]";
                }
                else
                    EquipSeven.Text = player.EquipList[6].Name;
            }
            else
            {
                EquipSeven.Text = "-";
            }
            EquipSeven.Position = position;
            position.Y += 60;
            ControlManager.Add(EquipSeven);
            EquipSeven.Selected += new EventHandler(EquipSeven_Selected);
            EquipSeven.Cleared += new EventHandler(EquipSeven_Clear);


            //LabelEight
            if (player.EquipList[7] != null)
            {
                if (player.EquipList[7].Equipped)
                {
                    EquipEight.Text = player.EquipList[7].Name + "[E]";
                }
                else
                    EquipEight.Text = player.EquipList[7].Name;
            }
            else
            {
                EquipEight.Text = "-";
            }
            EquipEight.Position = position;
            position.Y += 60;
            ControlManager.Add(EquipEight);
            EquipEight.Selected += new EventHandler(EquipEight_Selected);
            EquipEight.Cleared += new EventHandler(EquipEight_Clear);


            //LabelNine
            if (player.EquipList[8] != null)
            {
                if (player.EquipList[8].Equipped)
                {
                    EquipNine.Text = player.EquipList[8].Name + "[E]";
                }
                else
                    EquipNine.Text = player.EquipList[8].Name;
            }
            else
            {
                EquipNine.Text = "-";
            }
            EquipNine.Position = position;
            position.Y += 60;
            ControlManager.Add(EquipNine);
            EquipNine.Selected += new EventHandler(EquipNine_Selected);
            EquipNine.Cleared += new EventHandler(EquipNine_Clear);


            //LabelTen
            if (player.EquipList[9] != null)
            {
                if (player.EquipList[9].Equipped)
                {
                    EquipTen.Text = player.EquipList[9].Name + "[E]";
                }
                else
                    EquipTen.Text = player.EquipList[9].Name;
            }
            else
            {
                EquipTen.Text = "-";
            }
            EquipTen.Position = position;
            ControlManager.Add(EquipTen);
            EquipTen.Selected += new EventHandler(EquipTen_Selected);
            EquipTen.Cleared += new EventHandler(EquipTen_Clear);

        }

        #region ConsumablesSelected

        void LabelOne_Clear(object sender, EventArgs e)
        {
                player.ConsumablesList.RemoveAt(0);
                player.ConsumablesList.Insert(0, null);
                if (fromCombat)
                {
                    GameRef.ContinueCombat();
                }
                else
                {
                    GameRef.ContinueGame(false);
                }
        }

        void LabelOne_Selected(object sender, EventArgs e)
        {
            Consumable c = (Consumable)player.ConsumablesList[0];
            if (c != null)
            {
                c.Use();
                player.ConsumablesList.RemoveAt(0);
                player.ConsumablesList.Insert(0, null);
                populateConLabels(Vector2.Zero);
                if (fromCombat)
                {
                    GameRef.ContinueCombat();
                }
                else
                {
                    GameRef.ContinueGame(false);
                }
            }
        }

        void LabelTwo_Clear(object sender, EventArgs e)
        {
            player.ConsumablesList.RemoveAt(1);
            player.ConsumablesList.Insert(1, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void LabelTwo_Selected(object sender, EventArgs e)
        {
            Consumable c = (Consumable)player.ConsumablesList[1];
            if (c != null)
            {
                c.Use();
                player.ConsumablesList.RemoveAt(1);
                player.ConsumablesList.Insert(1, null);

                if (fromCombat)
                {
                    GameRef.ContinueCombat();
                }
                else
                {
                    GameRef.ContinueGame(false);
                }
            }
        }

        void LabelThree_Clear(object sender, EventArgs e)
        {
            player.ConsumablesList.RemoveAt(2);
            player.ConsumablesList.Insert(2, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void LabelThree_Selected(object sender, EventArgs e)
        {
            Consumable c = (Consumable)player.ConsumablesList[2];
            if (c != null)
            {
                c.Use();
                player.ConsumablesList.RemoveAt(2);
                player.ConsumablesList.Insert(2, null);
                if (fromCombat)
                {
                    GameRef.ContinueCombat();
                }
                else
                {
                    GameRef.ContinueGame(false);
                }
            }
        }

        void LabelFour_Clear(object sender, EventArgs e)
        {
            player.ConsumablesList.RemoveAt(3);
            player.ConsumablesList.Insert(3, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void LabelFour_Selected(object sender, EventArgs e)
        {
            Consumable c = (Consumable)player.ConsumablesList[3];
            if (c != null)
            {
                c.Use();
                player.ConsumablesList.RemoveAt(3);
                player.ConsumablesList.Insert(3, null);
                if (fromCombat)
                {
                    GameRef.ContinueCombat();
                }
                else
                {
                    GameRef.ContinueGame(false);
                }
            }
        }

        void LabelFive_Clear(object sender, EventArgs e)
        {
            player.ConsumablesList.RemoveAt(4);
            player.ConsumablesList.Insert(4, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void LabelFive_Selected(object sender, EventArgs e)
        {
            Consumable c = (Consumable)player.ConsumablesList[4];
            if (c != null)
            {
                c.Use();
                player.ConsumablesList.RemoveAt(4);
                player.ConsumablesList.Insert(4, null);
                if (fromCombat)
                {
                    GameRef.ContinueCombat();
                }
                else
                {
                    GameRef.ContinueGame(false);
                }
            }
        }

        void LabelSix_Clear(object sender, EventArgs e)
        {
            player.ConsumablesList.RemoveAt(5);
            player.ConsumablesList.Insert(5, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void LabelSix_Selected(object sender, EventArgs e)
        {
            Consumable c = (Consumable)player.ConsumablesList[5];
            if (c != null)
            {
                c.Use();
                player.ConsumablesList.RemoveAt(5);
                player.ConsumablesList.Insert(5, null);
                if (fromCombat)
                {
                    GameRef.ContinueCombat();
                }
                else
                {
                    GameRef.ContinueGame(false);
                }
            }
        }

        void LabelSeven_Clear(object sender, EventArgs e)
        {
            player.ConsumablesList.RemoveAt(6);
            player.ConsumablesList.Insert(6, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void LabelSeven_Selected(object sender, EventArgs e)
        {
            Consumable c = (Consumable)player.ConsumablesList[6];
            if (c != null)
            {
                c.Use();
                player.ConsumablesList.RemoveAt(6);
                player.ConsumablesList.Insert(6, null);
                if (fromCombat)
                {
                    GameRef.ContinueCombat();
                }
                else
                {
                    GameRef.ContinueGame(false);
                }
            }
        }

        void LabelEight_Clear(object sender, EventArgs e)
        {
            player.ConsumablesList.RemoveAt(7);
            player.ConsumablesList.Insert(7, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void LabelEight_Selected(object sender, EventArgs e)
        {
            Consumable c = (Consumable)player.ConsumablesList[7];
            if (c != null)
            {
                c.Use();
                player.ConsumablesList.RemoveAt(7);
                player.ConsumablesList.Insert(7, null);
                if (fromCombat)
                {
                    GameRef.ContinueCombat();
                }
                else
                {
                    GameRef.ContinueGame(false);
                }
            }
        }

        void LabelNine_Clear(object sender, EventArgs e)
        {
            player.ConsumablesList.RemoveAt(8);
            player.ConsumablesList.Insert(8, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void LabelNine_Selected(object sender, EventArgs e)
        {
            Consumable c = (Consumable)player.ConsumablesList[8];
            if (c != null)
            {
                c.Use();
                player.ConsumablesList.RemoveAt(8);
                player.ConsumablesList.Insert(8, null);
                if (fromCombat)
                {
                    GameRef.ContinueCombat();
                }
                else
                {
                    GameRef.ContinueGame(false);
                }
            }
        }

        void LabelTen_Clear(object sender, EventArgs e)
        {
            player.ConsumablesList.RemoveAt(9);
            player.ConsumablesList.Insert(9, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void LabelTen_Selected(object sender, EventArgs e)
        {
            Consumable c = (Consumable)player.ConsumablesList[9];
            if (c != null)
            {
                c.Use();
                player.ConsumablesList.RemoveAt(9);
                player.ConsumablesList.Insert(9, null);
                if (fromCombat)
                {
                    GameRef.ContinueCombat();
                }
                else
                {
                    GameRef.ContinueGame(false);
                }
            }
        }
        #endregion
        #region Equippablesselected

        void EquipOne_Clear(object sender, EventArgs e)
        {
            player.EquipList.RemoveAt(0);
            player.EquipList.Insert(0, null);
            populateEquipLabels();
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void EquipOne_Selected(object sender, EventArgs e)
        {
            Equipment c = player.EquipList[0];
            if (c != null)
            {
                player.Equip(c);
            }
        }

        void EquipTwo_Clear(object sender, EventArgs e)
        {
            player.EquipList.RemoveAt(1);
            player.EquipList.Insert(1, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void EquipTwo_Selected(object sender, EventArgs e)
        {
            Equipment c = player.EquipList[1];
            if (c != null)
            {
                player.Equip(c);
            }
        }

        void EquipThree_Clear(object sender, EventArgs e)
        {
            player.EquipList.RemoveAt(2);
            player.EquipList.Insert(2, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void EquipThree_Selected(object sender, EventArgs e)
        {
            Equipment c = player.EquipList[2];
            if (c != null)
            {
                player.Equip(c);
            }
        }

        void EquipFour_Clear(object sender, EventArgs e)
        {
            player.EquipList.RemoveAt(3);
            player.EquipList.Insert(3, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void EquipFour_Selected(object sender, EventArgs e)
        {
            Equipment c = player.EquipList[3];
            if (c != null)
            {
                player.Equip(c);
            }
        }

        void EquipFive_Clear(object sender, EventArgs e)
        {
            player.EquipList.RemoveAt(4);
            player.EquipList.Insert(4, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void EquipFive_Selected(object sender, EventArgs e)
        {
            Equipment c = player.EquipList[4];
            if (c != null)
            {
                player.Equip(c);
            }
        }

        void EquipSix_Clear(object sender, EventArgs e)
        {
            player.EquipList.RemoveAt(5);
            player.EquipList.Insert(5, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void EquipSix_Selected(object sender, EventArgs e)
        {
            Equipment c = player.EquipList[5];
            if (c != null)
            {
                player.Equip(c);
            }
        }

        void EquipSeven_Clear(object sender, EventArgs e)
        {
            player.EquipList.RemoveAt(6);
            player.EquipList.Insert(6, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void EquipSeven_Selected(object sender, EventArgs e)
        {
            Equipment c = player.EquipList[6];
            if (c != null)
            {
                player.Equip(c);
            }
        }

        void EquipEight_Clear(object sender, EventArgs e)
        {
            player.EquipList.RemoveAt(7);
            player.EquipList.Insert(7, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void EquipEight_Selected(object sender, EventArgs e)
        {
            Equipment c = player.EquipList[7];
            if (c != null)
            {
                player.Equip(c);
            }
        }

        void EquipNine_Clear(object sender, EventArgs e)
        {
            player.EquipList.RemoveAt(8);
            player.EquipList.Insert(8, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void EquipNine_Selected(object sender, EventArgs e)
        {
            Equipment c = player.EquipList[8];
            if (c != null)
            {
                player.Equip(c);
            }
        }

        void EquipTen_Clear(object sender, EventArgs e)
        {
            player.EquipList.RemoveAt(9);
            player.EquipList.Insert(9, null);
            if (fromCombat)
            {
                GameRef.ContinueCombat();
            }
            else
            {
                GameRef.ContinueGame(false);
            }
        }

        void EquipTen_Selected(object sender, EventArgs e)
        {
            Equipment c = player.EquipList[9];
            if (c != null)
            {
                player.Equip(c);
            }
        }
        #endregion
        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            base.Draw(gameTime);
            ControlManager.Draw(GameRef.spriteBatch);
            GameRef.spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (player.ConsumablesList != null)
            {
                foreach (Item i in player.ConsumablesList)
                {
                    if (i != null)
                    {
                        if (ControlManager[ControlManager.selectedControl].Text.Equals(i.Name))
                        {
                            Desclabel.Text = i.Description;
                        }
                    }

                }
            }
            if (player.EquipList != null)
            {
                foreach (Item i in player.EquipList)
                {
                    if (i != null)
                    {
                        if (ControlManager[ControlManager.selectedControl].Text.Equals(i.Name) || ControlManager[ControlManager.selectedControl].Text.Equals(i.Name + "[E]"))
                        {
                            Desclabel.Text = i.Description;
                        }
                    }
                }
            }
            if (InputHandler.KeyPressed(Keys.I) || InputHandler.KeyPressed(Keys.Escape))
            {
                if (fromCombat)
                {
                    GameRef.ContinueCombat();
                }
                else
                {
                    GameRef.ContinueGame(false);
                }
            }
            Vector2 position = new Vector2(0, 0);
            populateConLabels(position);
            populateEquipLabels();

            ControlManager.Update(gameTime, playerIndexInControl);
            base.Update(gameTime);
        }
    }
}
