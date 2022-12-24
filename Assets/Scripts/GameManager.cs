using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Transform playerTransform;
    public bool isPlayerAlive = true;
    private GameManager gameManager;

    private void Awake()
    {
        int count = FindObjectsOfType<GameManager>().Length;
        if (count > 1)
        {
            Destroy(this);
        }
    }

    public void updatePlayerTransfrom(Transform transform)
    {
        this.playerTransform = transform;
    }

    public Transform getPlayerTransfrom()
    {
        return this.playerTransform;
    }
}
