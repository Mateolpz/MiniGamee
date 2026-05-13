using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject orbPrefab;

    [Header("Spawneo")]
    public float spawnInterval = 2f;
    public int maxOrbs = 8;

    [Header("Área de spawneo")]
    public float rangeX = 4f;      // ancho del área (igual que el límite del jugador)
    public float spawnZ = 15f;     // qué tan lejos aparecen por delante
    public float fixedY = 0.5f;    // altura sobre el agua

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

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnOrb();
        }
    }

    void SpawnOrb()
    {
        GameObject[] current = GameObject.FindGameObjectsWithTag("Orb");
        if (current.Length >= maxOrbs) return;

        // Spawnear delante del jugador en X aleatorio
        Vector3 spawnPos = new Vector3(
            Random.Range(-rangeX, rangeX),
            fixedY,
            player.position.z + spawnZ
        );

        GameObject orb = Instantiate(orbPrefab, spawnPos, Quaternion.identity);
        orb.tag = "Orb";

        Destroy(orb, 10f);
    }
}