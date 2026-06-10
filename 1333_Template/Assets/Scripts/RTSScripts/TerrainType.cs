using UnityEngine;

[CreateAssetMenu(fileName = "TerrainType", menuName = "Game/TerrainType")]
public class TerrainType : ScriptableObject
{
    [SerializeField] private string _terrainName;
    [SerializeField] private Color _gizmoColor = Color.white;
    [SerializeField] private bool _walkable = true;
    [SerializeField] private int _movementCost = 1;
    [SerializeField] private Texture2D _terrainTexture;

    // Public properties to access fields
    public string TerrainName => _terrainName;
    public Color GizmoColor => _gizmoColor;
    public bool Walkable => _walkable;
    public int MovementCost => _movementCost;
    public Texture2D TerrainTexture => _terrainTexture;
}
