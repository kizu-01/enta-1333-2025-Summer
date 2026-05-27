using UnityEngine;

//GridSettings is a ScriptableObject for easy customization of grid dimensions and orientation
[CreateAssetMenu(fileName = "GridSettings", menuName = "Game/GridSettings")]
public class GridSettings : ScriptableObject
{
    [SerializeField] private int _gridSizeX;
    [SerializeField] private int _gridSizeY;
    [SerializeField] private float _nodeSize;
    [SerializeField] private bool _useXZPlane;

    public int GridSizeX => _gridSizeX;

    public int GridSizeY => _gridSizeY;

    public float NodeSize => _nodeSize;

    public bool UseXZPlane => _useXZPlane;

}
