using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooting
{
    void Shoot(IDamageable tireur, IDamageable cible);
}
