using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFists : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private PlayerStatManager playerstat;
    [SerializeField] private ZombieStatManager zombiestat;
    private Animator anim;
    bool attack = false;
    //private bool attacking;
    //private bool isTouching;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        //playerstat = GetComponent<PlayerStatManager>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && attack == false)
        {
            attack = true;
            //Debug.Log("´êÀ½");
            anim.SetTrigger("Attack");
            StartCoroutine(Attack());
            //Debug.Log("µô ·Î±×");
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.7f);
        playerstat.ZombieAttack(10, attack);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.7f);
        attack = false;
    }
}
