using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Wobble")]
    public float wobbleAmount = 0f;
    public float wobbleSpeed = 1.2f;

    private Vector3 startPos;
    private Transform player;

    void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isPaused) return;

        // Solo se mece suavemente, no se mueve
        float wobble = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
        transform.position = new Vector3(
            startPos.x + wobble,
            startPos.y,
            startPos.z
        );

        // Se destruye cuando queda atrás del jugador
        if (player != null && transform.position.z < player.transform.position.z - 15f)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FlashEffect());
        }
    }

    System.Collections.IEnumerator FlashEffect()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            Color original = rend.material.color;
            rend.material.color = new Color(1f, 0.3f, 0.3f, 0.8f);
            yield return new WaitForSeconds(0.2f);
            rend.material.color = original;
        }
    }
}