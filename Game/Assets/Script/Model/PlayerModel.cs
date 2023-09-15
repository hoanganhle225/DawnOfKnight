
using UnityEngine;

public class PlayerModel 
{
    public int playerStatus = 0;
    public float moveSpeed = 5f ;
    public float BoostSpeed = 10f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    public float speed = 0f;
    public float chargeTime = 0f;
    public float health = 100;
    public float currentHealth;
    public float Dame = 40;
    public float Charged = 0f;
    public float Def =0;
    public float Exp = 0;
    public int score = 0;
    public float Atk = 0;
    public int Lv = 1;
    public void setModel(float moveSpeed,float jumpForce,float chargeTime,float health,float Dame,float speed,float _Exp,float _Def,float _Atk, int _Lv)
    {
        currentHealth = health;
        this.moveSpeed = moveSpeed;
        this.jumpForce = jumpForce;
        this.chargeTime = chargeTime;
        this.health = health;
        this.Dame = Dame;
        this.speed = speed;
        Def=_Def;
        Exp = _Exp;
        Atk = _Atk;
        Lv = _Lv;
    }

}
