using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Fading : MonoBehaviour
{
    public Image UI;
    private void Start()
    {
        StartCoroutine(Loadin());
    }
    public void Loadto(string level)
    {      
      StartCoroutine(Loadout(level));   
    }
    IEnumerator Loadin()
    {
        float time =2.0f;
        while (time > 0)
        {
            time-= Time.unscaledDeltaTime * 0.5f;
            UI.color=new Color (0f,0f,0f,time);
            yield return 0;
        }
    }

    IEnumerator  Loadout(string level)
    {

        float time = 0f;
        while (time < 1f)
        {
            time += Time.unscaledDeltaTime * 0.5f;
            UI.color = new Color(0f, 0f, 0f, time);
            yield return 0;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }
}
