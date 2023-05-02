using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //[SerializeField]
    Animator anim;

    [SerializeField]
    float transitionTime = 1.0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void StartGame()
    {
        /*StartCoroutine(LoadLevel(
            SceneManager.GetActiveScene().buildIndex + 1));*/

        //Scene currentScene = SceneManager.GetActiveScene();
       
        SceneManager.LoadScene("2.MainScene");
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
