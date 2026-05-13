using UnityEngine;

public class AmbientController : MonoBehaviour
{
    [Header("Luz direccional")]
    public Light directionalLight;

    [Header("Colores de luz por fase")]
    public Color phase1LightColor = new Color(1f, 0.7f, 0.3f);   // naranja
    public Color phase2LightColor = new Color(0.8f, 0.5f, 1f);   // morado
    public Color phase3LightColor = new Color(0.5f, 0.9f, 1f);   // azul brillante

    [Header("Intensidad por fase")]
    public float phase1Intensity = 1.2f;
    public float phase2Intensity = 1.5f;
    public float phase3Intensity = 2f;

    [Header("Color de niebla por fase")]
    public Color phase1FogColor = new Color(0.18f, 0.11f, 0.31f);
    public Color phase2FogColor = new Color(0.25f, 0.1f, 0.4f);
    public Color phase3FogColor = new Color(0.1f, 0.3f, 0.5f);

    [Header("Transición")]
    public float transitionSpeed = 1f;

    private int lastPhase = 1;
    private Color targetLightColor;
    private Color targetFogColor;
    private float targetIntensity;

    void Start()
    {
        targetLightColor = phase1LightColor;
        targetFogColor = phase1FogColor;
        targetIntensity = phase1Intensity;

        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 20f;
        RenderSettings.fogEndDistance = 60f;
    }

    void Update()
    {
        int currentPhase = GameManager.Instance.currentPhase;

        // Detectar cambio de fase
        if (currentPhase != lastPhase)
        {
            lastPhase = currentPhase;

            switch (currentPhase)
            {
                case 2:
                    targetLightColor = phase2LightColor;
                    targetFogColor = phase2FogColor;
                    targetIntensity = phase2Intensity;
                    break;
                case 3:
                    targetLightColor = phase3LightColor;
                    targetFogColor = phase3FogColor;
                    targetIntensity = phase3Intensity;
                    break;
                default:
                    targetLightColor = phase1LightColor;
                    targetFogColor = phase1FogColor;
                    targetIntensity = phase1Intensity;
                    break;
            }
        }

        // Transición suave de luz
        if (directionalLight != null)
        {
            directionalLight.color = Color.Lerp(
                directionalLight.color,
                targetLightColor,
                transitionSpeed * Time.deltaTime
            );

            directionalLight.intensity = Mathf.Lerp(
                directionalLight.intensity,
                targetIntensity,
                transitionSpeed * Time.deltaTime
            );
        }

        // Transición suave de niebla
        RenderSettings.fogColor = Color.Lerp(
            RenderSettings.fogColor,
            targetFogColor,
            transitionSpeed * Time.deltaTime
        );
    }
}
