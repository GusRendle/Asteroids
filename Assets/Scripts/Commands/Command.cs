
using UnityEngine;

public abstract class Command: MonoBehaviour{
    public Player entity;
    public Command(Player e){
        entity = e;
    }
    public abstract void Execute();
}