using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssaultBooming.Grid
{
    public class GridSystem
    {
        private int width;
        private int height;
        private float cellSize;

        public GridSystem(int _width, int _height, float _cellSize)
        {
            this.width = _width;
            this.height = _height;
            this.cellSize = _cellSize;

            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _height; z++)
                {
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * 0.2f, Color.white, 1000);
                }
            }
        }

        public Vector3 GetWorldPosition(int x, int z)
        {
            return new Vector3(x, 0, z) * cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(
                Mathf.RoundToInt(worldPosition.x / cellSize),
                Mathf.RoundToInt(worldPosition.z / cellSize)
            );
        }
    }
}

