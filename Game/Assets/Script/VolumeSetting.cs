using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSetting : MonoBehaviour
{
    public GameObject SettingPanel;

    public void SetPannel()
    {
       
            if (IsGamePause())
            {
                Resume();
                Cursor.visible = false;
            }
            else
            {
                Pause();
            }
        
    }
    private bool IsGamePause()
    {
        return SettingPanel.activeSelf;
    }
    public void Resume()
    {
        SettingPanel.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        SettingPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

}
