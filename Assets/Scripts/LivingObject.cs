using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObject : MonoBehaviour, IDamageable
{
    private int _maxLife;
    protected int _currentLife;
    private bool _isDead;

    public int MaxLife { get => _maxLife; set => _maxLife = value; }
    public int CurrentLife { get => _currentLife; set => _currentLife = value; }
    public bool IsDead { get => _isDead; set => _isDead = value; }

    public virtual void Die(IDamageable Cible)
    {
        Destroy(this.gameObject);
        
    }

    public void Heal()
    {
        throw new System.NotImplementedException();
    }

    public virtual void TakeDamage(int damage, IDamageable Attaquant)
    {
        _currentLife -= damage;
        Debug.Log(this.gameObject.name + _currentLife);

        if (_currentLife <= 0)
        {
            Die(this);
        }
    }
}
