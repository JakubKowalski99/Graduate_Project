using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class responsible for Main Menu of the game and its options
public class MainMenu : MonoBehaviour
{
    public GameObject fadeInTransitionScene;
    public GameObject fadeOutTransitionScene;
    public string sceneLoaded;
    public float waitTime;
    
    //Play the game
    public void PlayGame()
    {
        StartCoroutine(Faderoutine());
    }
    //Quit the game
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    //Changing scenes interface, fade out between
    public IEnumerator Faderoutine()
    {
        if (fadeOutTransitionScene != null)
        {
            Instantiate(fadeOutTransitionScene, Vector3.zero, Quaternion.identity);
        }

        yield return new WaitForSeconds(waitTime);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneLoaded);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}