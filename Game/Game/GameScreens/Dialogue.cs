using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using spacewizards.Controls;
using Microsoft.Xna.Framework.Input;

namespace spacewizards.GameScreens
{
    public class DialogueWindow : Control
    {
        private Rectangle _dialogueScreen;
        public Rectangle DialogueScreen
        {
            get { return _dialogueScreen; }
            private set
            {
                _dialogueScreen = value;
            }
        }
        private Label _dialogue;
        public Label Dialogue
        {
            get { return _dialogue; }
            private set
            {
                _dialogue = value;
            }
        }
        public ControlManager Manager;
        public DialogueWindow(Rectangle dialogueScreen, Label dialogue, ControlManager manager)
        {
            _dialogueScreen = dialogueScreen;
            _dialogue = dialogue;
            _dialogue.Size = 
            _dialogue.Position = new Vector2((_dialogueScreen.X - 300),(_dialogueScreen.Y));
            _dialogue.Color = Color.LightGreen;
            Manager = manager;
            
        }
        public void Show()
        {
            Manager.Add(_dialogue);
            _dialogue.Visible = true;
            _dialogue.Enabled = true;
        }
        public void Hide()
        {
            Manager.Remove(_dialogue);
            _dialogue.Visible = false;
            _dialogue.Enabled = false;
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, text, position, color);

        }
        public override void Update(GameTime gameTime)
        {

        }
        public override void HandleInput(PlayerIndex playerIndex)
        {
            if (InputHandler.KeyPressed(Keys.A))
                Hide();
        }
    }

}
