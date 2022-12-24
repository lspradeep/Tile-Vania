using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    private Transform playerTransform;
    public bool isPlayerAlive = true;
    private GameManager gameManager;
    [SerializeField] GameObject restartGameCanva;
    [SerializeField] GameObject helpCanva;
    [SerializeField] TextMeshProUGUI killCountText;
    private int killCount = 0;

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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        restartGameCanva.SetActive(false);
    }

    public void ShowHelpCanva()
    {
        helpCanva.SetActive(true);
        PauseGame();
    }

    public void HideHelpCanva()
    {
        helpCanva.SetActive(false);
        ResumeGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        if (!isPlayerAlive && !restartGameCanva.activeSelf)
        {
            restartGameCanva.SetActive(true);
        }
    }

    public void UpdateKillCount()
    {
        killCount++;
        killCountText.SetText("Kills: " + killCount);
    }
}
