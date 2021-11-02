using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatManager : MonoBehaviour
{
    [SerializeField] private ZombieStat stats;
    [SerializeField] private Damageble damageable;
    [SerializeField] private DamageSource damageSource;
    [SerializeField] private MainUi Ui;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;
    public GameObject Zombie;
    public Transform blood;
    public float knifehit;

    public ZombieStat Stats { get { return stats; } }

    void Start()
    {
        stats = Instantiate(stats);
        //Debug.Log("Å¸°Ý");
        StartCoroutine(MakeNoise());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(knifehit);
    }
    public void HitDamage(float hitdamage)
    {
        knifehit = hitdamage;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            DamageSource nextDamageSource = damageable.GetNextDamageSource();
            stats.AddHealth(-nextDamageSource.DamageValue);
            StartCoroutine(BloodSeconds(0.3f));
            Debug.Log(stats.Health);
        }else if (col.gameObject.tag == "Knife")
        {
            stats.AddHealth(-knifehit);
            Debug.Log(stats.Health);
            StartCoroutine(BloodSeconds(0.3f));
        }

        if (stats.Health <= 0)
        {
            Ui.AddKill(1);
            StartCoroutine(DeathTimer());
        }
    }

    IEnumerator BloodSeconds(float second)
    {
        //Debug.Log("À¸¾Ç");
        blood.gameObject.SetActive(true);
        yield return new WaitForSeconds(second);
        blood.gameObject.SetActive(false);

    }
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(Zombie);
    }

    IEnumerator MakeNoise()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int randomIndex = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[randomIndex];
        audioSource.Play();
        yield return new WaitForSeconds(7f);
    }

}
