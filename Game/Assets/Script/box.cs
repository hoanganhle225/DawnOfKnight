using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
  public GameObject boxPrefab;
  
        private void LateUpdate()
        {
            
                // Hướng camera về target
                transform.LookAt(boxPrefab.transform);

            
        }
    

}
