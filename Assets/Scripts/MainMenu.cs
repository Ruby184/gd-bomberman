using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game");
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Progress :" + asyncOperation.progress);

        while (!asyncOperation.isDone)
        {
            Debug.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");

            if (asyncOperation.progress >= 0.9f)
            {
                Debug.Log("Press the space bar to continue");

                if (Input.GetKeyDown(KeyCode.Space)) {
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
