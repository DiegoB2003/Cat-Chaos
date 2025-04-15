using UnityEngine;
using UnityEngine.UI;

public class NoiseMeterUI : MonoBehaviour
{
    [SerializeField] private Slider noiseSlider;
    [SerializeField] private Image fillImage;

    void Update()
    {
        float currentNoiseLevel = NoiseManager.Instance.GetNoiseLevel();
        float maxNoise = NoiseManager.Instance.GetMaxNoise();

        float noisePercentage = currentNoiseLevel / maxNoise;
        noiseSlider.value = noisePercentage * 100f;

        fillImage.color = Color.Lerp(Color.green, Color.red, noisePercentage);
    }
}
