using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using spacewizards.Controls;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewizards.GameScreens
{
    public abstract partial class BaseGameState : GameState
    {
        protected Game1 GameRef;
        protected ControlManager ControlManager;
        protected PlayerIndex playerIndexInControl;

        public BaseGameState(Game game, GameStateManeger manager) : base(game, manager)
        {
            GameRef = (Game1)game;

            playerIndexInControl = PlayerIndex.One;

        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected override void LoadContent()
        {
            ContentManager Content = Game.Content;
            
            SpriteFont menuFont = Content.Load<SpriteFont>(@"Fonts\ControlFont");
            ControlManager = new ControlManager(menuFont);
            base.LoadContent();
        }
    }
}
