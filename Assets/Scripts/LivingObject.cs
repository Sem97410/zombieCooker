using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObject : MonoBehaviour, IDamageable
{
    private int _maxLife;
    private int _currentLife;
    private bool _isDead;

    public int MaxLife { get => _maxLife; set => _maxLife = value; }
    public int CurrentLife { get => _currentLife; set => _currentLife = value; }
    public bool IsDead { get => _isDead; set => _isDead = value; }

    public void Die(IDamageable Cible)
    {
        throw new System.NotImplementedException();
    }

    public void Heal()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage, IDamageable Attaquant)
    {
        throw new System.NotImplementedException();
    }
}
