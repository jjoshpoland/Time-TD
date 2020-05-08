using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace TD.Maps
{
    public class Map : MonoBehaviour
    {
        [SerializeField]
        int Size;
        [SerializeField]
        int cellSize;
        [SerializeField]
        Canvas GridCanvas;

        public Text CellTextLabel;
        public Cell CellPrefab;


        public int CellSize
        {
            get
            {
                return cellSize;
            }
            private set
            {
                cellSize = value;
            }
        }

        public bool GridGenerated
        {
            get
            {
                return grid != null;
            }
            private set
            {
                return;
            }
        }

        Cell[,] grid;

        // Start is called before the first frame update
        void Start()
        {
            GenerateGrid();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Instantiate a new grid of cells in a square, taking their size into account for cell placement. Then recalculates the placement of all environment objects.
        /// </summary>
        public void GenerateGridInEditor()
        {
            DestroyGridInEditor();

            grid = new Cell[Size, Size];

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    CreateCell(x, y);
                }
            }

            GameObject[] envObjects = GameObject.FindGameObjectsWithTag("Environment");

            for (int i = 0; i < envObjects.Length; i++)
            {
                SnapToMap snappableObject = envObjects[i].GetComponent<SnapToMap>();
                if(snappableObject != null)
                {
                    snappableObject.OccupyCell();
                }
            }
        }

        //TODO: generate in play mode that calls the destroy in play mode first instead of destroy in editor mode
        public void GenerateGrid()
        {
            DestroyGrid();
            grid = new Cell[Size, Size];

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    CreateCell(x, y);
                }
            }

            GameObject[] envObjects = GameObject.FindGameObjectsWithTag("Environment");

            for (int i = 0; i < envObjects.Length; i++)
            {
                SnapToMap snappableObject = envObjects[i].GetComponent<SnapToMap>();
                if (snappableObject != null)
                {
                    snappableObject.SnapToGrid();
                }
            }
        }

        void CreateCell(int x, int y)
        {
            //Instantiate and position cell
            Cell newCell = Instantiate(CellPrefab, transform);
            grid[x, y] = newCell;
            newCell.transform.localPosition = new Vector3(x * cellSize, 0, y * cellSize);
            newCell.Coords = new Vector2Int(x, y);

            //Instantiate and position cell label
            Text label = Instantiate(CellTextLabel);
            label.rectTransform.SetParent(GridCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(newCell.transform.position.x, newCell.transform.position.z);
            label.text = x.ToString() + "," + y.ToString();
        }

        /// <summary>
        /// Destroy all cells in the grid and sets the grid to null. To be called in Play Mode.
        /// </summary>
        public void DestroyGrid()
        {
            if(grid == null || grid.Length == 0)
            {
                return;
            }

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    Cell cell = grid[x, y];
                    if(cell != null)
                    {
                        Destroy(cell);
                    }
                }
            }

            grid = null;
        }

        /// <summary>
        /// Destroys all game objects spawned by this map. To be called in Editor Mode.
        /// </summary>
        public void DestroyGridInEditor()
        {
            Component[] textLabels = GridCanvas.GetComponentsInChildren<Text>();
            for (int i = 0; i < textLabels.Length; i++)
            {
                DestroyImmediate(textLabels[i].gameObject);
            }

            Component[] cells = GetComponentsInChildren<Cell>();
            for (int i = 0; i < cells.Length; i++)
            {
                DestroyImmediate(cells[i].gameObject);
            }

            grid = new Cell[Size, Size];
        }

        /// <summary>
        /// Returns the position of the nearest valid coordinate on the map
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector3 GetClosestCoordinatePosition(Vector3 position)
        {
            Vector2Int coords = ConvertPositiontoCoords(position);

            //TODO: check if the cell at the calculated coordinates contains a map tile

            return new Vector3(coords.x * cellSize, transform.position.y, coords.y * cellSize);
        }

        public Cell GetCellAtPosition(Vector3 position)
        {
            Vector2Int coords = ConvertPositiontoCoords(position);
            
            return grid[coords.x, coords.y];
        }

        public Vector2Int ConvertPositiontoCoords(Vector3 position)
        {
            int x = Mathf.RoundToInt(position.x / cellSize);
            int z = Mathf.RoundToInt(position.z / cellSize);

            int xMax = Size;
            int zMax = Size;

            x = Mathf.Clamp(x, 0, xMax - 1);
            z = Mathf.Clamp(z, 0, zMax - 1);

            return new Vector2Int(x, z);
        }

        
    }

    [CustomEditor(typeof(Map))]
    public class MapEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Map map = (Map)target;

            if(GUILayout.Button("Generate"))
            {
                map.GenerateGridInEditor();
            }
            if (GUILayout.Button("Destroy"))
            {
                map.DestroyGridInEditor();
            }
        }
    }
}

