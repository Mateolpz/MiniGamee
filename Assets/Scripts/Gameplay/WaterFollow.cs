using UnityEngine;

public class WaterFollow : MonoBehaviour
{
    private Transform player;
    public float offsetZ = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            player.position.z + offsetZ
        );
    }
}
