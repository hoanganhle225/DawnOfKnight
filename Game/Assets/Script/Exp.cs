using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerExp : MonoBehaviour
{
    public Slider playerExp;
    // Start is called before the first frame update
    public void setMaxExp(float h)
    {
        playerExp.maxValue = h;
       
    }
    public void setExp(float h)
    {
        playerExp.value = h;
    }
    public void setmin(float h)
    {
        playerExp.minValue = h;
    }
}
