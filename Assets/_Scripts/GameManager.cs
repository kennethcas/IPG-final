using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    GameObject pausePanel;

    private bool isGamePaused = false;
    private void Awake()
    {
        //SINGLETON
        if (instance == null)
            instance = this;
        //pausePanel.SetActive(false);
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

        CheckCameras();

        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pausePanel.SetActive(true);
            //CheckPause();//not working for some reason?
            Debug.Log("checking pause method called");
            /*if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (isGamePaused)
                    PauseGame();
                else
                    ResumeGame();
            }*/

            if (isGamePaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void CheckCameras()
    {
        Debug.Log("checking for triggers to switch CinemachineCameras");
    }

    public void CheckPause()
    {
        Debug.Log("checking pause method called");
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isGamePaused)
                PauseGame();
            else
                ResumeGame();
        }*/

        if (isGamePaused)
            PauseGame();
        else
            ResumeGame();
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
