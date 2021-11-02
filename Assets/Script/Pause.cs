using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool Paused = false;

    // Update is called once per frame
    void Update()
    {
        if(Paused == false && (Input.GetKeyDown(KeyCode.Escape)))
        {
            Time.timeScale = 0;
            Paused = true;

        }else if (Paused == true && (Input.GetKeyDown(KeyCode.Escape)))
        {
            Time.timeScale = 1;
            Paused = false;
        }
    }
}
