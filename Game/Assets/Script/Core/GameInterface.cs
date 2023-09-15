using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusCharacter 
{
    Attack,
    Moving,
    Idling,
    Die
}

public interface IAttack
{
    void SetData(IData data);
    List<ICharacter> DetectTarget();
    void AttackTarget();
}

public interface IData
{

}

public interface IDamage
{
    void Take(float value);
   
}

/*public interface ICharacter
{
    void SetData(IData data);
    void Create();
    StatusCharacter GetStatus();
}*/

public interface ICharacter
{
    void SetData();
    void SetStatus(int Status);
    void HandleDamageTaken(float value = 0);
    void Create();
}

public interface IEnemy
{
    int HP { get; set; }
    float RangeDetect { get; set; }
   
    void MeeleAttack();
    void GetHit(int value = 0);
    void RangeAttack();
}

public interface ICamera
{
   /* void FollowCharacter();
    void RotationAroundCharacter();*/
}


