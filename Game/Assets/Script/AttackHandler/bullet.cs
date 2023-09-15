using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    private Rigidbody rb;
    public float attack;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        // Hủy đối tượng sau khoảng thời gian lifetime
        Destroy(gameObject, lifetime);
    }

    public void setDame(float attack)
    {
        this.attack = attack;
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        GameObject Target = collision.gameObject;
       
        if (Target.tag =="Character")
        {
            
            singleDmg(Target.transform);

            Destroy(gameObject, 0);
        }
        else if(Target.tag == "Ground")
        {
            rb.velocity = transform.forward * 0;
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject, lifetime);
        }
    }
    void singleDmg(Transform targeted)
    {
        
        Gethit player;     
            player = targeted.GetComponent<Gethit>();

            if (player != null)
                player.Take(attack);
        
        
       
    }
}
