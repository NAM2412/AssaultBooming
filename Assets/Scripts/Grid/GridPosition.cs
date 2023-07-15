using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssaultBooming.Grid
{
    public struct GridPosition
    {
        public int x;
        public int z;

        public GridPosition(int _x, int _z)
        {
            this.x = _x;
            this.z = _z;
        }

        public override string ToString()
        {
            return $"x: {x}, z: {z}";
        }
    }
}

