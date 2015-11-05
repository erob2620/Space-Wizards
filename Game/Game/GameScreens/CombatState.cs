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
using spacewizards.Abilities;
using Microsoft.Xna.Framework.Media;

namespace spacewizards.GameScreens
{
    public class CombatState : BaseGameState
    {
        private bool isSpecialAttack = false;

        private bool textBox = false;
        private string text;
        private SpriteFont font;
        private Vector2 textPos = new Vector2(500, 500);
        private Texture2D textSprite;
        public int totalGold;
        private string EnemyMoves = "";

        public const string OutOfMana = "You are out of Mana \nPlease select another option \n \n   Press X to continue";

        public static Random r = new Random();
        public List<Item> enemyDrops = new List<Item>();
        public PictureBox Background { get; set; }
        public PictureBox ArrowTexture { get; set; }
        public PictureBox enemyBackground { get; set; }

        public PlayerCharacter Player { get; set; }
        public List<Enemy> Enemies { get; set; }

        public Label PlayerLevel { get; set; }
        public Label PlayerHealth { get; set; }
        public Label PlayerName { get; set; }
        public Label PlayerMana { get; set; }

        public Label FakeAttack { get; private set; }
        public Label SpecialLabel { get; set; }
        public Label ItemsLabel { get; set; }
        public Label RunLabel { get; set; }

        public Label EnemyName { get; set; }
        public Label EnemyHealth { get; set; }
        public Label EnemyLevelLabel { get; set; }
        public Label EnemyLevel { get; set; }
        public Label EnemyHealthLabel { get; set; }

        public Label SecondEnemyHealth { get; set; }
        public Label SecondEnemyName { get; set; }
        public Label SecondEnemyHealthLabel { get; set; }
        public Label SecondEnemyLevel { get; set; }
        public Label SecondEnemyLevelLabel { get; set; }
        public PictureBox SecondEnemyBackground { get; set; }

        public LinkLabel Attack { get; set; }
        public LinkLabel Items { get; set; }
        public LinkLabel Special { get; set; }
        public LinkLabel Run { get; set; }


        public StringBuilder sb;
        public LinkLabel firstEnemy;
        public LinkLabel SecondEnemy;


        private float maxItemWidth = 0f;
        public float MaxItemWidth
        {
            get { return maxItemWidth; }
            set { maxItemWidth = value; }
        }
        public CombatState(Game1 game, GameStateManeger manager, PlayerCharacter player, List<Enemy> enemies)
            : base(game, manager)
        {
            totalGold = 0;
            this.Song = game.Content.Load<Song>("Music/Battle");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Song);

