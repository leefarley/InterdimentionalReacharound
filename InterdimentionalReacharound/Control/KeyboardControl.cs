using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InterdimentionalReacharound.Control
{
    public class KeyboardControl : IControl
    {
        private PlayerIndex _playerIndex;
        private KeyboardState _keyboardState;

        public KeyboardControl(PlayerIndex playerIndex)
        {
            _playerIndex = playerIndex;
        }
        public void UpdateContolState()
        {
            _keyboardState = Keyboard.GetState(_playerIndex);
        }

        public bool IsJumpPressed()
        {
            return _keyboardState.IsKeyDown(Keys.Space);
        }

        public float GetVelocty()
        {
            if (_keyboardState.IsKeyDown(Keys.Right) && _keyboardState.IsKeyUp(Keys.Left))
                return 1;
            if (_keyboardState.IsKeyDown(Keys.Left) && _keyboardState.IsKeyUp(Keys.Right))
                return -1;
            return 0;
        }
    }
}
