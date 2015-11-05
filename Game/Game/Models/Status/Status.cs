using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacewizards.Models
{
    public abstract class Status
    {
        public string Name { get; set; }
        protected bool keepApplying;
        protected int durationLeft;
        public abstract void apply();
        public abstract void Remove();
        public Status(){}
        public PlayerCharacter player;

    
    }
}
