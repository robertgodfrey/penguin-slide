using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fishCountText;
    [SerializeField] private TextMeshProUGUI totalTimeText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI fishLabel;
    [SerializeField] private TextMeshProUGUI timeLabel;

    void Start()
    {
        if (PlayerPrefs.GetInt("Success") == 1)
        {
            gameOverText.text = "YOU MADE IT!";
            fishCountText.text = PlayerPrefs.GetInt("Fish").ToString();
            totalTimeText.text = PlayerPrefs.GetString("Time") + "s";
            fishLabel.text = "FISH:";
            timeLabel.text = "TIME:";
        }
    }
}
