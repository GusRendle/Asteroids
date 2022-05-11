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

    // Start is called before the first frame update
    void Start()
    {
        if(!tutorialMode){
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
        }else{
            Spawn();
        }
    }
    //Asteroids needs to spawn at random positions with random trajectory
    public void Spawn()
    {
        for (int i = 0; i< this.spawnAmount; i++)
        {
            //Get random radial direction 
            //The spawner will be placed at player's position and the asteroids will be
            //generated around the radius of the spawner $insideUnitCircle
            //The asteroids should always be spawned on the dge of the circle, so we normalize it
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;
            //Create an angle of a random variance so that asteroids spawning on the edge of the spawner circli 
            //have a random cone trajectory
            float varience = Random.Range(-this.trajectoryVarience, this.trajectoryVarience);
            //Create an angle of a random variance in the z-axis
            Quaternion rotation = Quaternion.AngleAxis(varience, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPre, spawnPoint, rotation);
            //Give the asteroid random size
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            //Give the asteroid random sprite
            asteroid.sprite = Random.Range(0,4);
            //Rotate the sprite a random angle to make all asteroids look different
            asteroid.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
            //Set trajectory to always point at the center where the player is
            asteroid.trajectory = rotation * -spawnDirection;
            asteroid.SetTrajectory(asteroid.trajectory);
            
        }
    }

}