using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    public void PlayGame() {
        StartCoroutine(AsyncLoad(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void QuitGame() {
        Application.Quit();
    }

    private IEnumerator AsyncLoad(int sceneId) {
        gameObject.SetActive(false);
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }

}
