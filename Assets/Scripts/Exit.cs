using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    int currentScene;
    int nextScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        nextScene = currentScene + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(LoadNextScene());            
        }
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2f);

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }
}
