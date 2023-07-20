using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer_UI : MonoBehaviour  //InGame_UI
{

    private bool gamePaused;

    [Header("Menu gameObjects")]
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject endLevelUI;

    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI currentFruitsAmout;

    [SerializeField] private TextMeshProUGUI endTimerText;
    [SerializeField] private TextMeshProUGUI endBestTimeText;
    [SerializeField] private TextMeshProUGUI endFruitsText;


    private void Start()
    {
        GameManager.instance.levelNumber = SceneManager.GetActiveScene().buildIndex;
        PlayerManager.instance.inGameUI = this;
        Time.timeScale = 1;
        SwitchUI(inGameUI);
    }

    void Update()
    {
        UpdateInGameInfo();

        if (Input.GetKeyDown(KeyCode.Escape))
            CheckIsNotPaused();

    }

    private bool CheckIsNotPaused()
    {
        if (!gamePaused)
        {
            gamePaused = true;
            Time.timeScale = 0;
            SwitchUI(pauseUI);
            return true;    
        }
        else
        {
            gamePaused = false;
            Time.timeScale = 1;
            SwitchUI(inGameUI);
            return false;
        }
    }

    public void onDeath() => SwitchUI(pauseUI);

    public void OnLevelFinished()
    {
        endFruitsText.text = "Fruits: " + PlayerManager.instance.fruits;
        endTimerText.text = "Your Time: " + GameManager.instance.timer.ToString("00") + " s";
        endBestTimeText.text = "Best Time: " + PlayerPrefs.GetFloat("Level" + GameManager.instance.levelNumber + "BestTime", 999).ToString("00") + " s";
        SwitchUI(endLevelUI);
    }

    private void UpdateInGameInfo(){
        timerText.text = "Timer: " + GameManager.instance.timer.ToString("00") + " s";
        currentFruitsAmout.text = PlayerManager.instance.fruits.ToString();
    }
    public void SwitchUI(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        uiMenu.SetActive(true);
    }

    public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");
    public void ReloadCurrentLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void LoadNextLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

}
