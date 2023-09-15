using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestHeath : MonoBehaviour
{
    public float up=1;
    public GameObject Target;
    public Slider HealthBar;
    Camera camera;
    Vector3 tmp;
    public TextMeshProUGUI Lv;
    float min = 2070f;
    float scale;
    public void Start()
    {
      
        camera = Camera.main;
    }
    public void LateUpdate()
    {

       
        tmp = camera.WorldToScreenPoint(Target.transform.position);
        scale = Mathf.Clamp(distance(),0.3f,0.7f);
        transform.position = tmp+ Vector3.up * up;
        transform.localScale=new Vector3(scale, scale, 1f);
    }
    private float distance()
    {
        return Vector3.Distance(tmp, Target.transform.position)/min;
    }
    public void SetTarget(GameObject target)
    {
        Target = target;
    }
    public void SetLv(string lv)
    {
        Lv.text = "Lv " + lv;
    }
    public void setMaxHealth(float h)
    {
        HealthBar.maxValue = h;
        HealthBar.value = h;
    }
    public void setHealth(float h)
    {
        HealthBar.value = h;
    }
}
