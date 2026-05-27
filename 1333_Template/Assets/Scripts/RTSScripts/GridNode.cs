using UnityEngine;
 // Represents each node on our grid. Lightweight struct
public struct GridNode
{
    public string Name;
    public Vector3 WorldPosition;
    public bool Walkable;
    public int Weight;
}