
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class ButtonMenu : MonoBehaviour
{
    public Button NewGame;
    public Button Continue;
    // Start is called before the first frame update
    void Start()
    {
        string fileName = "Player.json";
        string filePath = Application.persistentDataPath + "/" + fileName;
        if (!File.Exists(filePath)) Continue.interactable=false;

    }
   public void ContinueGame()
    {
        SceneManager.LoadScene(1);

    }
    public void LoadNewGame()
    {
        string fileName = "Player.json";
        string filePath = Application.persistentDataPath + "/" + fileName;
        File.Delete(filePath);
        fileName = "spawner.json";
        filePath = Application.persistentDataPath + "/" + fileName;
        File.Delete(filePath);
        SceneManager.LoadScene(1);
        
    }
    
   
}
