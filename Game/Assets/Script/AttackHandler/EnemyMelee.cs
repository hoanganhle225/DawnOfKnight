using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float dame;
    public Collider WeaponCollider;
   


    private void OnTriggerEnter(Collider collision)
    {

        GameObject Target = collision.gameObject;

        if (Target.tag == "Character")
        {
            WeaponCollider.enabled = false;
            singleDmg(Target.transform);


        }

    }

    public void setDame(float dame)
    {
        this.dame = dame;
    }
    void singleDmg(Transform targeted)
    {


        Gethit hit = targeted.GetComponent<Gethit>();
        if (hit != null)
            hit.Take(dame);

    }
}
