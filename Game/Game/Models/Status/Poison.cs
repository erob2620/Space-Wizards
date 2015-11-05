using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacewizards.Models.Statuses
{
    public class Poison : Status
    {
        public Poison()
        {
            Name = "PSN";
            durationLeft = 3;
        }
        public override void apply()
        {
            if (durationLeft == 0)
            {
                Remove();
            }
            else if (player != null)
            {
                player.CurrentHP -= (player.Level / 3) + 1;
                durationLeft--;
            }
        }

        public override void Remove()
        {
            if (player != null)
                player.CurrentStatus = null;
        }
    }
}
