using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Knife", menuName ="Knife")]
public class KnifeData : ScriptableObject
{
    [SerializeField] private float damageValue;
    [SerializeField] private float secondsBetWeenAttacks;

    private Clock attackTimeoutClock;

    public bool AttackInTimeout { get; private set; }

    public float DamageValue { get { return damageValue; } }
    public float SecondsBetweenAttacks { get { return secondsBetWeenAttacks; } }

    void Awake()
    {
        attackTimeoutClock = new Clock(secondsBetWeenAttacks, secondsBetWeenAttacks);
        AttackInTimeout = false;
    }

    public override string ToString()
    {
        return "Knife";
    }

    public void TickClock(float dt)
    {
        attackTimeoutClock.Tick(dt);

        if(attackTimeoutClock.ReachedDesiredWaitTime())
        {
            AttackInTimeout = false;
        }
        else
        {
            AttackInTimeout = true;
        }
    }
}
