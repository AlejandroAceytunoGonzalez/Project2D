using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public float minLoadtime = 0.3f;
    private bool gameStarting = false;
    public GameObject loadingScreen;
    public void NewGame() {
        if (!gameStarting)
        {
            gameStarting = true;
            StartCoroutine(LoadYourAsyncScene());
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
            

    IEnumerator LoadYourAsyncScene()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(minLoadtime);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
