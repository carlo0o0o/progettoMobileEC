using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer_UI : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    void Start() => timerText = GetComponent<TextMeshProUGUI>();

    void Update() => timerText.text = "Timer: " + GameManager.instance.timer.ToString("00") + " s";
}
