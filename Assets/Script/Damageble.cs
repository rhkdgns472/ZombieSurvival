using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageble : MonoBehaviour
{

    private Queue<DamageSource> damageSources;

    public delegate void DSdel();
    public event DSdel OnDamageSourceTouched;

    void Start()
    {
        damageSources = new Queue<DamageSource>();    
    }

    public void EnqueueDamageSource(DamageSource source)
    {
        damageSources.Enqueue(source);
        if(OnDamageSourceTouched != null)
        {
            OnDamageSourceTouched.Invoke();
        }
    }

    public DamageSource GetNextDamageSource()
    {
        if(damageSources.Count == 0)
        {
            return null;
        }
        else
        {
            DamageSource nextSrc = damageSources.Peek();
            damageSources.Dequeue();
            return nextSrc;
        }
    }
}
