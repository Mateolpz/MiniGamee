using UnityEngine;

public class Orb : MonoBehaviour
{
    [Header("Flotación")]
    public float floatSpeed = 1.5f;
    public float floatAmount = 0.2f;

    [Header("Rotación")]
    public float rotateSpeed = 60f;

    [Header("Movimiento hacia el jugador")]
    public float forwardSpeed = 0f; // 0 = estático, >0 = viene hacia ti

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Flotación suave en Y
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z - forwardSpeed * Time.deltaTime
        );

        // Rotación suave
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}