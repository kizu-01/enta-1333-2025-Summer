using UnityEngine;

// Represents each node on our grid.  Lightweight struct
[System.Serializable]
public struct GridNode
{
    public string Name; //an index for us to keep track and organize nodes
    public Vector3 WorldPosition;

    // Added for TerrainType reference
    public TerrainType TerrainType;

    // Properties used to safely access TerrainType data
    public bool Walkable => TerrainType != null && TerrainType.Walkable;

    public int Weight => TerrainType != null ? TerrainType.MovementCost : 1;

    public Color GizmoColor => TerrainType != null ? TerrainType.GizmoColor : Color.gray;
}
