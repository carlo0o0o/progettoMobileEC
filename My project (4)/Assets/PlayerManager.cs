using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform respawnPoint;
                     public GameObject currentPlayer;

    private void Awake()
    {
        instance = this;
        PlayerRespawn();

    }

    private void PlayerRespawn()
    {
        if (currentPlayer == null)
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
    }
}
