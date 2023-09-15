using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameBtn : MonoBehaviour
{
    public GameObject loadingScene;
    public Slider barLoading;
    public Fading fading;
    public Button NewGame;
    public Button Continue;
    // Start is called before the first frame update
    void Start()
    {
        string fileName = "Player.json";
        string filePath = Application.persistentDataPath + "/" + fileName;
        if (!File.Exists(filePath)) Continue.interactable = false;

    }
    public void ContinueGame()
    {
        StartGame(1);

    }
    public void LoadNewGame()
    {
        string fileName = "Player.json";
        string filePath = Application.persistentDataPath + "/" + fileName;
        File.Delete(filePath);
        fileName = "spawner.json";
        filePath = Application.persistentDataPath + "/" + fileName;
        File.Delete(filePath);
        StartGame(1);

    }
    public void StartGame(int indexScene)
    {
        StartCoroutine(LoadScene(indexScene));
    }

    IEnumerator LoadScene(int indexScene)
    {
        loadingScene.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (timer < 3f)
        {
            timer += Time.deltaTime;
            barLoading.value = timer / 3f;
            yield return null;
        }

        barLoading.value = 1f;
        operation.allowSceneActivation = true;
    }
}
