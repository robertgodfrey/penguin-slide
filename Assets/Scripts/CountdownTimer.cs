using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float totalTime = 45f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private PenguinController penguinController;
    
    private float timeRemaining;

    void Start()
    {
        timeRemaining = totalTime;
        InvokeRepeating("UpdateTimer", 1f, 1f);
    }

    void UpdateTimer()
    {
        timeRemaining -= 1f;

        if (timeRemaining <= 5)
        {
            timerText.color = Color.red;
        }
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            PlayerPrefs.SetString("FailReason", "OUT OF TIME");
            penguinController.EndGame(false);
        }
        timerText.text = string.Format("00:{0:00}", timeRemaining);
    }

    public string GetTotalTime()
    {
        return (totalTime - timeRemaining).ToString("F1");
    }
}
