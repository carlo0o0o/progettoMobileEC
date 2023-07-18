using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinSelection_UI : MonoBehaviour
{
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equpButton;
    [SerializeField] private Animator anim;
    [SerializeField] private int skin_Id;
    [SerializeField] private bool[] skinPurchased;
    [SerializeField] private int[] priceForSkin;

    private void Start()
    {
        skinPurchased[0] = true;
    }

    private void SetUpSkinInfo()
    {
        equpButton.SetActive(skinPurchased[skin_Id]);
        buyButton.SetActive(!skinPurchased[skin_Id]);

        if (!skinPurchased[skin_Id])
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = priceForSkin[skin_Id].ToString();    // "Price: " +

        anim.SetInteger("SkinId", skin_Id);
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
        skinPurchased[skin_Id] = true;

        SetUpSkinInfo();
    }

    public void Select()
    {
        PlayerManager.instance.chosenSkinId = skin_Id;
        Debug.Log("Skin was equiped");
    }

}
