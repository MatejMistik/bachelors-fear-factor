using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PauseScript : MonoBehaviour
{

    public static bool gameIsPaused = false;
    private GameObject pauseMenuUI;
    private GameObject infoPanel;
    private Button resumeButton;
    private Button mainMenuButton;
    private Button quitButton;


    void Start()
    {
        resumeButton = transform.Find("PauseMenu/ResumeButton").GetComponent<Button>();
        mainMenuButton = transform.Find("PauseMenu/MainMenuButton").GetComponent<Button>();
        quitButton = transform.Find("PauseMenu/QuitButton").GetComponent<Button>();
        infoPanel = GameObject.Find("InfoPanel");
        pauseMenuUI = GameObject.Find("PauseMenu");
        pauseMenuUI.SetActive(false);
        infoPanel.SetActive(false);
        resumeButton.onClick.AddListener(delegate { TaskOnClickResume(); });
        quitButton.onClick.AddListener(delegate { TaskOnClickQuit(); });
        mainMenuButton.onClick.AddListener(delegate { TaskOnClickMenu(); });

        // Activate info about level on start of the scene;
        infoPanel.SetActive(true);
        Pause();
    }
    void TaskOnClickResume()
    {
        pauseMenuUI.SetActive(false);
        Resume();
    }

    void TaskOnClickQuit()
    {
        Application.Quit();
    }

    void TaskOnClickMenu()
    {
        pauseMenuUI.SetActive(false);
        Resume();
        Loader.Load(Loader.Scene.MainMenu);
    }

    // Update is called once per frame
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
            {
                pauseMenuUI.SetActive(false);
                Resume();
            }
            else
            {
                pauseMenuUI.SetActive(true);
                Pause();
            }

        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (gameIsPaused)
            {
                infoPanel.SetActive(false);
                Resume();
            }   else
            {
                infoPanel.SetActive(true);
                Pause();
            }
        }

        //Debug.Log(Cursor.lockState);

    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
}
