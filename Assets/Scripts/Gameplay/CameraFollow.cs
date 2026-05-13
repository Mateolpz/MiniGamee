using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform target; // el Player

    [Header("Posición relativa al jugador")]
    public Vector3 offset = new Vector3(0f, 4f, -6f);

    [Header("Suavizado")]
    public float smoothSpeed = 5f;
    public float rotationSmooth = 3f;

    [Header("Rotación")]
    public float lookDownAngle = 25f; // grados mirando hacia abajo

    void LateUpdate()
    {
        if (target == null) return;

        // Posición deseada detrás y arriba del jugador
        Vector3 desiredPos = target.position + offset;

        // Suavizado de posición
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );

        // Mirar hacia el jugador con ángulo hacia abajo
        Quaternion desiredRot = Quaternion.Euler(lookDownAngle, 0f, 0f);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            desiredRot,
            rotationSmooth * Time.deltaTime
        );
    }
}
