using UnityEngine;
using System.Collections.Generic;

// We protect the UnityEditor namespace so it is completely stripped out during standalone player builds
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GridManager : MonoBehaviour
{
    [SerializeField] private TerrainType _defaultTerrain;
    //variable to allow us to plug in our GridSettings scriptable Object
    [SerializeField] private GridSettings _gridSettings;
    public GridSettings GridSettings => _gridSettings;

    //2-Dimensional array of GridNode structs that represents our grid
    private GridNode[,] gridNodes;

#if UNITY_EDITOR
    [Header("Debug for editor playmode only")]
    [SerializeField] private List<GridNode> AllNodes = new();
#endif

    //flag for other scripts or this one to use to make sure grid is initialized before doign something else
    public bool IsInitialized { get; private set; } = false;

    public void InitializeGrid()
    {
        //initializing our array of GridNode structs using the dimensions from the Scriptable Objects
        gridNodes = new GridNode[_gridSettings.GridSizeX, _gridSettings.GridSizeY];

        //Nest for Loop to iterate over all GridNodes
        for (int x = 0; x < _gridSettings.GridSizeX; x++)
        {
            for (int y = 0; y < _gridSettings.GridSizeY; y++)
            {
                Vector3 worldPos = _gridSettings.UseXZPlane
                    ? new Vector3(x, 0, y) * _gridSettings.NodeSize
                    : new Vector3(x, y, 0) * _gridSettings.NodeSize;

                // Updated struct initialization to use TerrainType
                GridNode node = new GridNode
                {
                    Name = $"Cell_{(x + _gridSettings.GridSizeX * x + y)}",
                    WorldPosition = worldPos,
                    TerrainType = _defaultTerrain
                };

                gridNodes[x, y] = node;
            }
        }
        IsInitialized = true;
    }

#if UNITY_EDITOR
    public void PopulateDebugList()
    {
        AllNodes.Clear();

        for (int x = 0; x < _gridSettings.GridSizeX; x++)
        {
            for (int y = 0; y < _gridSettings.GridSizeY; y++)
            {
                GridNode node = gridNodes[x, y];

                AllNodes.Add(new GridNode
                {
                    Name = $"Cell_{x}+{y}",
                    WorldPosition = node.WorldPosition,
                    TerrainType = node.TerrainType
                });
            }
        }
    }
#endif

    //Function to retrieve GridNode data efficiently
    public GridNode GetNode(int x, int y)
    {
        if (x < 0 || x >= _gridSettings.GridSizeX || y < 0 || y >= _gridSettings.GridSizeY)
            throw new System.IndexOutOfRangeException("Grid node indices out of range");

        return gridNodes[x, y];
    }

    //Efficient visualization using Gizmos, toggleable through Unity Editor
    private void OnDrawGizmos()
    {
        if (gridNodes == null || GridSettings == null) return;

        //Draw the gridnode gizmos, size is 90% of GridNode Size for visual clarity
        for (int x = 0; x < _gridSettings.GridSizeX; x++)
        {
            for (int y = 0; y < _gridSettings.GridSizeY; y++)
            {
                GridNode node = gridNodes[x, y];

                // Modified to use dynamic GizmoColor based on TerrainType
                Gizmos.color = node.GizmoColor;
                Gizmos.DrawWireCube(node.WorldPosition, Vector3.one * GridSettings.NodeSize * 0.9f);
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //first draw the normal inspector GUI
        DrawDefaultInspector();

        //then look at the GridManager class this is attached to and call the PopulateDebugList function
        GridManager grid = (GridManager)target;
        if (grid.IsInitialized)
        {
            if (GUILayout.Button("Refresh Grid Debug View"))
            {
                // This method is also protected by #if UNITY_EDITOR inside GridManager, making it safe to execute here
                grid.PopulateDebugList();
            }
        }
    }
}
#endif