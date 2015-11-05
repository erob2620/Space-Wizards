using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spacewizards.Models.Statuses
{
    public class DefenceBreak : Status
    {
        int currentDef;
        public DefenceBreak()
        {
            currentDef = 0;
            Name = "-DEF ";
            keepApplying = true;

        }
        public override void apply()
        {
            if (durationLeft < 1)
            {
                Remove();
            }
            else if (keepApplying)
            {
                if (player != null)
                {
                    currentDef = player.Defense;
                    player.Defense = player.Defense / 2;
                    keepApplying = false;
                    durationLeft--;
                }

            }



        }

        public override void Remove()
        {
            player.Defense = currentDef;
            player.CurrentStatus = null;
        }
    }
}
