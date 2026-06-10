using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Pathfinder : MonoBehaviour
{
    public enum PathfindingType
    {
        Naive
    }

    public enum VisualizationState
    {
        Idle,
        Exploring,
        Reconstructing,
        Paused
    }

    [Header("Required Reference")]
    [SerializeField] private GridManager gridManager;

    [Header("Pathfinding Settings")]
    [SerializeField] private PathfindingType pathfindingType = PathfindingType.Naive;
    [SerializeField, Range(0, 100)] private int framesPerStep = 10;
    [SerializeField] private bool visualizePathfinding = true;

    [Header("Visualization Colors")]
    [SerializeField] private Color startNodeColor = Color.green;
    [SerializeField] private Color endNodeC = Color.red;
    [SerializeField] private Color currentPathColor = Color.yellow;
    [SerializeField] private Color visitedNodeColor = new Color(0.5f, 0.5f, 1f, 0.5f);
    [SerializeField] private Color unvisitedNodeColor = new Color(0.3f, 0.3f, 0.3f, 0.3f);
    [SerializeField] private Color finalPathColor = Color.cyan;
    [SerializeField] private Color currentNodeColor = Color.magenta;
    [SerializeField] private Color currentNeighborColor = new Color(1f, 0.5f, 0f, 0.5f); // Orange
    [SerializeField] private Color explorationLineColor = new Color(1f, 1f, 0f, 0.3f); // semi-transparent yellow

    [Header("Visualization Settings")]
    [SerializeField] private int currentSeed = 0;
    [SerializeField] private bool useSeededRandom = true;
    [SerializeField] private float minWeight = 1f;
    [SerializeField] private float maxWeight = 10f;

    // Pathfinder instances
    private NaivePathfinder naivePathfinder;

    //Visualizer instances
    private NaivePathfinderVisualizer naivePathfinderVisualizer;

    private System.Random seededRandom;

    // Visualization State
    private HashSet<Vector2Int> visitedNodes = new HashSet<Vector2Int>();
    private Dictionary<Vector2Int, int> nodeDistance = new Dictionary<Vector2Int, int>(); // track distances from start
    private List<Vector2Int> currentPath = new List<Vector2Int>();
    private List<Vector2Int> reconstructionPath = new List<Vector2Int>();
    private Vector2Int startNode;
    private Vector2Int endNode;

}
