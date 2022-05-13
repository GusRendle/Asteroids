using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPre;

    public float spawnDistance= 500.0f;

    public float spawnRate = 4.0f;
    public float trajectoryVarience = 30.0f;
    public int spawnAmount = 1;
    public bool tutorialMode = false;
    Vector2 trajectory;
    void Start()
    {
        if(!tutorialMode){
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
        }else{
            Spawn();
        }
    }
    public void Spawn()
    {
        for (int i = 0; i< this.spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;
            float varience = Random.Range(-this.trajectoryVarience, this.trajectoryVarience);
            Quaternion rotation = Quaternion.AngleAxis(varience, Vector3.forward);
            Asteroid asteroid = Instantiate(this.asteroidPre, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.sprite = Random.Range(0,4);
            asteroid.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
            asteroid.trajectory = rotation * -spawnDirection;
            asteroid.SetTrajectory(asteroid.trajectory);
            
        }
    }

}