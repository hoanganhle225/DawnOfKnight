using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashSceenController : MonoBehaviour
{
    public Image splashImage;

    private float fadeDuration = 2f;
    private float delayDuration = 0;

    IEnumerator Start()
    {
        splashImage.canvasRenderer.SetAlpha(0.0f);
        yield return new WaitForSeconds(delayDuration);

        splashImage.CrossFadeAlpha(1.0f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);

        gameObject.SetActive(false);
        LoadMainScene();
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
