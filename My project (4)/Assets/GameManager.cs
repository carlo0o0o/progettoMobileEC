using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int difficulty;

    [Header("Timer Info")]
    public float timer;
    public bool startTime;

    [Header("Level Info")]
    public int levelNumber;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }

    private void Start()
    {

        if(difficulty == 0)
            difficulty = PlayerPrefs.GetInt("GameDifficulty");

        Debug.Log(PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime"));
    }

    private void Update()
    {
        if (startTime)
            timer += Time.deltaTime;
    }

    public void SaveGameDifficulty()
    {
        PlayerPrefs.SetInt("GameDifficulty", difficulty);
    }

    public void SaveBestTime()
    {
        startTime = false;
        float lastTime = PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime");
        if(timer < lastTime)
            PlayerPrefs.SetFloat("Level" + levelNumber + "BestTime", timer);
        timer = 0;
    }
}
