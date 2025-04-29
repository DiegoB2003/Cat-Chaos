using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class NoiseMeterUI : MonoBehaviour
{
    [SerializeField] private Slider noiseSlider;
    [SerializeField] private Image fillImage;

    [Header("Flashing Settings")]
    [SerializeField] private float dangerThreshold = 0.7f;
    [SerializeField] private float flashSpeed = 2f;

    [Header("Clip")]
    public AudioClip warningSound;

    [Header("Audio")]
    [SerializeField] private AudioSource warningAudioSource; // assign in Inspector
    private bool isFlashing = false;
    private bool hasPlayedWarning = false;

    private void Update()
    {
        float currentNoiseLevel = NoiseManager.Instance.GetNoiseLevel();
        float maxNoise = NoiseManager.Instance.GetMaxNoise();

        float noisePercentage = currentNoiseLevel / maxNoise;
        noiseSlider.value = noisePercentage * 100f;

        // Handle danger zone
        if (noisePercentage >= dangerThreshold)
        {
            isFlashing = true;

            // Play warning sound only once
            if (!hasPlayedWarning)
            {
                warningAudioSource.clip = warningSound;
                warningAudioSource.Play();
                hasPlayedWarning = true;
            }
        }
        else
        {
            isFlashing = false;
            hasPlayedWarning = false; // Reset when dropping below threshold
            fillImage.color = Color.Lerp(Color.green, Color.red, noisePercentage);
        }

        // Flashing effect
        if (isFlashing)
        {
            float t = Mathf.PingPong(Time.time * flashSpeed, 1f);
            fillImage.color = Color.Lerp(Color.red, Color.yellow, t);
        }
    }
}
