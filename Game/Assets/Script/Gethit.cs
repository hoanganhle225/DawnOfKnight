using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gethit : MonoBehaviour,IDamage
{
        public event Action<float> OnDamageTaken; // Event khi nhận sát thương
        public void Take(float damageAmount)
        {
            // Gọi event OnDamageTaken để thông báo cho Controller
            OnDamageTaken?.Invoke(damageAmount);
        }
    
}
