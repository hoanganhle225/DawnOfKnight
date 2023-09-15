using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{

    [Header("Basic Object")]
    public GameObject DmgTaken;
    public Collider WeaponCollider;
    public PlayerHealthbar healthBar;
    public Animator animator;
    public GameObject ultiPrefab;
    public Transform cameraTransform;
    public Rigidbody rb;
    public bool isGrounded = true;
    public GameObject Bomb;
    public GameObject BlackHole;
    public GameObject LvUpPrefab;
    public GameObject ChargeEff;
    public gameManager gameManager;
    public bool Canmove = true;
    [Header("Point")]
    public GameObject dmgpopupPoint;
    public GameObject ultiPoint;
   
    [Header("UI")]
    public GameObject EndMenu;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI LV;
    public GameObject canvas;
    public PlayerExp exp;
    public GameObject LowHealthDisplay;
    public GameObject GetDamage;

    public Image CDSkill2;
    public Image CDSkill3;
    public TextMeshProUGUI CountCD2;
    public TextMeshProUGUI CountCD3;
    [Header("Audio")]
    public AudioSource gameplayAudioSource;
    public AudioSource gameoverAudioSource;

  
    public FixedJoystick JoyStick;
    public bool isAttacking = false;

    public bool isVoicePlaying = false;

    // Update is called once per frame
    public void SetFilledAmount(float amount,Image CDSkill)
    {
        CDSkill.fillAmount=amount;
    }
    public void SetCdText(string Number, TextMeshProUGUI TextCD)
    {
        TextCD.text = Number;
    }
    public void StatusImplement(int status)
    {
        if (status == 1)
        {
            Cursor.visible = true;
            EndMenu.SetActive(true);
            SwitchToGameoverAudio();
            Time.timeScale = 0;
             
    //AudioManager Audio = new AudioManager();
    //Audio.SwitchToGameoverAudio();
    
        }
    }
    public void CheckSkill()
    {
        gameManager.DestroySkillPanel();
    }
    public void SetLV(int Lv)
    {
        if (Lv >= 2)
        {
            InstansiateLvUp();
            LV.text = "LV " + Lv;
        }else
            LV.text = "LV " + Lv;
    }
    public void setExp(float h)
    {
        exp.setExp(h);
    }
    public void setminExp(float min)
    {
        exp.setmin(min);
    }
    public void setmaxExp(float max)
    {
        exp.setMaxExp(max);
    }
    public void AudioAttack()
    {
        if (gameObject.name == "Player")
        {
            int random = Random.Range(1, 3);
            if (random == 1)
            {
                AudioManager.PlaySoundStatic("VoiceAttack1");
                StartCoroutine(Wait(2f));

            }
            else
            {
                AudioManager.PlaySoundStatic("VoiceAttack2");
                StartCoroutine(Wait(2f));

            }
        }
        else if (gameObject.name == "MalePlayer")
        {
            int random = Random.Range(1, 3);
            if (random == 1)
            {
                AudioManager.PlaySoundStatic("VoiceAttack1Male");
                StartCoroutine(Wait(2f));

            }
            else
            {
                AudioManager.PlaySoundStatic("VoiceAttack2Male");
                StartCoroutine(Wait(2f));

            }
        }

    }
    
    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        isVoicePlaying = false;
    }
    public void Dmgtaken(float attack)
    {
       GameObject text = Instantiate(DmgTaken,dmgpopupPoint.transform);
        text.GetComponent<TextMeshProUGUI>().text = "- "+attack.ToString();
    }
    public void playUltiAnimation(IEnumerator Animation)
    {
        StartCoroutine(Animation);
    }

    public void setHealth(PlayerHealthbar healthBar,float health)
    {
        healthBar.setHealth(health);
        
    }
    public void ViewSpawn(GameObject attackEffect,Transform transform)
    {
        Instantiate(attackEffect, gameObject.transform);
    }
    public void ViewSpawnDifferent(GameObject attackEffect, Vector3 position,Quaternion rotation)
    {
        Instantiate(attackEffect, position,rotation);
    }
    public void setScore(float score)
    {
        Score.text = score.ToString();
    }   
    public GameObject InstantiateBomb()
    {
        return Instantiate(Bomb, gameObject.transform.position, Quaternion.identity);
    }

    public void InstantiateBlackHole()
    {
        Instantiate(BlackHole, gameObject.transform.position, Quaternion.identity);
    }

    public void InstansiateLvUp()
    {
        Instantiate(LvUpPrefab, gameObject.transform.position, Quaternion.identity);
    }
    public void SwitchToGameoverAudio()
    {
        gameplayAudioSource.Stop();
        gameplayAudioSource.enabled = false;

        // Kích hoạt và phát background audio khi gameover
        gameoverAudioSource.enabled = true;
        gameoverAudioSource.Play();
    }
   

}
