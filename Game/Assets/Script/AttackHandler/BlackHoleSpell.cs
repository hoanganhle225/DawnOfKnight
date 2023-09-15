using UnityEngine;

public class BlackHoleSpell : MonoBehaviour
{
    public float pullingForce = 10f;
    public float skillRange = 5f; // Khoảng cách tối đa mà skill có thể kéo mục tiêu vào
    public LayerMask enemyLayer;
    public float SkillDuration=5f;
    void Start()
    {
        Destroy(gameObject,SkillDuration);
    }
    void Update()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, skillRange, enemyLayer);

        // Kiểm tra từng enemy trong tầm
        foreach (Collider enemyCollider in enemiesInRange)
        {
            // Tính vector hướng từ skill đến enemy
            Vector3 directionToEnemy = enemyCollider.transform.position - transform.position;
           
            // Kéo enemy vào trung tâm của skill
            enemyCollider.GetComponent<Rigidbody>().AddForce(-directionToEnemy * pullingForce * Time.deltaTime);
        }
    }
}