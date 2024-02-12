using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 30f;
    public TextMeshProUGUI timerText;
    private float timeRemaining;

    void Start()
    {
        timeRemaining = totalTime;
        InvokeRepeating("UpdateTimer", 1f, 1f);
    }

    void UpdateTimer()
    {
        timeRemaining -= 1f;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            // todo end game
        }
        timerText.text = string.Format("{0:00}", timeRemaining);
    }
}
