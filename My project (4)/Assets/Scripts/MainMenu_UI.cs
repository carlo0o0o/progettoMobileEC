using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{
    //[SerializeField] private GameObject continueButton;
    [SerializeField] private VolumeController_UI[] volumeController;

    private void Start()
    {
        //for(int i = 0; i<volumeController.Length; i++)
        //{
            //volumeController[i].GetComponent<VolumeController_UI>().SetUpVolumeSlider();
        //}
    }

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
