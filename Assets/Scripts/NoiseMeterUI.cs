using UnityEngine;
using UnityEngine.UI;

public class NoiseMeterUI : MonoBehaviour
{
    [SerializeField] public Slider noiseSlider;  // Assign the UI slider in the Inspector
    [SerializeField] public Image fillImage;     // Assign the Fill Area Image of the Slider
    private float currentNoiseLevel;
    private float maxNoise = 10f; // Adjust based on game balance

    void Update()
    {
        // Simulate getting noise level from game actions (replace with actual noise tracking)
        currentNoiseLevel = Mathf.Clamp(PlayerMovement.noiseLevel, 0, maxNoise);
        
        // Normalize value for the slider (0 to 100)
        float noisePercentage = (currentNoiseLevel / maxNoise);
        noiseSlider.value = noisePercentage * 100;

        // Change color from green (safe) to red (danger)
        fillImage.color = Color.Lerp(Color.green, Color.red, noisePercentage);
    }
}
