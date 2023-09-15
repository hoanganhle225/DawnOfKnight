using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public gameManager gameManager;
    public Spawner spawner;
    private string fileName = "Player.json";
    private string content;
    private PlayerController c;
    public PlayerData playerData = new PlayerData();
    public void Savegame()
    {
        c = gameManager.playerController;
        setPlayerData();
      

        string filePath = Application.persistentDataPath + "/" + fileName;
           string json = JsonConvert.SerializeObject(playerData);
            File.WriteAllText(filePath, json);
      
    }

    private void setPlayerData()
    {

        playerData.Atk=c.m.Atk;
        playerData.Def = c.m.Def;
        playerData.health=c.m.health;
        playerData.currentHealth=c.m.currentHealth;
        playerData.Exp=c.m.Exp;
        playerData.moveSpeed=c.m.moveSpeed;
        playerData.jumpForce = c.m.jumpForce;
        playerData.chargeTime = c.m.chargeTime;
        playerData.Dame=c.m.Dame;
        playerData.speed = c.m.speed;
        playerData.Atk = c.m.Atk;
        playerData.Lv = c.m.Lv;
    
}
}
