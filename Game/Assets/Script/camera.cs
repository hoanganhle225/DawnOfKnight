using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour,ICamera
{

    public GameObject targetM;
    public GameObject targetF;
    public Vector3 offset = new Vector3(0f, 10f, -10f);
    

    private void LateUpdate()
    {
        
        if (targetM.activeSelf && targetM.transform != null)
        {
            // Tính toán vị trí mới của camera
            Vector3 desiredPosition = targetM.transform.position + offset;

            // Cập nhật vị trí của camera
            transform.position = desiredPosition;

            // Xoay camera để nhìn xuống mục tiêu
            transform.LookAt(targetM.transform);
        }
        else if (targetF.activeSelf && targetF.transform != null)
        {
            // Tính toán vị trí mới của camera
            Vector3 desiredPosition = targetF.transform.position + offset;

            // Cập nhật vị trí của camera
            transform.position = desiredPosition;

            // Xoay camera để nhìn xuống mục tiêu
            transform.LookAt(targetF.transform);
        }


    }


}
