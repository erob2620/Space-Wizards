using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using spacewizards.Controls;
using spacewizards.Models;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace spacewizards.GameScreens
{
    public class CharacterStats : BaseGameState
    {
        PlayerCharacter Player;
        public Label CharacterName;
        public Label PlayerHealth;
        public Label PlayerAttack;
        public Label PlayerDefense;
        public Label PlayerSpecialAtk;
        public Label PlayerSpecialDef;
        public Label PlayerSpeed;

        public bool fromCombat;

        private float maxItemWidth = 0f;
        public float MaxItemWidth
        {
            get { return maxItemWidth; }
            set { maxItemWidth = value; }
        }

        public LinkLabel ContinueGame;
        public LinkLabel QuitGame;

        public PictureBox Background;
        public PictureBox ArrowTexture;

        public CharacterStats(Game1 game, GameStateManeger manager, PlayerCharacter player, bool fromCombat)
            : base(game, manager)
        {
            Player = player;
            this.fromCombat = fromCombat;
        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            base.Draw(gameTime);
            ControlManager.Draw(GameRef.spriteBatch);
            GameRef.spriteBatch.End();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            ContentManager Content = Game.Content;

            Background = new PictureBox(Content.Load<Texture2D>(@"Backgrounds\MenuBackground"), GameRef.ScreenRectangle);
            ControlManager.Add(Background);

            Texture2D arrow = Content.Load<Texture2D>(@"GUI\leftarrowUp");

            ArrowTexture = new PictureBox(
                arrow,
                new Rectangle(0, 0, arrow.Width, arrow.Height));
            ControlManager.Add(ArrowTexture);

            Label NameLabel = new Label();
            NameLabel.Text = "Name: ";
            NameLabel.Position = new Vector2(200, 200);
            NameLabel.Size = NameLabel.SpriteFont.MeasureString(NameLabel.Text);
            ControlManager.Add(NameLabel);

            CharacterName = new Label();
            CharacterName.Text = Player.name;
            CharacterName.Position = new Vector2(200 + NameLabel.Size.X, 200);
            ControlManager.Add(CharacterName);

            Label HealthLabel = new Label();
            HealthLabel.Text = "Health: ";
            HealthLabel.Size = HealthLabel.SpriteFont.MeasureString(HealthLabel.Text);
            HealthLabel.Position = new Vector2(200, 250);
            ControlManager.Add(HealthLabel);

            PlayerHealth = new Label();
            PlayerHealth.Text = Player.CurrentHP.ToString();
            PlayerHealth.Size = PlayerHealth.SpriteFont.MeasureString(PlayerHealth.Text);
            PlayerHealth.Position = new Vector2(200 + HealthLabel.Size.X, 250);
            ControlManager.Add(PlayerHealth);

            Label TotalHealth = new Label();
            TotalHealth.Text = " / " + Player.HP.ToString();
            TotalHealth.Position = new Vector2(PlayerHealth.Position.X + PlayerHealth.Size.X, 250);
            ControlManager.Add(TotalHealth);

            Label ManaLabel = new Label();
            ManaLabel.Text = "Mana: ";
            ManaLabel.Position = new Vector2(200, 300);
            ManaLabel.Size = ManaLabel.SpriteFont.MeasureString(ManaLabel.Text);
            ControlManager.Add(ManaLabel);

            Label PlayerMana = new Label();
            PlayerMana.Text = Player.CurrentMana.ToString();
            PlayerMana.Size = PlayerMana.SpriteFont.MeasureString(PlayerMana.Text);
            PlayerMana.Position = new Vector2(200 + ManaLabel.Size.X, 300);
            ControlManager.Add(PlayerMana);

            Label TotalMana = new Label();
            TotalMana.Text = " / " + Player.mana.ToString();
            TotalMana.Position = new Vector2(PlayerMana.Position.X + PlayerMana.Size.X, 300);
            ControlManager.Add(TotalMana);

            Label AttackLabel = new Label();
            AttackLabel.Text = "Attack: ";
            AttackLabel.Position = new Vector2(200, 350);
            AttackLabel.Size = AttackLabel.SpriteFont.MeasureString(AttackLabel.Text);
            ControlManager.Add(AttackLabel);

            PlayerAttack = new Label();
            PlayerAttack.Text = Player.Atk.ToString();
            PlayerAttack.Position = new Vector2(200 + AttackLabel.Size.X, 350);
            ControlManager.Add(PlayerAttack);

            Label DefenseLabel = new Label();
            DefenseLabel.Text = "Defense: ";
            DefenseLabel.Position = new Vector2(200, 400);
            DefenseLabel.Size = DefenseLabel.SpriteFont.MeasureString(DefenseLabel.Text);
            ControlManager.Add(DefenseLabel);

            PlayerDefense = new Label();
            PlayerDefense.Text = Player.Defense.ToString();
            PlayerDefense.Position = new Vector2(200 + DefenseLabel.Size.X, 400);
            ControlManager.Add(PlayerDefense);

            Label SpecialAtkLabel = new Label();
            SpecialAtkLabel.Text = "Special Attack: ";
            SpecialAtkLabel.Position = new Vector2(200, 450);
            SpecialAtkLabel.Size = SpecialAtkLabel.SpriteFont.MeasureString(SpecialAtkLabel.Text);
            ControlManager.Add(SpecialAtkLabel);

            PlayerSpecialAtk = new Label();
            PlayerSpecialAtk.Text = Player.SpecAttack.ToString();
            PlayerSpecialAtk.Position = new Vector2(200 + SpecialAtkLabel.Size.X, 450);
            ControlManager.Add(PlayerSpecialAtk);

            Label SpecialDefLabel = new Label();
            SpecialDefLabel.Text = "Special Defense: ";
            SpecialDefLabel.Position = new Vector2(200, 500);
            SpecialDefLabel.Size = SpecialDefLabel.SpriteFont.MeasureString(SpecialDefLabel.Text);
            ControlManager.Add(SpecialDefLabel);

            PlayerSpecialDef = new Label();
            PlayerSpecialDef.Text = Player.SpecDefense.ToString();
            PlayerSpecialDef.Position = new Vector2(200 + SpecialDefLabel.Size.X, 500);
            ControlManager.Add(PlayerSpecialDef);


            ContinueGame = new LinkLabel();
            ContinueGame.Text = "Continue Game";
            ContinueGame.Size = ContinueGame.SpriteFont.MeasureString(ContinueGame.Text);
            ContinueGame.Selected += new EventHandler(ContinueGame_Selected);
            ControlManager.Add(ContinueGame);

            QuitGame = new LinkLabel();
            QuitGame.Text = "Quit Game";
            QuitGame.Size = QuitGame.SpriteFont.MeasureString(QuitGame.Text);
            QuitGame.Selected += new EventHandler(QuitGame_Selected);
            ControlManager.Add(QuitGame);

            ControlManager.NextControl();

            ControlManager.FocusedChanged += new EventHandler(ControlManager_FocusedChanged);

            Vector2 position = new Vector2(200, 600);

            foreach (Control c in ControlManager)
            {
                if (c is LinkLabel)
                {

                    if (c.Size.X > MaxItemWidth)
                        maxItemWidth = c.Size.X;

                    c.Position = position;
                    position.Y += c.Size.Y + 5f;
                }

            }

            ControlManager_FocusedChanged(ContinueGame, null);
        }
        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, playerIndexInControl);

            if (InputHandler.KeyPressed(Keys.Escape) || InputHandler.KeyPressed(Keys.C))
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
            base.Update(gameTime);
        }
        public void ContinueGame_Selected(object sender, EventArgs e)
        {
            GameRef.ContinueGame(false);
        }
        public void QuitGame_Selected(object sender, EventArgs e)
        {
            Game.Exit();
        }
        public void ControlManager_FocusedChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Vector2 position = new Vector2(control.Position.X + control.Size.X + 5f,
                control.Position.Y + 10f);
            ArrowTexture.SetPosition(position);
        }
    }
}
