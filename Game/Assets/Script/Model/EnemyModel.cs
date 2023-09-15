using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel
{
    public float health = 100;
    public float currentHealth;
    public float speed = 5f;
    public float reloadTime = 2;
    public float currentReloadTime = 0;
    public int count = 1;
    public float BulletDame = 5f;
    public float MeleeDame = 10f;
    public float detectionRange = 10f;
    public float Def=0;
    public float Exp = 0;
    public int Lv;
    public void SetEnemyModel(float health, float speed, float reloadTime, float detectionRange, float BulletDame, float MeleeDame, float _Def,float _Exp, int _Lv)
        
    {
        this.health = health;
        this.speed = speed;
        this.reloadTime = reloadTime;
        this.detectionRange = detectionRange;
        this.BulletDame = BulletDame;
        this.MeleeDame = MeleeDame;
        Def=_Def;
        Exp=_Exp;
        Lv = _Lv;
    }
}
