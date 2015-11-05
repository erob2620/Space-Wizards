using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using spacewizards.Models;
using spacewizards.Models.Monsters;


namespace spacewizards
{
    public class UserControlledSprite : Sprite
    {
        public static Random rand = new Random();
        public PlayerCharacter Character;
        public Mapper mapper { get; set; }
        Game1 GameRef;
        enum LastPressedDirection { left, right, up, down };
        public Vector2 baseSpeed;
        public Vector2 halfSpeed;
        LastPressedDirection lastPressedDirection = LastPressedDirection.up;
        public Rectangle interactRect = new Rectangle(0, 0, 1, 1);
        public Rectangle interactRect_
        {
            get
            {
                return interactRect;
            }
            set
            {
                interactRect = value;
            }
        }

        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, PlayerCharacter pc, Game1 game, Mapper mapperInput, float zIndex = .1f)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed)
        {
            this.zIndex = zIndex;
            Character = pc;
            GameRef = game;
            this.mapper = mapperInput;
            baseSpeed = speed;
            halfSpeed = speed / 2;
        }

        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame)
        {

        }



        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;
                //if ((Keyboard.GetState().GetPressedKeys(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Down)) ||
                //    (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Left)) ||
                //    (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Right)) ||
                //    (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Left)) ||
                //    (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Right)) ||
                //    (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Right)))
                //{
                //    this.speed = new Vector2((speed.X) / 2, (speed.Y) / 2);
                //}
                //else
                //{
                //    this.speed = baseSpeed;
                //}

                if (Keyboard.GetState().GetPressedKeys().Count() >= 2)
                {
                    this.speed = halfSpeed;
                }
                else
                {
                    this.speed = baseSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    inputDirection.X -= 1.5f;
                    lastPressedDirection = LastPressedDirection.left;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    inputDirection.X += 1.5f;
                    lastPressedDirection = LastPressedDirection.right;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    inputDirection.Y -= 1.5f;
                    lastPressedDirection = LastPressedDirection.up;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    inputDirection.Y += 1.5f;
                    lastPressedDirection = LastPressedDirection.down;
                }
                if (InputHandler.KeyPressed(Keys.I))
                {
                    GameRef.ShowInventory(false);
                }
                if (InputHandler.KeyPressed(Keys.C))
                {
                    GameRef.ShowStats(false);
                }
                if (InputHandler.KeyPressed(Keys.Space) || InputHandler.KeyPressed(Keys.Enter))
                {
                    if (lastPressedDirection == LastPressedDirection.up)
                    {
                        interactRect = new Rectangle(((int)position.X + frameSize.X / 2), ((int)position.Y - frameSize.Y / 2), 5, frameSize.Y);
                    }
                    else if (lastPressedDirection == LastPressedDirection.down)
                    {
                        interactRect = new Rectangle((((int)position.X + frameSize.X / 2)), ((int)position.Y + frameSize.Y / 2), 5, frameSize.Y);
                    }
                    else if (lastPressedDirection == LastPressedDirection.right)
                    {
                        interactRect = new Rectangle(((int)position.X + frameSize.X / 2), ((int)position.Y + frameSize.Y / 2), frameSize.X * 2, 5);
                    }
                    else if (lastPressedDirection == LastPressedDirection.left)
                    {
                        interactRect = new Rectangle((((int)position.X - frameSize.X / 2)), ((int)position.Y + frameSize.Y / 2), frameSize.X, 5);
                    }
                }
                inputDirection *= speed;
                Vector2 destination = inputDirection + position;

                if (inputDirection.X != 0 || inputDirection.Y != 0)
                    Encounter();

                return inputDirection;
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //move the sprite based on direction
            position += direction;

            //reset the interaction box
            //Rectangle interactRect = new Rectangle((int)position.X, (int)position.Y, 1, 1);

            //if sprite is off-screen, move it back into the game window
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Height - frameSize.Y;

            base.Update(gameTime, clientBounds);
        }
        public void Encounter()
        {
            Character.incrementEncounter();

            int i = rand.Next(1, 101);
            if (i < Character.EncounterChance)
            {
                GameRef.StartCombat();
                Character.EncounterChance = 0;
            }
        }
    }
}
