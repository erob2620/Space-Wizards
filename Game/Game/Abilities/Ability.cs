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


namespace spacewizards.Abilities
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Ability
    {
        public virtual string GetName(PlayerCharacter pc)
        {
            return null;
        }

        public virtual int getCost(PlayerCharacter pc)
        {
            return 0;
        }
        public virtual int Effect(PlayerCharacter pc)
        {
            return 0;
        }
    }
}
