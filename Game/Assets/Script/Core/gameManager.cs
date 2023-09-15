using TMPro;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class gameManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text Score;
    bool RightMouse = false;
    bool Boosting = false;
    public GameObject playerPrefabM;
    public GameObject playerPrefabF;
    private PlayerModel playerModel;
    public PlayerController playerController;
    private Gethit hit = new Gethit();
    private PlayerStat playerData = new PlayerStat();
    public Spawner spawner;
    public bool isSelected = false;
    public GameObject GenderSelect;
    public FixedJoystick joystick;
    public Button attackButton;
    public Button skillButton1;
    public Button skillButton2;
    public Button skillButton3;
    public GameObject LockSKill2;
    public GameObject LockSKill3;
    public PlayerExp exp;
    private bool innited = false;
    public GameObject Skill2Panel;
    public GameObject Skill3Panel;

    // Start is called before the first frame update
    public static gameManager instance;
    private SettingsManager setting = new SettingsManager();
    private async void Start()
    {
        instance = this;
        Application.targetFrameRate = 60;
        ReadDataPlayer();
        Init();
    }

    public void Init()
    {
        attackButton.onClick.AddListener(OnAttackButtonClick);
        skillButton1.onClick.AddListener(OnSkill1ButtonUp);
        skillButton2.onClick.AddListener(OnSkill2ButtonUp);
        skillButton3.onClick.AddListener(OnSkill3ButtonUp);
        skillButton2.interactable = false;
        skillButton3.interactable = false;
    }

    private async void ReadDataPlayer()
    {
        playerData.SetData();
        InstantiatePlayer();
    }

    public void OnSkill2ButtonUp()
    {
        playerController.Skill_2();
    }

    public void OnSkill3ButtonUp()
    {
        playerController.Skill_3();
    }

    private void OnAttackButtonClick()
    {
        playerController.Attack();
    }

    public void OnSkill1ButtonDown()
    {
        RightMouse = true;
    }

    public void OnSkill1ButtonUp()
    {
        RightMouse = false;
    }

    public static float PlayerAtk()
    {
        return instance.playerController.m.Atk;
    }

    public static void enemyDead(float exp)
    {
        instance.handleEnemyDead(exp);
    }

    public static int currentPlayerLv()
    {
        return instance.currentPlayerlv();
    }

    private int currentPlayerlv()
    {
        return playerController.m.Lv;
    }

    public void handleEnemyDead(float exp)
    {
        playerController.HandleExp(exp);
        AddScore();
    }

    public void DestroySkillPanel()
    {
        if (Skill2Panel != null && playerController.m.Lv>=2)
        {
            skillButton2.interactable = true;
            Destroy(LockSKill2);
            Skill2Panel.SetActive(true);
            Time.timeScale = 0;
        }
    
        if (Skill3Panel != null && playerController.m.Lv >= 3)
        {
            skillButton3.interactable = true;
            Destroy(LockSKill3);
            Skill3Panel.SetActive(true);
            Time.timeScale = 0;
        }
       
    }

    public void CloseSkill2()
    {
      
        Destroy(Skill2Panel);
        Time.timeScale = 1;
       

    }

    public void CloseSkill3()
    {
        
        Destroy(Skill3Panel);
        Time.timeScale = 1;

    }

    void Update()
    {
        if (!innited) return;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            playerController.HandleInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), RightMouse, Input.GetKey(KeyCode.LeftShift), Input.GetKey(KeyCode.F));
        }
        else
        {
            playerController.HandleInput(joystick.Horizontal, joystick.Vertical, RightMouse, Boosting, Input.GetKey(KeyCode.F));
        }

        

        playerController.CheckHpStatus();
    }

    private async void InstantiatePlayer()
    {
        playerController = new PlayerController();
        playerModel = new PlayerModel();

        playerModel.setModel(playerData.playerData.moveSpeed, playerData.playerData.jumpForce, playerData.playerData.chargeTime,
            playerData.playerData.health, playerData.playerData.Dame, playerData.playerData.speed, playerData.playerData.Exp,
            playerData.playerData.Def, playerData.playerData.Atk, playerData.playerData.Lv);
        playerController.SetModel(playerModel);
        await Task.Delay(500);

        if (setting.LoadGender() == "Male")
        {
            playerPrefabM.SetActive(true);
            SetDataplayer(playerPrefabM);
        }
        else if (setting.LoadGender() == "Female")
        {
            playerPrefabF.SetActive(true);
            SetDataplayer(playerPrefabF);
        }

        spawner.setLV();
        innited = true;
    }

    public void SetDataplayer(GameObject prefab)
    {
        playerController.SetView(prefab.GetComponent<PlayerView>());
        hit = prefab.gameObject.AddComponent<Gethit>();
        hit.OnDamageTaken += playerController.HandleDamageTaken;

        playerController.v.setHealth(playerController.v.healthBar, playerData.playerData.currentHealth);
        playerController.m.currentHealth = playerData.playerData.currentHealth;
        playerController.StatUpdate(playerData.playerData.Lv.ToString());
        playerController.v.SetLV(playerController.m.Lv);

        playerController.SetExpBar(exp);
        playerController.setDefaultExp();
       

        if (playerController.m.Lv >= 2)
        {
            DestroySkillPanel();
        }
    }

    public void AddScore()
    {
        score += 100;
        Score.text = score.ToString();
    }
}