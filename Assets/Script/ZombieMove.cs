using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
enum WandererBehavior_SM { Start, GoToRandomLocation, Wait, Chase, ExtendedChase }
public class ZombieMove : MonoBehaviour
{
    [SerializeField] private PlayerStatManager playstat;
    ///Rigidbody2D rb;
    public Transform target;
    public float move = 2.0f;
    private Animator anim;
    private bool attack = false;
    void Start()
    {
        //rb = transform.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (attack == false)
        {
            ZombieMoving();
        }

        LookAt(target.transform.position);
        anim.SetBool("Moving", true);
    }
    void ZombieMoving()
    {
        transform.parent.position = Vector2.MoveTowards(transform.parent.position, target.position, move * Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Debug.Log("À¸¾Ç");
            Vector2 v2 = (transform.parent.position - target.transform.position).normalized;
            //rb.AddForce(v2 * 2.0f, ForceMode2D.Impulse);
            transform.parent.Translate(v2 * 0.35f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && attack == false)
        {
            attack = true;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        attack = false;
    }

    void LookAt(Vector2 target)
    {
        float dx = target.x - transform.position.x;
        float dy = target.y - transform.position.y;
        float angle = Mathf.Atan(Mathf.Abs(dy / dx)) * Mathf.Rad2Deg;
        bool mouseInQuadrant2 = dx < 0f && dy > 0f;
        bool mouseInQuadrant3 = dx < 0 && dy < 0;
        bool mouseInQuadrant4 = dx > 0 && dy < 0;

        if (mouseInQuadrant2)
        {
            angle = 90 + (90 - angle);
        }
        else if (mouseInQuadrant3)
        {
            angle += 180;
        }
        else if (mouseInQuadrant4)
        {
            angle = 270 + (90 - angle);
        }

        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
