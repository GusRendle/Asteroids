using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSave{
    public PlayerData playerData;
    public Asteroids asteroidsData;

    public float[] position;

    public GameSave(Asteroids asteroids, PlayerData player){

        this.playerData = player;
        this.asteroidsData = asteroids;

    }
}