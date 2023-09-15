using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthbar : MonoBehaviour
{
    public Slider PlayerHealthBar;
    // Start is called before the first frame update
    public void setMaxHealth(float h)
    {
        PlayerHealthBar.maxValue = h;
        PlayerHealthBar.value = h;
    }
    public void setHealth(float h)
    {
        PlayerHealthBar.value = h;
    }
}
