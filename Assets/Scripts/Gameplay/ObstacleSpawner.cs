using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject obstaclePrefab;

    [Header("Spawneo")]
    public float spawnInterval = 3f;
    public int maxObstacles = 5;

    [Header("Área de spawneo")]
    public float rangeX = 4f;
    public float spawnZ = 20f;     // más lejos que los orbes
    public float fixedY = 0.3f;

    [Header("Dificultad progresiva")]
    public float minInterval = 1.2f;
    public float difficultyRate = 0.03f;

    private float timer = 0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isPaused) return;

        timer += Time.deltaTime;
        spawnInterval = Mathf.Max(minInterval, spawnInterval - difficultyRate * Time.deltaTime);

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        GameObject[] current = GameObject.FindGameObjectsWithTag("Obstacle");
        if (current.Length >= maxObstacles) return;

        Vector3 spawnPos = new Vector3(
            Random.Range(-rangeX, rangeX),
            fixedY,
            player.position.z + spawnZ
        );

        GameObject obs = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        obs.tag = "Obstacle";

        Destroy(obs, 12f);
    }
}