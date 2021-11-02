using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] protected Transform objectToSpawn;
    float ran;
    private float t;
    void Start()
    {
        ran = Random.Range(0.8f, 2.5f);
    }


    void Update()
    {
        t += Time.deltaTime;
        if (t >= ran)
        {
            t = 0;
            SpawnObject();
            ran = Random.Range(0.8f, 2.5f);
        }
    }


    protected Transform GetRandomSpawnTransform()
    {
        return transform.GetChild(Random.Range(0, transform.childCount));
    }

    virtual protected void SpawnObject()
    {
        Transform spawnTransform = GetRandomSpawnTransform();
        Transform obj = Instantiate(objectToSpawn);
        obj.gameObject.SetActive(true);
        obj.position = spawnTransform.position;
    }
}
