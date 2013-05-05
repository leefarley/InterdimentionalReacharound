using Microsoft.Xna.Framework;

namespace InterdimentionalReacharound.Control
{
    public interface IControl
    {
        void UpdateContolState();
        bool IsJumpPressed();
        float GetVelocty();

    }
}
