using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnitManager _unitManager;
    [SerializeField] private GridManager _gridManager;

    private void Awake()
    {
        _gridManager.InitializeGrid(); 
    }
}
