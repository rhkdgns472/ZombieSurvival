using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponControl : MonoBehaviour
{
    public Dictionary<string, Transform> weaponsDict;
    public List<Transform> weaponsList;
    public Transform NowWeapon { get; private set;}
    private void Awake()
    {
        Transform[] weapons = GetComponentsInChildren<Transform>();
        weaponsList = new List<Transform>();
        weaponsDict = new Dictionary<string, Transform>();

        for(int i=1; i< weapons.Length; i++)
        {
            if(weapons[i].parent == transform)
            {
                weaponsList.Add(weapons[i]);
            }
        }

        foreach(Transform weapon in weaponsList)
        {
            weaponsDict.Add(weapon.name, weapon);
        }
        NowWeapon = weaponsDict["knife"];
    }
    // Update is called once per frame
    void Update()
    {
        Firearm firearm = NowWeapon.GetComponent<Firearm>();
        Knife knife = NowWeapon.GetComponent<Knife>();
        if((firearm != null && firearm.Reloading) || (knife != null && knife.Attacking))
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            NowWeapon = weaponsDict["knife"];
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            NowWeapon = weaponsDict["handgun"];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            NowWeapon = weaponsDict["rifle"];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            NowWeapon = weaponsDict["shotgun"];
        }

        UpdateNowWeaponInScene();

        }
    void UpdateNowWeaponInScene()
    {
        foreach (Transform weapon in weaponsList)
        {
            if (weapon == NowWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }
}

