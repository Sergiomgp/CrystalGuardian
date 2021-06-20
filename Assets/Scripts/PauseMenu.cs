using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isGameOver = false;

    public GameObject menuUI;
    public GameObject HUD;
    public GameObject GameOverMenu;
    public GameObject background;

    private void Start()
    {
        background.SetActive(true);
        isPaused = false;
        //HUD.SetActive(true);
        menuUI.SetActive(false);
        Invoke("DisableBackground", 2f);

    }
    // Update is called once per frame
    void Update()
    {
        if (isGameOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else if (DisableComponent.Cutscene == true)
                {
                    CutScenePause();
                }
                else
                {
                    Pause();
                }
            }
        }

        if (isGameOver)
        {
            GameOverUI();
        }
    }


   public void Resume()
    {
        menuUI.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Pause()
    {
        HUD.SetActive(false);
        menuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        isPaused = true;
    }

    void CutScenePause()
    {
        HUD.SetActive(false);
        menuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        isPaused = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("leaving");
        Application.Quit();
    }

    public void RestartGame()
    {
        HUD.SetActive(true);
        isPaused = false;
        isGameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOverUI()
    {
        HUD.SetActive(false);
        menuUI.SetActive(false);
        GameOverMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    void DisableBackground()
    {
        background.SetActive(false);
    }
}
