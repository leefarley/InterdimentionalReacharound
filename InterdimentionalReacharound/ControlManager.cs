using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterdimentionalReacharound
{
    public static class ControlManager
    {
        public static bool HorizontialMovement(GamePadState gamePadState)
        {
            if (gamePadState.ThumbSticks.Left.X != 0)
            {
                return true;
            }
            return false;
        }
    }
}
