
using UnityEngine;


public class MoveBackCommand: Command{

    //Player
    private Player currentPlayer;

    //Pass the entity to the base class
    public MoveBackCommand(Player entity) : base(entity){
        currentPlayer = entity;
    }
    //Add Force to the passed rigid body of the player
    //the force is calculated as direction (Vector3) * speed (specified in the player class)
    public override void Execute()
    {
        currentPlayer.GetComponent<Rigidbody2D>().AddForce(-(currentPlayer.transform.up) * currentPlayer.thrustSpeed);
    }
}
