using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform target;

    [Header("Posición de cámara")]
    public Vector3 offset = new Vector3(0f, 3f, -6f);


    

    [Header("Suavizado")]
    public float smoothSpeed = 7f;

    [Header("Rotación fija")]
    public Vector3 fixedRotation = new Vector3(10f, 0f, 0f);

    void LateUpdate()
    {
        if (target == null) return;

        // Posición deseada detrás del jugador
        Vector3 desiredPos = target.position + offset;

        // Movimiento suave
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );

        // Mantener ángulo cinematográfico fijo
        transform.rotation = Quaternion.Euler(fixedRotation);
    }
}