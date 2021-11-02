using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerStatManager playstat;
    private Camera cam;
    //float v;
    void Start()
    {
        cam = GetComponent<Camera>();
        //v = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(playstat.Death == false)
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }

        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayerPrefs.SetFloat("s", v);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetFloat("s"));
        }else if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X누름");
            float g;
            g = PlayerPrefs.GetFloat("s");
            Debug.Log("s에 저장된 값 가져옴 : " + g);
        }else if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("있는가? " + PlayerPrefs.HasKey("s"));
        }else if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerPrefs.DeleteAll();
        }*/
    }
}
