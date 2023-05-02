using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    GameObject helpPanel;

    [SerializeField]
    CinemachineVirtualCamera cmvcam1;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

        //CheckCameras();

    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void CheckCameras()
    {
        //Debug.Log("checking for triggers to switch CinemachineCameras");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("2.MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HelpScreen()
    {
        if (helpPanel.activeSelf) //RESUMING GAME AND HIDING HELP PANEL
        {
            Debug.Log("help panel is active in hierarchy");
            helpPanel.SetActive(false);
            cmvcam1.enabled = true;
            Time.timeScale = 1;
            
        }
        else if (!helpPanel.activeSelf) //PAUSING AND SHOWING HELP PANEL
        {
            Debug.Log("help panel is  NOT active in hierarchy");
            helpPanel.SetActive(true);
            cmvcam1.enabled = false;
            Time.timeScale = 0;
        }
        

    }

  
}
