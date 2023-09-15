
using UnityEngine;

public class EnemyController : ICharacter
{

    public EnemyModel m = new EnemyModel();
    public EnemyView v = new EnemyView();
   /* private gameManager gameManager;*/
    private GameObject enemy;
    private Gethit hit;
    public void SetEnemyModel (EnemyModel model)
    {
        m = model;
    }

    public void SetEnemyView()
    {
        v=enemy.GetComponent<EnemyView>() ;
       
    }
    float DmgTakenCalcaculate(float RawAtk)

    {
        float DmgRec = m.Def / (10 + m.Def);
        return RawAtk * (1 - DmgRec);
    }
    public void HandleDamageTaken(float Dmg)
    {
        m.health -= DmgTakenCalcaculate(Dmg);
        v.setHealth(m.health);
        v.Dmgtaken((int)DmgTakenCalcaculate(Dmg));
        
        v.GetComponent<AudioSource>().Play();
        if (m.health <= 0)
        {

            v.enemySpawner.DecreaseEnemyCount();
            /*gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
            gameManager.AddScore();*/
            gameManager.enemyDead(m.Exp);

        }
    }
    
    public void SetEnemySpawner(Spawner spawner)
    {
        v.enemySpawner = spawner;
    }

    public void SetObject(GameObject enemy)
    {
        this.enemy = enemy;
        hit = this.enemy.GetComponent<Gethit>();
        SetEnemyView();
        hit.OnDamageTaken += HandleDamageTaken;

    }

    public void SetData()
    {
        throw new System.NotImplementedException();
    }

    public void Create()
    {
        throw new System.NotImplementedException();
    }

    public void SetStatus(int Status)
    {
        throw new System.NotImplementedException();
    }
}