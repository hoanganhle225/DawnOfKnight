using UnityEngine;
using System.IO;
using System.Data;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    private string fileName = "Player.json";
    private const string defaultContent = @"{
        ""moveSpeed"": 5.0,
        ""jumpForce"": 6.0,
        ""chargeTime"": 1.0,
        ""health"": 200.0,
        ""Dame"": 40.0,
        ""speed"": 5.0,
        ""Def"": 1,
        ""Exp"": 0,
        ""Atk"":40,
        ""Lv"":1,
        ""currentHealth"":200.0
    }";
    public PlayerData playerData = new PlayerData();
    void Start()
    {
        SetData();
    }
   
    public void SetData()
    {

        string filePath = Application.persistentDataPath + "/" + fileName;
        
        // Kiểm tra xem tệp có tồn tại không
        if (File.Exists(filePath))
        {
            ReadData(filePath);
            
        }
        else
        {
            playerData.moveSpeed = 5.0f;
            playerData.jumpForce = 6.0f;
            playerData.chargeTime = 1.0f;
            playerData.health = 200f;
            playerData.Dame = 40f;
            playerData.speed = 8f;
            playerData.Def = 1f;
            playerData.Exp = 0;
            playerData.Atk = 40;
            playerData.Lv = 1;
            playerData.currentHealth = 200;
            
                  
        }
    }

    
    
   
    private void ReadData(string Data)
    {
        // Đọc nội dung của tệp JSON
        string jsonData = File.ReadAllText(Data);
        // Chuyển đổi chuỗi JSON thành đối tượng PlayerData
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }
}