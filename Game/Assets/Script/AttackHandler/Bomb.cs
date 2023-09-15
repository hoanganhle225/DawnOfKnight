
using TMPro;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    public float atk = 10;
    public float ScaleAtk = 1;
    public float ExplosionRadius = 5f;
    public ParticleSystem Explode;
    public TextMeshProUGUI CountDown;
    public float TimeExplode = 2f;
    public GameObject ClockGlass;
    public GameObject BombPrefab;
    void Start()
    {

        Invoke("attack", TimeExplode);
      

    }

    // Update is called once per frame
    void Update()
    {
        TimeExplode-= Time.deltaTime;
        CountDown.text = ((int)TimeExplode).ToString();
    }
    public void SetAtk(float atk)
    {
        this.atk = atk;
    }
    private void attack() {
        CountDown.enabled= false;
        ClockGlass.GetComponent<MeshRenderer>().enabled = false;
        BombPrefab.GetComponent<MeshRenderer>().enabled = false;
        Explode.Play();
            Destroy(gameObject, 1.5f);
            Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

            foreach (Collider hitCollider in colliders)
            {
                // Kiểm tra nếu collider là đối tượng có tag "Enemy" (thay "Enemy" bằng tag của đối tượng bạn muốn gây sát thương)
                if (hitCollider.CompareTag("Enemy")|| hitCollider.CompareTag("EnemyShoot"))
                {
                    // Thực hiện sát thương cho đối tượng có tag "Enemy"
                    Gethit Enemy = hitCollider.GetComponent<Gethit>();
                    if (Enemy != null)
                    {
                        Enemy.Take(atk*ScaleAtk);
                    }
                
            }
        }
    }
}
