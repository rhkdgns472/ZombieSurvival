using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    [SerializeField] private KnifeData knife;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private ZombieStatManager zombiestat;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioAttack;
    [SerializeField] private GameUi gameUi;
    private Animator anim;
    private bool attacking;

    public bool Attacking { get { return attacking; } }

    void Start()
    {
        anim = GetComponent<Animator>();
        knife = Instantiate(knife);
        attacking = false;
        zombiestat.HitDamage(knife.DamageValue);
    }

    void Update()
    {
        DoAttackOnInput(Input.GetKeyDown(KeyCode.Mouse0));
        knife.TickClock(Time.deltaTime);
        if (!attacking && playerMove.IsMove)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }


    void DoAttackOnInput(bool keycodePresseDown)
    {
        if (keycodePresseDown && !knife.AttackInTimeout && !attacking && gameUi.start)
        {
            attacking = true;
            //aud.PlayClip("knife");
            anim.SetTrigger("AttackBtnPressed");
            StartCoroutine(Attack(0.1666667f));
        }
    }

    public override FirearmStats GetFirearmData()
    {
        return null;
    }

    IEnumerator Attack(float delay)
    {
        float y_position = 1.271f;
        //float animClipLength = GetAnimationClip("knife_meleeattack").length;
        float y_bound_size = 1.457f;
        float secondsPerStep = (0.4833f - 0.1666667f) / 40f;
        float stepAmount = y_bound_size / 40f + 0.093f;
        float elapsedTime = 0;
        Transform hitbox = gameObject.GetComponent<Transform>().GetChild(0);
        audioSource.clip = audioAttack;
        audioSource.Play();
        yield return new WaitForSeconds(delay);

        SetHitBox(true);

        while (y_position > -1.083f)
        {
            if (elapsedTime >= secondsPerStep)
            {
                elapsedTime = 0;
                y_position -= stepAmount;
                float x_position = KnifePositionOverTime(y_position);


                hitbox.localPosition = new Vector2(x_position, y_position);

                if (y_position <= -1.083f)
                {
                    y_position = -1.083f;
                }
            }


            elapsedTime += Time.deltaTime;
            yield return null;
        }
        hitbox.localPosition = Vector2.zero;
        SetHitBox(false);
        attacking = false;


    }

    float KnifePositionOverTime(float y)
    {
        if (y < -1.083f)
        {
            return 0.683f;
        }
        else if (y < -0.185f)
        {
            return 1.29f - Mathf.Pow(10, -y - 1.3f);
        }
        else if (y <= 1.271f)
        {
            return 1.25f - Mathf.Pow(10, y - 1.25f);
        }
        else if (y > 1.271f)
        {
            return 0.2f;
        }
        else
        {
            return -1;
        }
    }

    void SetHitBox(bool status)
    {
        transform.GetChild(0).gameObject.SetActive(status);
    }

    public override string GetInfo()
    {
        return "Knife";
    }
}
