using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 6f;
    public float horizontalLimit = 4f; // límite izquierda/derecha
    public float smoothing = 0.15f;

    [Header("Efecto de inclinación")]
    public float tiltAmount = 15f;
    public float tiltSmooth = 5f;

    [Header("Movimiento hacia adelante")]
    public float forwardSpeed = 8f;

    private Rigidbody rb;
    private float horizontalInput;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isPaused) return;

        // Input horizontal (A/D o flechas)
#if UNITY_EDITOR || UNITY_STANDALONE

        horizontalInput = Input.GetAxisRaw("Horizontal");

#else

if (Input.touchCount > 0)
{
    Touch touch = Input.GetTouch(0);

    horizontalInput = ((touch.position.x / Screen.width) * 2f) - 1f;
}
else
{
    horizontalInput = 0f;
}

#endif

        // Inclinación visual del barco
        float targetTilt = -horizontalInput * tiltAmount;
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0f, 0f, targetTilt),
            tiltSmooth * Time.deltaTime
        );
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isPaused) return;

        // Movimiento hacia adelante automático
        Vector3 targetPos = rb.position + new Vector3(
            horizontalInput * moveSpeed * Time.fixedDeltaTime,
            0f,
            forwardSpeed * Time.fixedDeltaTime  // siempre avanza en Z
        );

        // Limitar movimiento horizontal
        targetPos.x = Mathf.Clamp(targetPos.x, -horizontalLimit, horizontalLimit);

        // Mantener Y fijo sobre el agua
        targetPos.y = rb.position.y;

        rb.MovePosition(targetPos);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            GameManager.Instance.OrbCollected();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.PlayerHit();
        }
    }
}