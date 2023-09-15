using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EnemyView : MonoBehaviour
{
    
    public GameObject dmgpopupPoint;
    public GameObject DmgTaken;
    public TestHeath healthBar;
    public GameObject healthUI;
    public Spawner enemySpawner;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Collider WeaponCollider;
    public GameObject target;
    public Animator animator;
    public GameObject EnemyMeleeComponent;
    public int status = 1;
    public PlayerView targetVariable;

   
    public EnemyModel m =new EnemyModel();
    public void SetEnemyModel(EnemyModel model)
    {
        m = model;
    }
    public void sethealthUI(GameObject UI)
    {
        this.healthUI = UI;
    }
    public void setHealth(float health)
    {
        healthBar.setHealth(health);
        if (health <= 0) {
            Destroy(this.healthUI);
            status = 0;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            
            Destroy(gameObject, 0.5f);
        }
    }
    public void Dmgtaken(float attack)
    {
        animator.SetTrigger("Damage");
        
        GameObject text = Instantiate(DmgTaken, dmgpopupPoint.transform);
        text.GetComponent<TextMeshProUGUI>().text = "- " + attack.ToString();
    }
    public void SetHealthbar(TestHeath testHeath)
    {
        healthBar = testHeath;  
    }
    public void EnemyDestroyed()
    {
        
    }

    private void LateUpdate()
    {
        
        if (target != null)
        {
            // Lấy vector hướng từ model đến điểm nhìn (target)
            Vector3 direction = target.transform.position - transform.position;

            // Tạo một Quaternion để nhìn vào hướng của target
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Khóa trục Z của Quaternion để giữ nguyên giá trị trục Z của model
            lookRotation.eulerAngles = new Vector3(lookRotation.eulerAngles.z, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.x);

            // Áp dụng Quaternion đã khóa vào transform
            transform.rotation = lookRotation;
        }
    }


    void Update()
    {
        animator.SetBool("Idle", true);
        if (status != 0) 
        {
            DetectTarget();

            if (m.health <= 0) {
                animator.SetTrigger("Death");
                Destroy(gameObject, 10f);
            }
            if (target == null)
                return;

            // Tính toán hướng di chuyển từ vị trí hiện tại đến vị trí của target
            Vector3 direction = target.transform.position - transform.position;
            if (gameObject.tag == "Enemy")
            {
                if (direction.magnitude >= 1.5f && IsAnimationPlaying("Attack") == false && IsAnimationPlaying("Damage") == false)
                {
                    // Normalized direction để có hướng di chuyển đơn vị
                    direction.Normalize();
                    animator.SetBool("Walk", true);
                    animator.SetBool("Idle", false);
                    // Di chuyển đối tượng theo hướng và tốc độ đã cho
                    transform.position += direction * m.speed * Time.deltaTime;
                }
                else if (direction.magnitude < 2f)
                {
                    Punch();
                }
            }
            if (gameObject.tag == "EnemyShoot")
            {
                // Chỉ di chuyển nếu khoảng cách giữa đối tượng và target lớn hơn một ngưỡng nhất định
                if (direction.magnitude >= 5f && IsAnimationPlaying("Attack") == false && IsAnimationPlaying("Damage") == false)
                {
                    // Normalized direction để có hướng di chuyển đơn vị
                    direction.Normalize();
                    animator.SetBool("Walk", true);

                    animator.SetBool("Idle", false);
                    // Di chuyển đối tượng theo hướng và tốc độ đã cho
                    transform.position += direction * m.speed * Time.deltaTime;
                }
                else if (direction.magnitude < 6f)
                {
                    Shoot();
                }
                
            }
            
             


            if (m.currentReloadTime <= m.reloadTime)
            {
                m.currentReloadTime += Time.deltaTime;

            }
        }
    }

    private void DetectTarget()
    {
       
        Collider[] colliders = Physics.OverlapSphere(transform.position, m.detectionRange);
        m.count = 0;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Character"))
            {
                target = collider.gameObject;
                
                m.count = 1;


            }
        }
        if (m.count == 0) 
        {

            animator.SetBool("Walk", false);

            animator.SetBool("Idle", true);
            target = null;
        } 

    }
    public void SetEnemySpanwer(Spawner spawner)
    {
        enemySpawner= spawner;
    }

    public void Shoot()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        if (bulletPrefab != null && bulletSpawnPoint != null && m.reloadTime <= m.currentReloadTime)
        {
            
            animator.SetTrigger("Attack");
            m.currentReloadTime = 0f;
        }
            
    }


    private void Punch()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", false);
        animator.SetTrigger("Attack");

    }

    public void SpawnArrow()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<bullet>().setDame(m.BulletDame);
    }

    private bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f;
    }
    public void EnableMelee()
    {
        WeaponCollider.enabled = true;
    }
    public void DisableMelee()
    {
        WeaponCollider.enabled = false;
    }

}
