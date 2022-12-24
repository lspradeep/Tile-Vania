using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooberPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject goober;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(ProduceGoober());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ProduceGoober()
    {
        yield return new WaitForSecondsRealtime(15);
        Instantiate(goober, transform.position, Quaternion.identity);
        if (gameManager.isPlayerAlive)
        {
            StartCoroutine(ProduceGoober());
        }
    }
}
