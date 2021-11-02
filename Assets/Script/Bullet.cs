using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : DamageSource
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (transform.tag != col.transform.tag)
        {
            Damageble damageable = col.GetComponentInChildren<Damageble>();
            if (damageable != null)
            {
                damageable.EnqueueDamageSource(this);
            }
        }

        if(col.tag == "Wall" || col.tag == "Zombie")
        {
            Destroy(gameObject);
        }
    }
}
