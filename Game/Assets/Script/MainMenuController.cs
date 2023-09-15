using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float animationDuration = 3f;
    private bool isAnimating = false;
    public GameObject aboutGame;
    public GameObject quitGameChoose;
    public GameObject settingMenu;
    public GameObject chooseSetting;
    public GameObject settingVolume;
    public GameObject settingBack;
    public GameObject settingSex;
    public GameObject ExitButton;

    private void Start()
    {

        aboutGame.SetActive(false);
        quitGameChoose.SetActive(false);
        settingMenu.SetActive(false);
    }

    public void OnButtonAbout()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            StartCoroutine(AnimateText());
        }
        aboutGame.SetActive(true);
    }

    public void QuitAboutGame()
    {
        aboutGame.SetActive(false);
    }

    public void QuitGameYes()
    {
        
        Application.Quit();
    }

    public void QuitGameNo()
    {
        quitGameChoose.SetActive(false);
    }

    public void QuitGameChoose()
    {
        quitGameChoose.SetActive(true);
    }

    private IEnumerator AnimateText()
    {
        string fullText = textMeshPro.text;
        textMeshPro.text = "";

        foreach (char character in fullText)
        {
            textMeshPro.text += character;
            yield return new WaitForSeconds(animationDuration / fullText.Length);
        }

        isAnimating = false;
    }

    //Setting Menu

    public void SettingMenu()
    {
        settingMenu.SetActive(true);
    }

    public void ExitSettingMenu()
    {
        settingMenu.SetActive(false);
        chooseSetting.SetActive(true);
        settingVolume.SetActive(false);
        settingSex.SetActive(false);
    }
    public void BackButton()
    {
        settingVolume.SetActive(false);
        ExitButton.SetActive(true);
        settingBack.SetActive(false);
        settingSex.SetActive(false);
        chooseSetting.SetActive(true);
    }
    public void ChooseVolume()
    {
        chooseSetting.SetActive(false);
        settingVolume.SetActive(true);
        ExitButton.SetActive(false);
        settingBack.SetActive(true);
    }

    public void ChooseSex()
    {
        chooseSetting.SetActive(false);
        settingSex.SetActive(true);
        ExitButton.SetActive(false);
        settingBack.SetActive(true);
    }
}