            Player = player;
            Enemies = enemies;
            foreach (Enemy e in Enemies)
            {
                totalGold += e.goldAmt;
            }
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            base.Draw(gameTime);
            ControlManager.Draw(GameRef.spriteBatch);
            if (textBox)
            {
                Vector2 FontOrigin = font.MeasureString(text) / 2;
                GameRef.spriteBatch.Draw(Game.Content.Load<Texture2D>(@"Images/textBox"), new Vector2(textPos.X - 375, textPos.Y - 75),
                new Rectangle(0,
                400, 725, 200),
                Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                GameRef.spriteBatch.DrawString(font, text, textPos, Color.Black, 0, FontOrigin, 1.0f, SpriteEffects.None, 1f);
            }
            GameRef.spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {

            if (Player.CurrentHP <= 0)
            {
                GameRef.GameOver();
            }
            if (!textBox)
            {
                if (Enemies.Count == 2)
                {
                    SecondEnemyHealth.Text = Enemies[1].CurrentHP.ToString();
                }
                if (InputHandler.KeyPressed(Keys.Escape))
                {
                    RemoveCombatMenu();
                    AddCombatMenu();
                }
                ControlManager.Update(gameTime, playerIndexInControl);
                base.Update(gameTime);
            }
            if (textBox)
            {
                if (InputHandler.KeyPressed(Keys.X))
                {
                    EnemyMoves = "";
                    textBox = false;
                    RemoveCombatMenu();
                    AddCombatMenu();

                }
            }

            PlayerMana.Text = Player.CurrentMana.ToString();
            PlayerHealth.Text = Player.CurrentHP.ToString();
            EnemyHealth.Text = Enemies[0].CurrentHP.ToString();
            EnemyName.Text = Enemies[0].name;
            EnemyLevel.Text = Enemies[0].Level.ToString();
            enemyBackground.Image = Enemies[0].image;


        }
        protected override void LoadContent()
        {
            base.LoadContent();
            ContentManager Content = Game.Content;


            textSprite = Game.Content.Load<Texture2D>(@"Images/textBox");
            font = Game.Content.Load<SpriteFont>(@"Fonts/ControlFont");


            Background = new PictureBox(
                Content.Load<Texture2D>(@"Backgrounds\CombatBackground"),
                GameRef.ScreenRectangle);
            ControlManager.Add(Background);

            Texture2D arrow = Content.Load<Texture2D>(@"GUI\leftarrowUp");

            ArrowTexture = new PictureBox(
                arrow,
                new Rectangle(0, 0, arrow.Width, arrow.Height));
            ControlManager.Add(ArrowTexture);

            enemyBackground = new PictureBox(
                Enemies[0].image,
                new Rectangle(250, 250,
                    Enemies[0].image.Width * 3, Enemies[0].image.Height * 3));
            ControlManager.Add(enemyBackground);

            EnemyName = new Label();
            EnemyName.Text = Enemies[0].name;
            EnemyName.Position = new Vector2(250, 220);
            ControlManager.Add(EnemyName);

            EnemyHealthLabel = new Label();
            EnemyHealthLabel.Text = "Health:";
            EnemyHealthLabel.Position = new Vector2(250, 440);
            EnemyHealthLabel.Size = EnemyHealthLabel.SpriteFont.MeasureString(EnemyHealthLabel.Text);
            ControlManager.Add(EnemyHealthLabel);

            EnemyHealth = new Label();
            EnemyHealth.Position = new Vector2(250 + EnemyHealthLabel.Size.X, 440);
            EnemyHealth.Text = Enemies[0].CurrentHP.ToString();
            ControlManager.Add(EnemyHealth);

            EnemyLevelLabel = new Label();
            EnemyLevelLabel.Text = "Level: ";
            EnemyLevelLabel.Size = EnemyLevelLabel.SpriteFont.MeasureString(EnemyLevelLabel.Text);
            EnemyLevelLabel.Position = new Vector2(250, 465);
            ControlManager.Add(EnemyLevelLabel);

            EnemyLevel = new Label();
            EnemyLevel.Text = Enemies[0].Level.ToString();
            EnemyLevel.Position = new Vector2(250 + EnemyLevelLabel.Size.X, 465);
            ControlManager.Add(EnemyLevel);

            if (Enemies.Count() > 1)
            {
                SecondEnemyBackground = new PictureBox(
                    Enemies[1].image,
                     new Rectangle(600, 250,
                     Enemies[1].image.Width * 3, Enemies[1].image.Height * 3));
                ControlManager.Add(SecondEnemyBackground);

                SecondEnemyName = new Label();
                SecondEnemyName.Text = Enemies[1].name;
                SecondEnemyName.Position = new Vector2(600, 220);
                ControlManager.Add(SecondEnemyName);

                SecondEnemyHealthLabel = new Label();
                SecondEnemyHealthLabel.Text = "Health:";
                SecondEnemyHealthLabel.Position = new Vector2(600, 440);
                SecondEnemyHealthLabel.Size = EnemyHealthLabel.SpriteFont.MeasureString(EnemyHealthLabel.Text);
                ControlManager.Add(SecondEnemyHealthLabel);

                SecondEnemyHealth = new Label();
                SecondEnemyHealth.Position = new Vector2(600 + EnemyHealthLabel.Size.X, 440);
                SecondEnemyHealth.Text = Enemies[1].CurrentHP.ToString();
                ControlManager.Add(SecondEnemyHealth);

                SecondEnemyLevelLabel = new Label();
                SecondEnemyLevelLabel.Text = "Level: ";
                SecondEnemyLevelLabel.Size = EnemyLevelLabel.SpriteFont.MeasureString(EnemyLevelLabel.Text);
                SecondEnemyLevelLabel.Position = new Vector2(600, 465);
                ControlManager.Add(SecondEnemyLevelLabel);

                SecondEnemyLevel = new Label();
                SecondEnemyLevel.Text = Enemies[1].Level.ToString();
                SecondEnemyLevel.Position = new Vector2(600 + EnemyLevelLabel.Size.X, 465);
                ControlManager.Add(SecondEnemyLevel);
            }


            PlayerName = new Label();
            PlayerName.Position = new Vector2(300, 800);
            PlayerName.Text = Player.name;
            ControlManager.Add(PlayerName);

            Label LevelLabel = new Label();
            LevelLabel.Text = "Level:";
            LevelLabel.Size = LevelLabel.SpriteFont.MeasureString(LevelLabel.Text);
            LevelLabel.Position = new Vector2(300, 855);
            ControlManager.Add(LevelLabel);

            Label PlayerHealthLabel = new Label();
            PlayerHealthLabel.Text = "Health:";
            PlayerHealthLabel.Position = new Vector2(300, 830);
            PlayerHealthLabel.Size = PlayerHealthLabel.SpriteFont.MeasureString(PlayerHealthLabel.Text);
            ControlManager.Add(PlayerHealthLabel);

            PlayerHealth = new Label();
            PlayerHealth.Position = new Vector2(300 + PlayerHealthLabel.Size.X, 830);
            PlayerHealth.Text = Player.CurrentHP.ToString();
            ControlManager.Add(PlayerHealth);

            PlayerLevel = new Label();
            PlayerLevel.Position = new Vector2(300 + LevelLabel.Size.X, 855);
            PlayerLevel.Text = Player.Level.ToString();
            ControlManager.Add(PlayerLevel);

            Label ManaLabel = new Label();
            ManaLabel.Text = "Mana: ";
            ManaLabel.Size = ManaLabel.SpriteFont.MeasureString(ManaLabel.Text);
            ManaLabel.Position = new Vector2(300, 880);
            ControlManager.Add(ManaLabel);

            PlayerMana = new Label();
            PlayerMana.Position = new Vector2(300 + ManaLabel.Size.X, 880);
            PlayerMana.Text = Player.CurrentMana.ToString();
            ControlManager.Add(PlayerMana);

            PlayerTurn();
        }
        private void PlayerTurn()
        {
            if (Player.CurrentStatus != null)
                Player.CurrentStatus.apply();

            Attack = new LinkLabel();
            Attack.Text = "Attack";
            Attack.Size = Attack.SpriteFont.MeasureString(Attack.Text);
            Attack.Selected += attackLabel_Selected;

            Special = new LinkLabel();
            Special.Text = new AttackMove().GetName(Player);
            Special.Size = Special.SpriteFont.MeasureString(Special.Text);
            Special.Selected += new EventHandler(attackLabel_Selected);

            Items = new LinkLabel();
            Items.Text = "Items";
            Items.Size = Items.SpriteFont.MeasureString(Items.Text);
            Items.Selected += new EventHandler(Items_Selected);

            Run = new LinkLabel();
            Run.Text = "Run";
            Run.Size = Run.SpriteFont.MeasureString(Run.Text);
            Run.Selected += new EventHandler(Run_Selected);

            ControlManager.Add(Attack);
            ControlManager.Add(Special);
            ControlManager.Add(Items);
            ControlManager.Add(Run);

            ControlManager.NextControl();

            ControlManager.FocusedChanged += new EventHandler(ControlManager_FocusedChanged);

            Vector2 position = new Vector2(600, 750);

            int i = 0;
            foreach (Control c in ControlManager)
            {
                if (c is LinkLabel)
                {

                    if (c.Size.X > MaxItemWidth)
                        maxItemWidth = c.Size.X;

                    if (i == 0)
                    {
                        c.Position = position;
                        position.Y += c.Size.Y + 5f;
                    }

                    if (i % 2 != 0)
                    {
                        c.Position = position;
                        position.X += Special.Size.X + 50f;
                        position.Y -= c.Size.Y + 5f;
                    }
                    else
                    {
                        c.Position = position;
                        position.Y += c.Size.Y + 5f;
                    }
                    i++;
                }

            }
            ControlManager_FocusedChanged(Attack, null);


        }
        public void ControlManager_FocusedChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Vector2 position = new Vector2(control.Position.X + control.Size.X + 5f,
                control.Position.Y + 10f);
            ArrowTexture.SetPosition(position);
        }
        private void Run_Selected(object sender, EventArgs e)
        {
            Run.HasFocus = false;
            if (!textBox)
            {
                if (Player.Speed > Enemies[0].Speed && Player.Speed > Enemies[1].Speed)
                {
                    GameRef.ContinueGame(false);
                }
                else
                {
                    EnemyTurn();
                }
            }
        }
        private void attackLabel_Selected(object sender, EventArgs e)
        {
            if (!textBox)
            {
                LinkLabel temp = sender as LinkLabel;

                isSpecialAttack = (temp.Text == "Attack") ? false : true;
                if (isSpecialAttack)
                {
                    Special.HasFocus = false;
                }
                else
                {
                    Attack.HasFocus = false;
                }
                if (isSpecialAttack && (int.Parse(PlayerMana.Text) <= 0))
                {
                    showDialog(OutOfMana);
                    RemoveCombatMenu();
                    PlayerTurn();
                }
                if (!textBox)
                {
                    sb = new StringBuilder("You gained the following items:" + "\n");

                    firstEnemy = new LinkLabel();
                    firstEnemy.Selected += Enemy_Selected;
                    firstEnemy.Text = EnemyName.Text;
                    firstEnemy.Size = firstEnemy.SpriteFont.MeasureString(firstEnemy.Text);
                    firstEnemy.Position = EnemyName.Position;
                    ControlManager.Add(firstEnemy);

                    if (Enemies.Count > 1)
                    {
                        SecondEnemy = new LinkLabel();
                        SecondEnemy.Text = SecondEnemyName.Text;
                        SecondEnemy.Position = SecondEnemyName.Position;
                        SecondEnemy.Size = SecondEnemy.SpriteFont.MeasureString(SecondEnemy.Text);
                        SecondEnemy.Selected += Enemy_Selected;
                        ControlManager.Add(SecondEnemy);

                        RemoveCombatMenu();

                        firstEnemy.HasFocus = true;
                        ControlManager_FocusedChanged(firstEnemy, null);
                    }
                    else
                    {
                        DamageEnemy(Enemies[0]);
                        RemoveCombatMenu();
                        EnemyTurn();
                    }

                }
            }
        }
        private void RemoveCombatMenu()
        {


            FakeAttack = new Label();
            FakeAttack.Text = "Attack";
            FakeAttack.Size = FakeAttack.SpriteFont.MeasureString(FakeAttack.Text);
            FakeAttack.Position = Attack.Position;

            SpecialLabel = new Label();
            SpecialLabel.Text = Special.Text;
            SpecialLabel.Size = SpecialLabel.SpriteFont.MeasureString(SpecialLabel.Text);
            SpecialLabel.Position = Special.Position;

            ItemsLabel = new Label();
            ItemsLabel.Text = "Items";
            ItemsLabel.Size = ItemsLabel.SpriteFont.MeasureString(ItemsLabel.Text);
            ItemsLabel.Position = Items.Position;

            RunLabel = new Label();
            RunLabel.Text = "Run";
            RunLabel.Size = RunLabel.SpriteFont.MeasureString(RunLabel.Text);
            RunLabel.Position = Run.Position;

            ControlManager.Add(FakeAttack);
            ControlManager.Add(SpecialLabel);
            ControlManager.Add(ItemsLabel);
            ControlManager.Add(RunLabel);

            ControlManager.Remove(Attack);
            ControlManager.Remove(Special);
            ControlManager.Remove(Items);
            ControlManager.Remove(Run);

        }

