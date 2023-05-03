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
        //DEV TOOL -> NOT IN THE FINAL BUILD
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }*/

    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void CheckCameras()
    {
     
    }

    public void StartGame()
    {
        SceneManager.LoadScene("2.MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("1.MainMenu");
    }

    public void HelpScreen()
    {
        //if the helpPanel is active
        if (!helpPanel.activeInHierarchy) //RESUMING GAME AND HIDING HELP PANEL
        {
            helpPanel.SetActive(true);
            cmvcam1.enabled = false;
            Time.timeScale = 0;
            
        }
        else if (helpPanel.activeInHierarchy) //PAUSING AND SHOWING HELP PANEL
        {
            helpPanel.SetActive(false);
            cmvcam1.enabled = true;
            Time.timeScale = 1;
        }    
    }
}
