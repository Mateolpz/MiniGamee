using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    [Header("Velocidad de scroll")]
    public float scrollSpeedX = 0.02f;
    public float scrollSpeedZ = 0.05f;

    [Header("Olas")]
    public float waveHeight = 0.1f;
    public float waveSpeed = 1f;

    private Material mat;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        mat = meshRenderer.material;
    }

    void Update()
    {
        // Scroll de textura (agua moviéndose)
        float offsetX = Time.time * scrollSpeedX;
        float offsetZ = Time.time * scrollSpeedZ;
        mat.mainTextureOffset = new Vector2(offsetX, offsetZ);

        // Ola suave en Y
        float wave = Mathf.Sin(Time.time * waveSpeed) * waveHeight;
        transform.position = new Vector3(
            transform.position.x,
            wave,
            transform.position.z
        );
    }
}