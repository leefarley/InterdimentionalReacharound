using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InterdimentionalReacharound.Control
{

    public class Controller : IControl
    {
        private PlayerIndex _playerIndex;
        private GamePadState _gamePadState;

        public Controller(PlayerIndex playerIndex)
        {
            _playerIndex = playerIndex;
        }
        public void UpdateContolState()
        {
            _gamePadState = GamePad.GetState(_playerIndex);
        }

        public bool IsJumpPressed()
        {
            if (_gamePadState.Buttons.A == ButtonState.Pressed)
                return true;
            return false;
        }

        public float GetVelocty()
        {
            return _gamePadState.ThumbSticks.Left.X;
        }
    }
}
