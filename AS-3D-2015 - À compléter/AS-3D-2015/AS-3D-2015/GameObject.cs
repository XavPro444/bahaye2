using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
    class GameObject
    {
        public Model model = null;
        public Vector3 postion = Vector3.Zero;
        public Vector3 rotation = Vector3.Zero;
        public float scale = 1.0f;
        public Vector3 velocité = Vector3.Zero;
        public bool alive = false;
    }
}
