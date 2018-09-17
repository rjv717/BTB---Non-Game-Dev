using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    // OBJECTS
    public GroundMesh groundMesh;
    public GameObject boundaries;
    public BoxCollider[] edges;
    public Player player;
    public Goal goal;
    public Transform obstacleFolder;
    public Transform ObstaclePreFab;

    //EVENTS
    public delegate void LevelStartHandler(LevelData levelData);
    public static event LevelStartHandler OnLevelStart;
    public delegate void LevelEndHandler(bool levelCompleted);
    public static event LevelEndHandler OnLevelEnd;

    void Awake()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        OnLevelStart += SetupLevel;
        Player.OnDie += EndLevel;
        Goal.OnGoalReached += EndLevel;
    }

    private void UnSubscribe()
    {
        OnLevelStart -= SetupLevel;
        Player.OnDie += EndLevel;
        Goal.OnGoalReached -= EndLevel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneLoader.LoadScene(SceneName.MenuMain);
        }
    }

    public static void StartLevel(LevelData levelData)
    {
        if (OnLevelStart != null)
        {
            OnLevelStart(levelData);
        }
    }

    public void SetupLevel(LevelData levelData)
    {
        ClearObstacles();
        GroundMesh.Ground ground = new GroundMesh.Ground();
        ground.X = levelData.groundX;
        ground.Z = levelData.groundZ;
        groundMesh.Generate(ground, levelData);
        Bounds bounds = new Bounds(boundaries.transform.position, levelData.groundX, levelData.groundZ);
        
        for (int i = 0; i < 4; i++)
        {
            edges[i].center = bounds.Centers[i];
            edges[i].size = bounds.Sizes[i];
        }

        player.transform.position = new Vector3(levelData.playerPosition.x, player.transform.position.y, levelData.playerPosition.z);
        goal.transform.position = new Vector3(levelData.goalPosition.x, levelData.goalPosition.y, levelData.goalPosition.z);
        for (int i = 0; i < levelData.obstaclePositions.Length; i++)
        {
            Transform newObstacle = Instantiate(ObstaclePreFab) as Transform;
            newObstacle.position = new Vector3(levelData.obstaclePositions[i].x, ObstaclePreFab.position.y, levelData.obstaclePositions[i].z);
            newObstacle.parent = obstacleFolder;
        }
    }

    private void ClearObstacles ()
    {
        foreach (Transform child in obstacleFolder)
        {
            Destroy(child.gameObject);
        }
    }

    private void EndLevel(bool levelCompleted)
    {
        UnSubscribe();
        if (OnLevelEnd != null)
        {
            OnLevelEnd(levelCompleted);
        }
    }

    private class Bounds
    {
        enum News { North, East, West, South};

        Vector3[] centers;
        Vector3[] sizes;

        public Vector3[] Centers
        {
            get
            {
                return centers;
            }

            set
            {
                centers = value;
            }
        }

        public Vector3[] Sizes
        {
            get
            {
                return sizes;
            }

            set
            {
                sizes = value;
            }
        }

        public Bounds (Vector3 center, int xSize, int zSize)
        {
            Centers = new Vector3[4];
            Sizes = new Vector3[4];

            Centers[(int)News.North].z = zSize / 2;
            Centers[(int)News.East].x = xSize / 2;
            Centers[(int)News.West].x = -(xSize / 2);
            Centers[(int)News.South].z = -(zSize / 2);

            Sizes[(int)News.North].x = xSize;
            Sizes[(int)News.East].z = zSize;
            Sizes[(int)News.West].z = zSize;
            Sizes[(int)News.South].x = xSize;

            for (int i = 0; i < sizeof(News); i++)
            {
                Sizes[i].y = 4;
            }
        }
    }
}
