using UnityEngine;

public class ParticleFollow : MonoBehaviour
{
    private Transform player;
    public Vector3 offset = new Vector3(0f, 1f, 5f);

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        transform.position = player.position + offset;
    }
}
