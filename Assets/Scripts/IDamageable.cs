using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage, IDamageable Attaquant);

    void Die(IDamageable Cible);

    void Heal();
}
