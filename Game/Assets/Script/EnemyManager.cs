using UnityEngine;

[System.Serializable]
public class EnemyManager : MonoBehaviour
{
    [Header("Set Object")]
    public GameObject healthbarPrefab;
    public GameObject healthBar;
    public GameObject UI;
    public TestHeath healthbar;
    public Spawner spawner;
    public Gethit hit;
    private GameObject enemy;

    [Header("Spawner Properti")]
    public int maxEnemyCount = 5;
    public int currentEnemyCount = 0;
    private EnemyController controller;

    public EnemyStat enemyData = new EnemyStat();
    private EnemyLevelData MainStat = new EnemyLevelData();
   

    // Update is called once per frame
    public void setData()
    {
        enemyData.SetData();
    }
    public void SpawnEnemy(Vector3 spawnPosition, GameObject[] enemyPrefab, string EnemyLV)
    {
        int randomIndex = Random.Range(0, enemyPrefab.Length);
       

        controller = new EnemyController();

        MainStat = enemyData.lvEnemy(EnemyLV);
        controller.m.SetEnemyModel(
            MainStat.Hp, enemyData.enemyData.speed, enemyData.enemyData.reloadTime,
            enemyData.enemyData.detectionRange, enemyData.enemyData.BulletDame,
            enemyData.enemyData.MeleeDame, MainStat.Def, MainStat.Exp, enemyData.enemyData.Lv
        );

       
        hit = new Gethit();

        enemy = Instantiate(enemyPrefab[randomIndex], spawnPosition, Quaternion.identity);
        enemy.AddComponent<Gethit>();
        controller.SetObject(enemy);
        hit = enemy.GetComponent<Gethit>();
        enemy.GetComponent<EnemyView>().SetEnemySpanwer(spawner);
        healthBar = Instantiate(healthbarPrefab, UI.transform);

        healthbar = healthBar.GetComponent<TestHeath>();
        enemy.GetComponent<EnemyView>().SetHealthbar(healthbar);
        controller.v.healthBar.setMaxHealth(MainStat.Hp);
        controller.v.healthBar.SetLv(EnemyLV);
        healthbar.GetComponent<TestHeath>().SetTarget(enemy);
        enemy.GetComponent<EnemyView>().sethealthUI(healthBar);
    }
}