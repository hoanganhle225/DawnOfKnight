using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class EnemyLvUpStat : MonoBehaviour
{
    private string filePath = "EnemyLVUp.json";
    public Dictionary<string, EnemyLevelData> enemyLvUpData;



    public async void LoadEnemyLvUpData()
   
    {
        await LoadEnemyLvUpDataAsync(); // Gọi async method để tải dữ liệu.
        
    }

    // Async method để tải dữ liệu từ tệp JSON trong thư mục StreamingAssets trên Android.
    private async Task LoadEnemyLvUpDataAsync()
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
        enemyLvUpData = JsonConvert.DeserializeObject<Dictionary<string, EnemyLevelData>>(json);
       
    }

}

[System.Serializable]
public class EnemyLevelData
{
    public float Exp;
    public float Atk;
    public float Hp;
    public float Def;
}
