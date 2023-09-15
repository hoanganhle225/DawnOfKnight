/*using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class PlayerLvUpStat : MonoBehaviour
{
    private string filePath = "playerLvUp.json";
    public Dictionary<string, PlayerLevelData> playerLvUpData;



    public void LoadPlayerLvUpData()
    {
        string fullPath = Path.Combine(Application.streamingAssetsPath, filePath);
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            //Debug.Log(json);
            playerLvUpData = JsonConvert.DeserializeObject<Dictionary<string, PlayerLevelData>>(json);
        }
        else
        {
            //Debug.LogError("PlayerLvUp file not found!");
        }
    }
}

[System.Serializable]
public class PlayerLevelData
{
    public float Exp;
    public float Atk;
    public float Hp;
    public float Def;
}
*/



using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class PlayerLvUpStat : MonoBehaviour
{
    private string filePath = "playerLvUp.json";
    public Dictionary<string, PlayerLevelData> playerLvUpData;

    public async void LoadPlayerLvUpData()
    {
        await LoadPlayerLvUpDataAsync(); // Gọi async method để tải dữ liệu.

    }

    // Async method để tải dữ liệu từ tệp JSON trong thư mục StreamingAssets trên Android.
    private async Task LoadPlayerLvUpDataAsync()
    {
        string fullPath = Path.Combine(Application.streamingAssetsPath, filePath);
        string json;

        // Trên máy tính cá nhân, đọc dữ liệu trực tiếp từ thư mục StreamingAssets
        // Trên Android, sử dụng UnityWebRequest

        UnityWebRequest www = UnityWebRequest.Get(fullPath);
        var asyncOperation = www.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        json = www.downloadHandler.text;

        playerLvUpData = JsonConvert.DeserializeObject<Dictionary<string, PlayerLevelData>>(json);
       
    }


}

[System.Serializable]
public class PlayerLevelData
{
    public float Exp;
    public float Atk;
    public float Hp;
    public float Def;
}

