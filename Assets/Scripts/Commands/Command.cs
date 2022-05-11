
using UnityEngine;

public abstract class Command: MonoBehaviour{

    //Enitity the command class is going to act upon
    public Player entity;

    //When the command is created it gets linked to the specified entity
    public Command(Player e){
        entity = e;
    }
    //Method to override in each inherent class
    public abstract void Execute();
}