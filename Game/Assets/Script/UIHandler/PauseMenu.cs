using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenuUI;
    public Fading fading;
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }
   public void Click()
    {
        if (IsGamePaused())
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        fading.Loadto(SceneManager.GetActiveScene().name);   
    }

    public void Quit(string name)
    {
        fading.Loadto(name);
        
    }

    public bool IsGamePaused()
    {
        return pauseMenuUI.activeSelf;
    }
}
