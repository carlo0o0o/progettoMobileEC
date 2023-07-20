using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{


    public void SwitchMenuTo(GameObject uiMenu)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        AudioManager.instance.PlaySFX(5);

        uiMenu.SetActive(true);
    }

    public void SetGameDifficulty(int i) => GameManager.instance.difficulty = i;
}
