using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firearm : Weapon
{
    [SerializeField] public FirearmStats firearmStats;
    [SerializeField] protected float speed = 100f;
    [SerializeField] protected PlayerAim aimController;
    [SerializeField] protected PlayerMove movementController;
    [SerializeField] private ZombieStatManager zombieStatManager;
    //[SerializeField] protected AudioManager audioManager;
    //[SerializeField] protected string attackAudioClipName;
    //[SerializeField] protected string reloadAudioClipName;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip audioAttack;
    [SerializeField] protected AudioClip audioReload;
    protected Animator anim;
    public Transform bullet;
    //public Transform pos;
    public Transform muzzle_flash;
    protected bool reloading;
    public bool Reloading { get { return reloading; } }

    void Start()
    {
        anim = GetComponent<Animator>();
        bullet = transform.GetChild(0);
        muzzle_flash = transform.GetChild(1);
        bullet.gameObject.SetActive(false);
        muzzle_flash.gameObject.SetActive(false);

    }

    void Awake()
    {
        firearmStats = Instantiate(firearmStats);
        reloading = false;
        audioSource.playOnAwake = false;
    }
    void Update()
    {
        //zombieStatManager.HitDamage(firearmStats.DamageValue);
        //Debug.Log(firearmStats.DamageValue);
        DoAttackOnInput(Input.GetKey(KeyCode.Mouse0));
        ReloadWeaponOnInput(Input.GetKeyDown(KeyCode.R));
        firearmStats.TickClocks(Time.deltaTime);

        if(!reloading && movementController.IsMove)
        {
            anim.SetBool("move",true);
        }else
        {
            anim.SetBool("move",false);
        }
    }

    public override FirearmStats GetFirearmData()
    {
        return firearmStats;
    }

    void ReloadWeaponOnInput(bool keycodePressedDown)
    {
        bool magazineIsFull = (firearmStats.MaxMagazineCapacity - firearmStats.CurrentMagazineCapacity) == 0;
        bool TotalMagIsEmpty = (firearmStats.TotalAmmo == 0);

        if (!reloading && keycodePressedDown && !magazineIsFull && !TotalMagIsEmpty)
        {
            anim.SetTrigger("reload");
            //Debug.Log(reloadAudioClipName);
            //audioManager.PlayClip(reloadAudioClipName);
            audioSource.clip = audioReload;
            audioSource.Play();
            StartCoroutine(ReloadWeaponOnInput(firearmStats.ReloadTimeInSeconds));
            reloading = true;
        }

    }

    IEnumerator ReloadWeaponOnInput(float delay)
    {
        yield return new WaitForSeconds(delay);
        int amountToTakeFromTotal = Mathf.Min((firearmStats.MaxMagazineCapacity - firearmStats.CurrentMagazineCapacity), firearmStats.TotalAmmo);
        firearmStats.AddToCurrentMagCapacity(amountToTakeFromTotal);
        firearmStats.AddToTotalAmmo(-amountToTakeFromTotal);
        firearmStats.ReloadClock.ResetClock();
        reloading = false;
    }

    virtual protected void DoAttackOnInput(bool keycodePressedDown)
    {
        bool needToReload = (firearmStats.CurrentMagazineCapacity == 0);

        if (keycodePressedDown && !needToReload && !firearmStats.AttackInTimeout && !reloading)
        {
            Vector2 bulletTravelDirection = aimController.LookDirection;
            Vector3 aimControllerPosition = aimController.transform.position;
            Transform bulletClone = Instantiate(bullet);
            Rigidbody2D rb = bulletClone.GetComponent<Rigidbody2D>();
            DamageSource damageSource = bulletClone.GetComponent<DamageSource>();

            anim.SetTrigger("shot");
            //audioManager.PlayClip(attackAudioClipName);
            audioSource.clip = audioAttack;
            audioSource.Play();
            muzzle_flash.gameObject.SetActive(true);
            firearmStats.AddToCurrentMagCapacity(-1);
            firearmStats.AttackTimeoutClock.ResetClock();

            bulletClone.gameObject.SetActive(true);
            damageSource.SetDamageValue(firearmStats.DamageValue);

            //Debug.Log(firearmStats.DamageValue);
            bulletClone.position = aimControllerPosition;
            rb.velocity = bulletTravelDirection * speed;
        }
        else
        {
            muzzle_flash.gameObject.SetActive(false);
        }
    }

    public override string GetInfo()
    {
        return $"{transform.name}: {firearmStats.CurrentMagazineCapacity}/{firearmStats.TotalAmmo}";
    }
}