        public void Items_Selected(object sender, EventArgs e)
        {
            Items.HasFocus = false;
            if (!textBox)
            {
                GameRef.ShowInventory(true);
            }
        }
        private void Enemy_Selected(object sender, EventArgs e)
        {
            if (!textBox)
            {
                LinkLabel temp = sender as LinkLabel;
                if (temp.Position == EnemyName.Position)
                {
                    DamageEnemy(Enemies[0]);
                    firstEnemy.HasFocus = false;
                }
                else
                {
                    DamageEnemy(Enemies[1]);
                    SecondEnemy.HasFocus = false;
                }
                EnemyTurn();
            }
        }
        public void EnemyTurn()
        {
            if (!textBox)
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    if (Enemies[i].CurrentHP > 0)
                    {
                        EnemyAttack(Enemies[i]);
                    }
                }

                //firstEnemy.Selected -= Enemy_Selected;
                //ControlManager.Remove(firstEnemy);
                //if (Enemies.Count > 1)
                //{
                //    SecondEnemy.Selected -= Enemy_Selected;
                //    ControlManager.Remove(SecondEnemy);
                //}
                EnemyMoves += "Press X to continue";
                showDialog(EnemyMoves);
            }

        }
        private void AddCombatMenu()
        {
            ControlManager.Add(Attack);
            ControlManager.Add(Special);
            ControlManager.Add(Items);
            ControlManager.Add(Run);

            ControlManager.Remove(FakeAttack);
            ControlManager.Remove(SpecialLabel);
            ControlManager.Remove(ItemsLabel);
            ControlManager.Remove(RunLabel);

            ControlManager.Remove(firstEnemy);
            ControlManager.Remove(SecondEnemy);

            Attack.HasFocus = true;

            ControlManager_FocusedChanged(Attack, null);
        }
        private void EnemyDeath(Enemy e)
        {
            Player.exp += e.EXPGiven;
            enemyDrops.AddRange(e.Drops);

            ControlManager.Remove(SecondEnemyBackground);
            ControlManager.Remove(SecondEnemyHealth);
            ControlManager.Remove(SecondEnemyHealthLabel);
            ControlManager.Remove(SecondEnemyName);
            ControlManager.Remove(SecondEnemyLevel);
            ControlManager.Remove(SecondEnemyLevelLabel);

            Player.AddItems(enemyDrops);
            Player.Money += totalGold;
            foreach (Item item in enemyDrops)
            {
                sb.Append(item.ToString() + "\n");
            }
            sb.Append("\nGold: " + totalGold);

            if (Enemies.Count == 1)
            {
                bool isLeveled = false;
                while (Player.exp >= Player.EXPToNext)
                {
                    Player.levelUp();
                    isLeveled = true;
                }
                EndCombat(isLeveled);
            }

            if (Enemies.Count > 1 && e == Enemies[0])
            {
                Enemies[0] = Enemies[1];
            }
            if (Enemies.Count != 1)
            {
                Enemies.Remove(Enemies[1]);
            }




        }
        private void DamageEnemy(Enemy e)
        {
            int damage = 1;
            if (isSpecialAttack)
            {
                Ability randomAblilty = Player.GetAbility();
                damage = (int)(randomAblilty.Effect(Player) - (e.SpecDefense * e.SP_DEF_MOD));
                Player.CurrentMana -= randomAblilty.getCost(Player);
            }
            else
            {
                if (((Player.Atk + 1) - e.Defense) > 0)
                {
                    damage = (int)((Player.getDamage()) - (e.Defense * e.DEF_MOD));
                }
            }

            e.CurrentHP -= damage;
            if (e.CurrentHP <= 0)
            {
                EnemyDeath(e);
            }

        }
        private void EnemyAttack(Enemy e)
        {
            bool isSpecial;
            int damage = e.GetAttack(out isSpecial);
            Status s = null;
            bool gotStatus = e.GetStatus(out s);

            if (gotStatus)
            {
                s.player = Player;
                Player.CurrentStatus = s;
            }
            int damageTaken = Player.getAttacked(damage);

            if (Player.CurrentHP <= 0)
            {
                GameRef.GameOver();
            }

            EnemyMoves += e.name + " attacked you for " + damageTaken.ToString() + " damage.\n";

        }
        public void showDialog(string text)
        {
            textBox = true;
            this.text = text;
        }
        public void EndCombat(bool isLeveled)
        {
            if (Enemies[0].name == "King James")
            {
                GameRef.Win();
            }
            else
            {
                GameRef.ShowGainedItems(sb.ToString(), isLeveled);
            }
        }
    }

}
