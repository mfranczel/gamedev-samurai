using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public static bool StatOpen = false;
    public static bool EndMenuOpen = false;
    
    public GameObject pauseMenuUi;
    public GameObject statMenuUi;
    public GameObject endGameMenu;
    
    public GameObject loadingScreen;
    public Slider loadingSlider;

    private void OnEnable()
    {
        PlayerController.onPlayerDeath += DisplayGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (StatOpen)
            {
                CloseStats();
            }
            else
            {
                if (GamePaused)
                {
                    Resume();
                }
                else {
                    Pause();
                }   
            }
        } else if (Input.GetKeyDown(KeyCode.Tab)) {
            if (StatOpen)
            {
                CloseStats();
            }
            else {
                if (!GamePaused)
                {
                    OpenStats();
                }
            }
        }
    }

    public void Resume() {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void CloseStats() {
        statMenuUi.SetActive(false);
        Time.timeScale = 1f;
        StatOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void OpenStats() {
        pauseMenuUi.SetActive(false);
        statMenuUi.SetActive(true);
        GamePaused = false;
        Time.timeScale = 0f;
        StatOpen = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void DisplayGameOver()
    {
        endGameMenu.SetActive(true);
        Time.timeScale = 0f;
        EndMenuOpen = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame() {
        Application.Quit();
    }

    void Pause() {
        pauseMenuUi.SetActive(true);
        statMenuUi.SetActive(false);
        StatOpen = false;
        Time.timeScale = 0f;
        GamePaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    public void ReplayGame() {
        StartCoroutine(AsyncLoad());
    }
    
    private IEnumerator AsyncLoad() {
        endGameMenu.SetActive(false);
        pauseMenuUi.SetActive(false);
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }
}
