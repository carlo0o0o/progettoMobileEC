using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;
    [HideInInspector] public int fruits;
    [HideInInspector] public Transform respawnPoint;
    [HideInInspector] public GameObject currentPlayer;
    [HideInInspector] public int chosenSkinId;

    public Timer_UI inGameUI;
    [Header("Player Info")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject deathFX;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RespawnPlayer();
    }

    public bool HaveEnoughFruits()
    {
        if (fruits > 0)
        {
            fruits--;

            if (fruits < 0)
                fruits = 0;
            return true;
        }
        return false;
    }

    public void OnTakingDamege()
    {
        if (HaveEnoughFruits())
        {
            Debug.Log("fruit was dropped");
        }
        else
        {
            KillPlayer();
            PermanentDeath();
        }
    }

    private void PermanentDeath()
    {
         //if (GameManager.instance.difficulty< 3)
         //{
          //  Invoke("RespawnPlayer", 1);
         //}
         //else
            inGameUI.onDeath();
            
    }

    public void OnFalling()
    {
        KillPlayer();

        if (HaveEnoughFruits())
        {
            PermanentDeath();
        }
    }
    public void RespawnPlayer()
    {
        if (currentPlayer == null) { 
            AudioManager.instance.PlaySFX(12);
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
            inGameUI.AssignPlayerControlls(currentPlayer.GetComponent<Player>());
        }
    }

    public void KillPlayer()
    {
        AudioManager.instance.PlaySFX(0);
        GameObject newDeathFx = Instantiate(deathFX, currentPlayer.transform.position, currentPlayer.transform.rotation);
        Destroy(newDeathFx, .4f);
        Destroy(currentPlayer);
    }
}
