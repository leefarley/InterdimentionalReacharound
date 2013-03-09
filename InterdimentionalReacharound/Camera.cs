using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InterdimentionalReacharound
{
    public class Camera
    {
        public Vector2 Position { get; set; }
        public Rectangle CameraBounds { get; set; }
        //Matrix viewMatrix;
        //float scale = 1.0f;
        //float rotation = 0.0f;

        public Camera(Rectangle cameraBounds)
        {
            CameraBounds = cameraBounds;
        }

        public void Update(Vector2 playerPosition, Viewport view)
        {
            Vector2 position = Vector2.Zero;
            position.X = ((playerPosition.X) - (view.Width / 2));
            position.Y = ((playerPosition.Y) - (view.Height / 2));

            if (position.X < 0)
                position.X = 0;
            else if ((position.X + view.Width) > CameraBounds.Right)
                position.X = CameraBounds.Right - view.Width;

            if (position.Y < 0)
                position.Y = 0;
            else if ((position.Y + view.Y) > CameraBounds.Bottom)
                position.Y = CameraBounds.Bottom - view.Height;


            Position = position;
        }
    }
}
