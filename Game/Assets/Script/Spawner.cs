using System.IO;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyManager Enemy;
    public TextMeshProUGUI Lv;
    public TextMeshProUGUI Timer;
    public gameManager gameManager;
    [Header("Prefab")]
    public GameObject[] enemyPrefab;
    public Transform playerTransform;
    [Header("Spawn Atribute")]
    public float spawnRadius = 10f;
    public int maxEnemyCount = 5;
    public int scaleLv = 1;
    private int ogCount;
    private float timeSpawner = 0;
    private int currentLv = 1;
    private string fileName = "Spawner.json";
    private const string defaultContent = @"{
        ""timeSpawner"":15
    }";
    private float nextWave;

    // Đánh dấu 4 điểm tạo thành hình vuông
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;

    private void Update()
    {
        timeSpawner -= Time.deltaTime;
        if (timeSpawner <= 0)
        {
            setLV();
            if (currentLv < 1) currentLv = 1;
            SpawnEnemyInSquare();
            maxEnemyCount = ogCount;
            timeSpawner = nextWave;
           
        }
        Timer.text = "Next Lv In: " + (int)(timeSpawner);
    }

    private void Start()
    {
       
        ogCount = maxEnemyCount;
        SetupSpawner();
        nextWave = timeSpawner;
        timeSpawner = 5f;
        Application.targetFrameRate=60;
        Enemy.setData();
    }

    public void setLV()
    {
        currentLv = gameManager.playerController.m.Lv / scaleLv;
        Lv.text = "Lv: " + currentLv;
    }

    // Sửa đổi phương thức spawn enemy để spawn random trong hình vuông
    private void SpawnEnemyInSquare()
    {
        while (maxEnemyCount > 0)
        {
           
            int rate = Random.Range(0, 99);
            if (currentLv < 3 && rate <= 20) rate = 50;
            if (currentLv < 2 && rate > 20 && rate <= 50) rate = 99;
            if (rate <= 20)
            {
                Vector3 spawnPosition = GetRandomPositionInSquare();
                Enemy.SpawnEnemy(spawnPosition, enemyPrefab, (currentLv - 2).ToString());
            }

            if (rate > 20 && rate <= 50)
            {
                Vector3 spawnPosition = GetRandomPositionInSquare();
                Enemy.SpawnEnemy(spawnPosition, enemyPrefab, (currentLv - 1).ToString());
            }
            if (rate > 50)
            {
                Vector3 spawnPosition = GetRandomPositionInSquare();
                Enemy.SpawnEnemy(spawnPosition, enemyPrefab, (currentLv).ToString());
            }
           
            maxEnemyCount--;
        }
    }

    // Lấy vị trí random trong hình vuông
    private Vector3 GetRandomPositionInSquare()
    {
        Vector3 pointA = point1.transform.position;
        Vector3 pointB = point2.transform.position;
        Vector3 pointC = point3.transform.position;
        Vector3 pointD = point4.transform.position;

        // Random tọa độ x và z trong khoảng min-max của hình vuông
        float minX = Mathf.Min(pointA.x, pointB.x, pointC.x, pointD.x);
        float maxX = Mathf.Max(pointA.x, pointB.x, pointC.x, pointD.x);
        float minZ = Mathf.Min(pointA.z, pointB.z, pointC.z, pointD.z);
        float maxZ = Mathf.Max(pointA.z, pointB.z, pointC.z, pointD.z);

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        float randomY = point1.transform.position.y; // Chỉ spawn cao hơn trục y của khu vực

        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

        return spawnPosition;
    }

    public int getTimer()
    {
        return (int)timeSpawner;
    }

    public void DecreaseEnemyCount()
    {
        // Giảm biến đếm enemy
        Enemy.currentEnemyCount--;

        if (Enemy.currentEnemyCount < maxEnemyCount)
        {
            // Gọi hàm tạo enemy khi còn chỗ trống

        }
    }

    void SetupSpawner()
    {

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

    private void ReadData(string Data)
    {
        // Đọc nội dung của tệp JSON
        string jsonData = File.ReadAllText(Data);
        // Chuyển đổi chuỗi JSON thành đối tượng PlayerData
        Data tmp = JsonUtility.FromJson<Data>(jsonData);

        timeSpawner = tmp.timeSpawner;
    }

    class Data
    {
        public int timeSpawner;
    }
}