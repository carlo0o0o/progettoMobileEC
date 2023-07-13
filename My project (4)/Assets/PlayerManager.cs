using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    public Transform respawnPoint;
    public GameObject currentPlayer;

    [SerializeField] private GameObject playerPrefab;

    private void Awake()
    {
        instance = this;
        PlayerRespawn();

    }

    public void PlayerRespawn()
    {
        if (currentPlayer == null)
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
    }
}
