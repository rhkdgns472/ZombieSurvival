using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainUi : MonoBehaviour
{
    [SerializeField] private WeaponControl weaponController;
    [SerializeField] private ZombieStatManager zombieStatManager;
    [SerializeField] private PlayerStatManager playstat;
    [SerializeField] private GameUi gameUi;


    //[SerializeField] private Text timeDisplay;
    [SerializeField] private Text ammoDisplay;
    [SerializeField] private Text killDisplay;
    [SerializeField] private Text timeDisplay;
    [SerializeField] private Text hpDisplay; 
    public int kill;
    public float time;
    public bool timer=false;
    //[SerializeField] private float hp;
    TimeSpan t;

    void Update()
    {
        ammoDisplay.text = weaponController.NowWeapon.GetComponent<Weapon>().GetInfo();
        time -= Time.deltaTime;
        t = TimeSpan.FromSeconds(time);
        killDisplay.text = "KiLLS : " + kill;
        timeDisplay.text = "TIME : " + String.Format("{0:00}:{1:00}.{2:000}", t.Minutes, t.Seconds, t.Milliseconds);
        hpDisplay.text = "HP : " + playstat.Stats.Health;
        if(time <= 0 && !timer)
        {
            timer = true;
            gameUi.GameClear();
            gameUi.AddScore(20000);
        }
        
    }

    public void AddKill(int k)
    {
        kill += k;
        gameUi.AddScore(k*1000);
    }
}
