using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeathBar : MonoBehaviour
{
    public GameObject Target;
    public Slider HealthBar;
    public void Start()
    {
        Target = GameObject.FindGameObjectWithTag("MainCamera");
    }
    public void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Target.transform.position);
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
