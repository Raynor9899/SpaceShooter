﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject EnemyGo; //This is our enemy prefab.
    public GameObject EnemyGo2;
    float maxSpawnRateInSeconds = 2f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Function to spawn an enemy randomly
    void SpawnEnemy() {
        //This is the bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //This is the top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //Instantiate an enemy
        GameObject anEnemy = (GameObject)Instantiate(EnemyGo);
        GameObject strongEnemy = (GameObject)Instantiate(EnemyGo2);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        strongEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        //Schedule when to spawn next enemy
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn() {
        float spawnInNSeconds;
        if (maxSpawnRateInSeconds > 1f) {
            //Pick a number between 1 and maxSpawnRateInSeconds
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        } else {
            spawnInNSeconds = 1f;
        }

        Invoke("SpawnEnemy", spawnInNSeconds);
    }

    void IncreaseSpawnRate() {
        if (maxSpawnRateInSeconds > 1f) {
            maxSpawnRateInSeconds--;
        }

        if (maxSpawnRateInSeconds == 1f) {
            CancelInvoke("IncreaseSpawnRate");
        }
    }
    public void ScheduleEnemySpawner() {
        //Reset max enemy spawn rate
        maxSpawnRateInSeconds = 2f;

        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        //Increase spawn rate every 20 seconds
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }
    //Function to stop enemy spawner
    public void UnscheduleEnemySpawner() {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }


}
