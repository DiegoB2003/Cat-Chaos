using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeRemaining = 180f; //3 minutes

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (timeRemaining > 0)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60); //Get minutes
            int seconds = Mathf.FloorToInt(timeRemaining % 60); //Get seconds
            timerText.text = $"Time: {minutes:00}:{seconds:00}"; //Display time in MM:SS format

            yield return new WaitForSeconds(1f); //Wait for 1 second
            timeRemaining--; //Decrease time remaining
        }

        timerText.text = "Game Over"; //Display Game Over message when timer reaches 0
    }
}
