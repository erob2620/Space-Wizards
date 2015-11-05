using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace spacewizards.Controls
{
    public class ControlManager : List<Control>
    {
        public int selectedControl = 0;

        private static SpriteFont spriteFont;

        public static SpriteFont SpriteFont
        {
            get { return spriteFont; }
        }

        public ControlManager(SpriteFont spriteFont)
        {
            ControlManager.spriteFont = spriteFont;
        }
        public ControlManager(SpriteFont spriteFont, int capacity)
            : base(capacity)
        {
            ControlManager.spriteFont = spriteFont;
        }
        public ControlManager(SpriteFont spriteFont, IEnumerable<Control> controls)
            : base(controls)
        {
            ControlManager.spriteFont = spriteFont;
        }
        public void Update(GameTime gameTime, PlayerIndex playerIndex)
        {
            if (Count != 0)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].Enabled)
                    {
                        this[i].Update(gameTime);
                    }
                    if (this[i].HasFocus)
                    {
                        this[i].HandleInput(playerIndex);
                    }
                }
                if (InputHandler.KeyPressed(Keys.Up) || InputHandler.KeyPressed(Keys.W))
                {
                    PreviousControl();
                }
                if (InputHandler.KeyPressed(Keys.Down) || InputHandler.KeyPressed(Keys.S))
                {
                    NextControl();

                }
                if (InputHandler.KeyPressed(Keys.Left) || InputHandler.KeyPressed(Keys.A))
                {
                    int currentControl = PreviousControl();

                    if ((selectedControl == currentControl - 1))
                    {
                        currentControl = PreviousControl();
                        if (currentControl == selectedControl - 1)
                        {
                            NextControl();
                        }
                    }

                }
                if (InputHandler.KeyPressed(Keys.Right) || InputHandler.KeyPressed(Keys.D))
                {
                    int currentControl = NextControl();
                    if ((selectedControl == currentControl + 1))
                    {
                        currentControl = NextControl();
                        if (currentControl == selectedControl + 1)
                        {
                            PreviousControl();
                        }
                    }
                }

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control c in this)
            {
                if (c.Visible)
                {
                    c.Draw(spriteBatch);
                }
            }
        }
        public event EventHandler FocusedChanged;

        public int NextControl()
        {
            int currentControl = 1;
            if (selectedControl >= Count)
            {
                selectedControl = 1;
            }
            if (Count != 0)
            {
                currentControl = selectedControl;
                this[selectedControl].HasFocus = false;

                do
                {
                    selectedControl++;

                    if (selectedControl == Count)
                        selectedControl = 0;

                    if (this[selectedControl].TabStop && this[selectedControl].Enabled && this[selectedControl].Text != "-")
                    {
                        if (FocusedChanged != null)
                            FocusedChanged(this[selectedControl], null);
                        break;
                    }
                } while (currentControl != selectedControl);


                this[selectedControl].HasFocus = true;
            }
            return currentControl;
        }
        public int PreviousControl()
        {
            int currentControl = 1;
            if (selectedControl >= Count)
            {
                selectedControl = 1;
            }
            if (Count != 0)
            {
                currentControl = selectedControl;

                this[selectedControl].HasFocus = false;

                do
                {
                    selectedControl += -1;

                    if (selectedControl < 0)
                        selectedControl = Count - 1;

                    if (this[selectedControl].TabStop && this[selectedControl].Enabled && this[selectedControl].Text != "-")
                    {
                        if (FocusedChanged != null)
                            FocusedChanged(this[selectedControl], null);

                        break;
                    }
                } while (currentControl != selectedControl);

                this[selectedControl].HasFocus = true;
            }
            return currentControl;
        }
    }
}
