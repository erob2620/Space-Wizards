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


namespace spacewizards
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract partial class GameState : DrawableGameComponent
    {
        protected Game game { get; set; }
        public Song Song { get; set; }
        public bool changeMusic = true;
        private List<GameComponent> childComponents;
        public List<GameComponent> Components
        {
            get { return childComponents; }
        }

        private GameState tag;
        public GameState Tag
        {
            get { return tag; }
        }
        protected GameStateManeger StateManeger;

        public GameState(Game game, GameStateManeger _stateManeger)
            : base(game)
        {
            StateManeger = _stateManeger;
            childComponents = new List<GameComponent>();
            tag = this;
            this.game = game;
            Song = game.Content.Load<Song>("Music/Vogel im Kafig");

        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            foreach (GameComponent gc in childComponents)
            {
                if (gc.Enabled)
                {
                    gc.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent drawComponent;
            foreach (GameComponent component in childComponents)
            {
                if (component is DrawableGameComponent)
                {
                    drawComponent = component as DrawableGameComponent;
                    if (drawComponent.Visible)
                    {
                        drawComponent.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }
        internal protected virtual void StateChange(object sender, EventArgs e)
        {
            if (StateManeger.CurrentState == Tag)
                Show();
            else
                Hide();
        }
        protected virtual void Show()
        {
            Visible = true;
            Enabled = true;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = true;
                }
            }
        }
        protected virtual void Hide()
        {
            Visible = false;
            Enabled = false;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }
    }
}
