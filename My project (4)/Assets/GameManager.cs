using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int difficulty;

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
           Debug.Log(PlayerPrefs.GetInt("Coins"));
        }
    }

    public void SaveGameDifficulty()
    {
        PlayerPrefs.SetInt("GameDifficulty", difficulty);
    }
}
