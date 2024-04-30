using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Class responsible for changing the scenes smoothly and position of player
public class SceneChange : MonoBehaviour
{
    public string sceneLoaded;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public Vector2 cameraNewMax;
    public Vector2 cameraNewMin;
    public VectorValue cameraMin;
    public VectorValue cameraMax;
    
    public GameObject fadeInTransitionScene;
    public GameObject fadeOutTransitionScene;
    public float waitTime;

    private void Awake()
    {
        if (fadeInTransitionScene != null)
        {
            GameObject fade = Instantiate(fadeInTransitionScene, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(fade, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerStorage.positionValue = playerPosition;
            StartCoroutine(Faderoutine());
        }
    }

    public IEnumerator Faderoutine()
    {
        if (fadeOutTransitionScene != null)
        {
            Instantiate(fadeOutTransitionScene, Vector3.zero, Quaternion.identity);
        }

        yield return new WaitForSeconds(waitTime);
        //ResetCameraBounds();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneLoaded);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    private void ResetCameraBounds()
    {
        cameraMax.positionValue = cameraNewMax;
        cameraMin.positionValue = cameraNewMin;
    }
}