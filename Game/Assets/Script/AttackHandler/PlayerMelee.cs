using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float forceMagnitude = 10f;
    public float ScaleBaseAtk = 1f;
    
    private void OnTriggerEnter(Collider collision)
    {

        GameObject Target = collision.gameObject;

        if (Target.tag == "Enemy" || Target.tag == "EnemyShoot" )
        {
            Rigidbody rb = Target.GetComponent<Rigidbody>();
            Vector3 pushDirection = Target.transform.position - transform.position; // Hướng đẩy lùi là hướng từ đối tượng hiện tại tới đối tượng va chạm
            pushDirection = pushDirection.normalized; // Giữ nguyên hướng đẩy lùi

            rb.AddForce(pushDirection * forceMagnitude, ForceMode.Impulse);
            singleDmg(Target.transform);

            
        }
        
    }
    void singleDmg(Transform targeted)
    {
        
     
        Gethit hit = targeted.GetComponent<Gethit>();
        if (hit != null)
        {
            hit.Take(Dmgcalcaculte(ScaleBaseAtk));
            
        }
    }
    float Dmgcalcaculte(float atk)
    {
        return atk * (gameManager.PlayerAtk());
    }
}
