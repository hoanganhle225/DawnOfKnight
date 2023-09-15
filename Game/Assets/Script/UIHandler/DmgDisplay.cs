using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Target;
   void Start(){
        Target = GameObject.FindGameObjectWithTag("MainCamera");
        Destroy(gameObject,0.8f);

        }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Target.transform.position);
        transform.Translate(Vector3.up * 2 * Time.deltaTime);
    }
}
