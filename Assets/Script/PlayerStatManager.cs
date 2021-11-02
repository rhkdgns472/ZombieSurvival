using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    [SerializeField] private PlayerStat stats;
    [SerializeField] private ZombieStatManager zombieStat;
    [SerializeField] private GameObject player;
    bool touch = false;
    public bool Death = false;
    public PlayerStat Stats { get { return stats; } }
    // Start is called before the first frame update
    void Start()
    {
        stats = Instantiate(stats);
        Debug.Log(stats.Health);
    }

    // Update is called once per frame
    void Update()
    {
        if(stats.Health <= 0)
        {
            Death = true;
            Destroy(player);
        }
    }

    public void ZombieAttack(float a , bool Attack)
    {
        if(touch == true && Attack == true)
        {
            stats.AddHealth(-a);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Zombie")
        {
            touch = true;
            //Debug.Log("안임");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Zombie")
        {
            touch = false;
            //Debug.Log("밖임");
        }
    }
}
