using System.Collections;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : ICharacter
   
{
    [Header("Data Setup")]
    public PlayerLvUpStat Stat = new PlayerLvUpStat();
    public PlayerModel m = new PlayerModel();
    public PlayerView v = new PlayerView();
    
    public PlayerLevelData playerData;
    [Header("Skill atribute")]
    public float BombSkillRLDTime = 5;
    public float CurrentTimeBombRLD = 0;
    public float BlackHoleRLDTime = 5f;
    public float CurrentTimeBlackHoleRLD = 0f;
    public void SetModel(PlayerModel model)
    {
        m = model;
        Stat.LoadPlayerLvUpData();

    }
    
    public void SetView(PlayerView view)
    {
        v = view;
       
    }
    public void SetStatus(int Status)
    {
        m.playerStatus = Status;
        v.StatusImplement(m.playerStatus);
    }
    public void SetExpBar(PlayerExp exp)
    {
        v.exp=exp;
    }
    float DmgTakenCalcaculate(float RawAtk)

    {
        float DmgRec = m.Def / (10 + m.Def);
        return RawAtk * (1 - DmgRec);
    }
    public void DeleteSave()
    {

        string fileName = "Player.json";
        string filePath = Application.persistentDataPath + "/" + fileName;
        File.Delete(filePath);
        fileName = "spawner.json";
        filePath = Application.persistentDataPath + "/" + fileName;
        File.Delete(filePath);
    }
    public void HandleDamageTaken(float damageAmount)
    {
        // Xử lý logic khi nhận sát thương
        m.currentHealth -= DmgTakenCalcaculate(damageAmount);
        v.setHealth(v.healthBar, m.currentHealth);
        v.Dmgtaken((int)DmgTakenCalcaculate(damageAmount));
        if (m.currentHealth <= 0)
        {
            SetStatus(1);
            DeleteSave();
        }

        v.StartCoroutine(ToggleObjectWithDelay(0.5f));
        // Thực hiện các hành động khác liên quan đến sát thương
    }
    public void HandleExp(float _Exp)
    {
        m.Exp += _Exp;
        v.setExp(m.Exp);
        if (m.Lv + 1 > 35) return;
        else
        if (m.Exp >= Stat.playerLvUpData[(m.Lv+1).ToString()].Exp)
        {
            setExp();
            m.Lv = m.Lv + 1;
            StatUpdate(m.Lv.ToString());
            v.SetLV(m.Lv);
            v.CheckSkill();
        }
       
    }
    public void setDefaultExp()
    {
        if ((m.Lv + 2) > 35)
        {
            v.setminExp(Stat.playerLvUpData[(35).ToString()].Exp);
            v.setmaxExp(999999999);
        }
        else
        {
            v.setminExp(Stat.playerLvUpData[(m.Lv).ToString()].Exp);
            v.setmaxExp(Stat.playerLvUpData[(m.Lv + 1).ToString()].Exp);
        }
        
    }
    public void setExp()
    {
        if ((m.Lv + 2) > 35)
        {
            v.setminExp(Stat.playerLvUpData[(35).ToString()].Exp);
            v.setmaxExp(9999999999);
        }
        else
        {
            v.setminExp(Stat.playerLvUpData[(m.Lv + 1).ToString()].Exp);
            v.setmaxExp(Stat.playerLvUpData[(m.Lv + 2).ToString()].Exp);
        }
    }
    public void StatUpdate(string lv)
    {
        
        
        playerData = Stat.playerLvUpData[lv];
        
        m.Atk = playerData.Atk;
        m.Def = playerData.Def;
        m.currentHealth = Calcaculate(m.health, m.currentHealth) * playerData.Hp;
        m.health = playerData.Hp;
       
        v.healthBar.setMaxHealth(m.health);
        v.healthBar.setHealth(m.currentHealth);
    }

    public void CheckHpStatus()
    {
        if (Calcaculate(m.health, m.currentHealth) - 0.25f <= 0.3f)
        {
            v.LowHealthDisplay.SetActive(true);

        }
        else
        {
            v.LowHealthDisplay.SetActive(false);
        };

    }
    public float Calcaculate(float max,float current)
    {
        float percent = (current / max)+0.25f;
        if (percent > 1) percent = 1;
        return percent;
    }
    private void CDSkillHandler()
    {
        if (CurrentTimeBlackHoleRLD > 0)
        {
            CurrentTimeBlackHoleRLD -= Time.deltaTime;
            v.SetFilledAmount((CurrentTimeBlackHoleRLD / BlackHoleRLDTime), v.CDSkill3);

            v.SetCdText(((int)CurrentTimeBlackHoleRLD).ToString(), v.CountCD3);
            if(CurrentTimeBlackHoleRLD < 0.3f)
                v.SetCdText("", v.CountCD3);

        }
        if (CurrentTimeBombRLD > 0)
        {
            CurrentTimeBombRLD -= Time.deltaTime;
            v.SetFilledAmount((CurrentTimeBombRLD/BombSkillRLDTime), v.CDSkill2);

            v.SetCdText(((int)CurrentTimeBombRLD).ToString(), v.CountCD2);

            if (CurrentTimeBombRLD < 0.3f)
                v.SetCdText("", v.CountCD2);
        }
    }
    public void HandleInput(float horizontalInput, float verticalInput, bool isRightMouseButtonDown, bool isBoosting, bool FKey)
    {
        CDSkillHandler();
        bool isAttacking = v.animator.GetBool("attack");
        //Vector3 cameraForward;

        Vector3 movementJS = new Vector3(v.JoyStick.Horizontal, 0f, v.JoyStick.Vertical);
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (FKey)
        {
            if (CurrentTimeBombRLD <= 0)
                v.InstantiateBomb();
        }

        if (isRightMouseButtonDown)
        {
            Skill_1();
        }
        else
        {
            if (isAttacking)
            {
                v.animator.SetBool("Run", false);
                m.moveSpeed = m.speed / 2f;
                v.WeaponCollider.enabled = true;
            }
            else
            {
                v.WeaponCollider.enabled = false;
                if (isBoosting && (horizontalInput != 0f || verticalInput != 0f))
                {
                    m.moveSpeed = m.speed * 2;
                    v.animator.SetBool("Run", true);
                }
                else
                {
                    m.moveSpeed = m.speed;
                    v.animator.SetBool("Run", false);

                }
            }
            v.animator.SetBool("Charge", false);


            if (horizontalInput != 0f || verticalInput != 0f)
            {
                v.animator.SetBool("Walk", true);
                v.animator.SetBool("Idle", false);
            }
            else
            {
                v.animator.SetBool("Walk", false);
                v.animator.SetBool("Idle", true);
            }
            if (v.Canmove)
            {
                if (movementJS.magnitude > 0f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(movementJS); // Xác định hướng xoay mới
                    v.transform.rotation = Quaternion.Lerp(v.transform.rotation, targetRotation, m.rotationSpeed * Time.deltaTime); // Áp dụng xoay cho model nhân vật

                    v.rb.MovePosition(v.rb.position + (movementJS * m.moveSpeed * Time.deltaTime));

                }
                if (movement.magnitude > 0f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(movement); // Xác định hướng xoay mới
                    v.transform.rotation = Quaternion.Lerp(v.transform.rotation, targetRotation, m.rotationSpeed * Time.deltaTime); // Áp dụng xoay cho model nhân vật

                    v.rb.MovePosition(v.rb.position + (movement * m.moveSpeed * Time.deltaTime));
                }

            }
                if (m.Charged >= m.chargeTime)
                {
                    v.playUltiAnimation(AnimationUlti());


                }
                if (m.Charged < m.chargeTime)
                    v.ChargeEff.SetActive(false);
                m.Charged = 0;
            }

        
    }

        public void Skill_2()
        {
            if (CurrentTimeBombRLD <= 0)
            {
                GameObject bombobject = v.InstantiateBomb();
                Bomb Bomb = bombobject.GetComponent<Bomb>();
                Bomb.SetAtk(m.Atk);
                CurrentTimeBombRLD = BombSkillRLDTime;
                v.SetFilledAmount(1, v.CDSkill2);
            }
        }
        public void Skill_3()
        {
            if (CurrentTimeBlackHoleRLD <= 0)
            {
                v.InstantiateBlackHole();
                CurrentTimeBlackHoleRLD = BlackHoleRLDTime;
                 v.SetFilledAmount(1, v.CDSkill3);
        }
        }
        
        public void Skill_1()
        {
            Vector3 movementJS = new Vector3(v.JoyStick.Horizontal, 0f, v.JoyStick.Vertical);
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            v.animator.SetBool("Run", false);
            m.moveSpeed = m.speed / 2f;

            // Di chuyển nhân vật
            v.animator.SetBool("Charge", true);
            m.Charged += Time.deltaTime;
            if (m.Charged >= m.chargeTime)
            {
                v.ChargeEff.SetActive(true);

            }
        if (v.Canmove)
        {
            if (movement.magnitude > 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement); // Xác định hướng xoay mới
                v.transform.rotation = Quaternion.Lerp(v.transform.rotation, targetRotation, m.rotationSpeed * Time.deltaTime); // Áp dụng xoay cho model nhân vật
                v.rb.MovePosition(v.rb.position + (movement * m.moveSpeed * Time.deltaTime));
            }

            if (movementJS.magnitude > 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementJS); // Xác định hướng xoay mới
                v.transform.rotation = Quaternion.Lerp(v.transform.rotation, targetRotation, m.rotationSpeed * Time.deltaTime); // Áp dụng xoay cho model nhân vật
                v.rb.MovePosition(v.rb.position + (movementJS * m.moveSpeed * Time.deltaTime));
            }
        }

            if (movement.magnitude != 0)
            {
                v.animator.SetBool("Walk", true);
                v.animator.SetBool("Idle", false);
            }
            else
            {
                v.animator.SetBool("Walk", false);
                v.animator.SetBool("Idle", true);
            }
        }
    
        public void Attack()
        {
            if (!v.isVoicePlaying)
            {
                v.isVoicePlaying = true;
                v.AudioAttack();
            }

            v.WeaponCollider.enabled = true;
            v.animator.SetTrigger("attack");
        }
        public IEnumerator AnimationUlti()
        {
            m.Charged = 0;
            v.animator.SetTrigger("Released");
            if (v.gameObject.name == "Player")
            {
                AudioManager.PlaySoundStatic("UltiVoice");
            }
            else if (v.gameObject.name == "MalePlayer")
            {
                AudioManager.PlaySoundStatic("UltiVoiceMale");
            }

            yield return new WaitForSeconds(0.8f);
            v.ViewSpawnDifferent(v.ultiPrefab, v.ultiPoint.transform.position, v.ultiPoint.transform.rotation);
        }
    private IEnumerator ToggleObjectWithDelay(float delayInSeconds)
    {
        // Bật GameObject
        v.GetDamage.SetActive(true);

        // Đợi trong 'delayInSeconds' giây
        yield return new WaitForSeconds(delayInSeconds);

        // Tắt GameObject
        v.GetDamage.SetActive(false);
    }

        public void takeDmg(float Dmg)
        {
            m.health -= Dmg;
            v.healthBar.setHealth(m.health);
            

        }

        public void Create()
        {
            throw new System.NotImplementedException();
        }

        public void SetData()
        {
            throw new System.NotImplementedException();
        }
    
}