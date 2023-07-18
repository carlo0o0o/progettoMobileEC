using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI currentFruitsAmout;


    void Update()
    {
        timerText.text = "Timer: " + GameManager.instance.timer.ToString("00") + " s";
        currentFruitsAmout.text = PlayerManager.instance.fruits.ToString();
    }
}
