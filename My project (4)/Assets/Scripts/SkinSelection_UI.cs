using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinSelection_UI : MonoBehaviour
{
    [SerializeField] private int[] priceForSkin;

    [SerializeField] private bool[] skinPurchased;
                     private int skin_Id;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI bankText;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private Animator anim;

    //private void Start()
    //{
    //    PlayerPrefs.SetInt("TotalFruitsCollected", 1000);
    //}



    private void SetUpSkinInfo()
    {
        skinPurchased[0] = true;

        for(int i=1; i<skinPurchased.Length; i++)
        {
            bool skinUnlocked = PlayerPrefs.GetInt("SkinPurchased" + i) == 1;

            if (skinUnlocked)
                skinPurchased[i] = true;
        }

        bankText.text = PlayerPrefs.GetInt("TotalFruitsCollected").ToString();

        selectButton.SetActive(skinPurchased[skin_Id]);
        buyButton.SetActive(!skinPurchased[skin_Id]);

        if (!skinPurchased[skin_Id])
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = priceForSkin[skin_Id].ToString();    // "Price: " +

        anim.SetInteger("SkinId", skin_Id);
    }

    public bool EnoughMoney()
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruitsCollected");

        if(totalFruits > priceForSkin[skin_Id])
        {
            totalFruits = totalFruits - priceForSkin[skin_Id];

            PlayerPrefs.SetInt("TotalFruitsCollected", totalFruits);

            return true;
        }
        return false;
    }

    public void NextSkin()
    {
        skin_Id++;

        if (skin_Id > 3)
            skin_Id = 0;
        SetUpSkinInfo();
    }

    public void PreviousSkin()
    {
        skin_Id--;

        if (skin_Id < 0)
            skin_Id = 3;
        SetUpSkinInfo();
    }

    public void Buy()
    {
        if (EnoughMoney())
        {

            PlayerPrefs.SetInt("SkinPurchased" + skin_Id, 1);
            //skinPurchased[skin_Id] = true;
            SetUpSkinInfo();
        }
        else
            Debug.Log("NotEnoughMoney");
    }

    public void Select()
    {
        PlayerManager.instance.chosenSkinId = skin_Id;
        Debug.Log("Skin was equiped");
    }



}
