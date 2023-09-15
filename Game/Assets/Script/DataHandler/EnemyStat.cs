using UnityEngine;
using System.IO;


public class EnemyStat 
{
    [SerializeField]
    private string fileName = "Enemy.json";
    private const string defaultContent = @"{
        ""health"": 100.0,
        ""speed"": 5.0,
        ""reloadTime"": 2.0,
        ""detectionRange"": 10.0,
        ""BulletDame"": 5.0,
        ""MeleeDame"": 10.0,
        ""Def"": 1,
        ""Exp"": 10,
        ""Lv"":1
    }";
    public EnemyData enemyData;
    public EnemyLvUpStat lvupStat= new EnemyLvUpStat();
    private bool isSet =false;
    
   
    public EnemyLevelData lvEnemy(string lv)
    {
        if (!isSet)
        {
            SetData();
            lvupStat.LoadEnemyLvUpData();
            isSet = true;
        }
        
        return lvupStat.enemyLvUpData[lv];
    }
    public void SetData()
    {
        lvupStat.LoadEnemyLvUpData();
        isSet = true;
        string filePath = Application.persistentDataPath + "/" + fileName;

        // Kiểm tra xem tệp có tồn tại không
        if (File.Exists(filePath))
        {
            ReadData(filePath);
           
        }
        else
        {
            File.WriteAllText(filePath, defaultContent);
            ReadData(filePath);

         
        }
    }
    public void SetLvStat(string lv)
    {
        enemyData.Exp = lvupStat.enemyLvUpData[lv].Exp;
    }
    [System.Serializable]
    public class EnemyData
    {
        public float health;
        public float speed;
        public float reloadTime;
        public float detectionRange;
        public float BulletDame;
        public float MeleeDame;
        public float Exp;
        public float Def;
        public int Lv;
    }
    private void ReadData(string Data)
    {
        // Đọc nội dung của tệp JSON
        string jsonData = File.ReadAllText(Data);
        // Chuyển đổi chuỗi JSON thành đối tượng PlayerData
        enemyData = JsonUtility.FromJson<EnemyData>(jsonData);
    }
}