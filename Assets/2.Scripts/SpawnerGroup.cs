using System.Collections.Generic;
using UnityEngine;

public class SpawnerGroup : MonoBehaviour
{
    public float SpawnTime;

    public List<Spawner> Spawners;

    private void Start()
    {
        InvokeRepeating("RandomSpawn", 0f, SpawnTime);
    }

    private void RandomSpawn()
    {
        int rand = Random.Range(0, 3);
        Spawners[rand].Spawn();
    }
}