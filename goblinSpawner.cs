using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject Goblin;

    public float DownTime = 0.5f; //Time between spawns

    private float SpawnTimer = 0.0f; //Time to start spawning when entering room

    //private bool SlimeSpawned = false;

    void Update()
    {
        if (Time.time >= SpawnTimer)
        {

            SpawnGoblin();
            SpawnTimer = Time.time + DownTime;
            //SlimeSpawned = true;


        }
    }

    public void SpawnGoblin()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(Goblin, spawnPoints[i].position, Quaternion.identity);
        }
    }
}
