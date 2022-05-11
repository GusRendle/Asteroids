using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Asteroids{
        public AsteroidData [] asteroids;
        public Asteroids(AsteroidData[] asteroids){
            this.asteroids = asteroids;
        }
}  