using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [HideInInspector]
    public bool isPaused = false;
    [SerializeField]
    GameObject pausePanel;
    [SerializeField]
    CinemachineVirtualCamera cmvcam1;
    private void Awake()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        cmvcam1.enabled = false;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        cmvcam1.enabled = true;

        pausePanel.SetActive(false);
    }
}
