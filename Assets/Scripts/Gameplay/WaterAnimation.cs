using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    [Header("Velocidad de scroll MUY lenta")]
    public float scrollSpeedX = 0.0003f;
    public float scrollSpeedZ = 0.001f;

    [Header("Olas casi imperceptibles")]
    public float waveHeight = 0.003f;
    public float waveSpeed = 0.08f;

    private Material mat;
    private MeshRenderer meshRenderer;

    private Vector3 startPos;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        mat = meshRenderer.material;

        startPos = transform.position;
    }

    void Update()
    {
        // Movimiento extremadamente suave de textura
        float offsetX = Time.time * scrollSpeedX;
        float offsetZ = Time.time * scrollSpeedZ;

        mat.mainTextureOffset = new Vector2(offsetX, offsetZ);

        // Ola mínima
        float wave = Mathf.Sin(Time.time * waveSpeed) * waveHeight;

        transform.position = new Vector3(
            startPos.x,
            startPos.y + wave,
            startPos.z
        );
    }
}