using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooting
{
    void Attack(IDamageable attaquant, IDamageable cible);
}
