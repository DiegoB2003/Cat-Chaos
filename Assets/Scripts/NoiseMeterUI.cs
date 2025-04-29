using UnityEngine;
using UnityEngine.UI;

public class NoiseMeterUI : MonoBehaviour
{
    [SerializeField] private Slider noiseSlider;
    [SerializeField] private Image fillImage;

    [Header("Flashing Settings")]
    [SerializeField] private float dangerThreshold = 0.7f;
    [SerializeField] private float flashSpeed = 2f;
    private bool isFlashing = false;

    private void Update()
    {
        float currentNoiseLevel = NoiseManager.Instance.GetNoiseLevel();
        float maxNoise = NoiseManager.Instance.GetMaxNoise();

        float noisePercentage = currentNoiseLevel / maxNoise;
        noiseSlider.value = noisePercentage * 100f;

        // Start/Stop flashing based on threshold
        if (noisePercentage >= dangerThreshold)
        {
            isFlashing = true;
        }
        else
        {
            isFlashing = false;
            fillImage.color = Color.Lerp(Color.green, Color.red, noisePercentage); // reset to static color
        }

        // Flashing effect
        if (isFlashing)
        {
            float t = Mathf.PingPong(Time.time * flashSpeed, 1f);
            fillImage.color = Color.Lerp(Color.red, Color.yellow, t);
        }
    }
}
