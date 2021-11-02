using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Firearm
{
    protected override void DoAttackOnInput(bool keycodePressedDown)
    {
        bool needToReload = (firearmStats.CurrentMagazineCapacity == 0);

        if(keycodePressedDown && !needToReload && !firearmStats.AttackInTimeout && !reloading)
        {
            Vector3 aimControllerPosition = aimController.transform.position;
            Vector2[] bulletTravelDirections = new Vector2[3];
            Transform[] bulletClones = new Transform[3];
            Rigidbody2D[] bulletCloneRBs = new Rigidbody2D[3];

            bulletTravelDirections[0] = RotateVetor(aimController.LookDirection, 3);
            bulletTravelDirections[1] = aimController.LookDirection;
            bulletTravelDirections[2] = RotateVetor(aimController.LookDirection, -3);

            bulletClones[0] = Instantiate(bullet);
            bulletClones[1] = Instantiate(bullet);
            bulletClones[2] = Instantiate(bullet);

            bulletCloneRBs[0] = bulletClones[0].GetComponent<Rigidbody2D>();
            bulletCloneRBs[1] = bulletClones[1].GetComponent<Rigidbody2D>();
            bulletCloneRBs[2] = bulletClones[2].GetComponent<Rigidbody2D>();

            anim.SetTrigger("shot");
            //audioManager.PlayClip(attackAudioClipName);
            audioSource.clip = audioAttack;
            audioSource.Play();
            muzzle_flash.gameObject.SetActive(true);
            firearmStats.AddToCurrentMagCapacity(-1);
            firearmStats.AttackTimeoutClock.ResetClock();

            for(int i =0; i <3; i++)
            {
                bulletClones[i].gameObject.SetActive(true);
                bulletClones[i].GetComponent<DamageSource>().SetDamageValue(firearmStats.DamageValue);
                bulletClones[i].position = aimControllerPosition;
                bulletCloneRBs[i].velocity = bulletTravelDirections[i] * speed;
            }
        }
        else
        {
            muzzle_flash.gameObject.SetActive(false);
        }
    }

    Vector2 RotateVetor(Vector2 v, float degrees)
    {
        Vector2 rotated;
        degrees *= Mathf.Deg2Rad;
        rotated.x = v.x * Mathf.Cos(degrees) - v.y * Mathf.Sin(degrees);
        rotated.y = v.x * Mathf.Sin(degrees) + v.y * Mathf.Cos(degrees);
        return rotated;
    }
}
