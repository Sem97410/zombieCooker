using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEvents : MonoBehaviour
{
    public void AttackPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<LivingObject>().TakeDamage(GetComponentInParent<Zombie>().Damage, GetComponentInParent<IDamageable>());
            }
        }
    }
}
