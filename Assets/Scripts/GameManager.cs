using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Transform playerTransform;
    public bool isPlayerAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
