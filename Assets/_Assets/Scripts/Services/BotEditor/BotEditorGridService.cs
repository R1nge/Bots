using _Assets.Scripts.Gameplay.Parts;
using UnityEngine;

namespace _Assets.Scripts.Services.BotEditor
{
    public class BotEditorGridService
    {
        private readonly int _cellSize = 1;
        private GridCell[,,] _XYZGrid;

        public void Init(int gridSize)
        {
            _XYZGrid = new GridCell[gridSize, gridSize, gridSize];

            int offset = gridSize / 2;

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    for (int z = 0; z < gridSize; z++)
                    {
                        // Calculate the actual position accounting for the offset
                        Vector3 position = new Vector3(
                            (x - offset) * _cellSize,
                            (y - offset) * _cellSize,
                            (z - offset) * _cellSize);

                        // Initialize each GridCell with the calculated position
                        _XYZGrid[x, y, z] = new GridCell
                        {
                            position = position,
                            botPart = null // Or the appropriate default value for botPart
                        };
                    }
                }
            }
        }

        public GridCell GetGridCell(Vector3 position)
        {
            var x = Mathf.FloorToInt(position.x / _cellSize) + _XYZGrid.GetLength(0) / 2;
            var y = Mathf.FloorToInt(position.y / _cellSize) + _XYZGrid.GetLength(1) / 2;
            var z = Mathf.FloorToInt(position.z / _cellSize) + _XYZGrid.GetLength(2) / 2;
    
            // Check if the indices are within the bounds of the grid
            if (x >= 0 && x < _XYZGrid.GetLength(0) &&
                y >= 0 && y < _XYZGrid.GetLength(1) &&
                z >= 0 && z < _XYZGrid.GetLength(2))
            {
                Debug.Log("X: " + x + " Y: " + y + " Z: " + z);
                return _XYZGrid[x, y, z];
            }
            else
            {
                Debug.LogError("Position out of range: " + position);
                return null; // or handle the error as appropriate
            }
        }
    }

    public class GridCell
    {
        public Vector3 position;
        public BotPart botPart;
    }
}