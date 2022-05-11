
using UnityEngine;


public class MoveForwardCommand: Command{

    //Player
    private Player currentPlayer;
    //Rigid body of the player (physical object)
    private Rigidbody2D playerBody;

    //Pass the entity to the base class
    public MoveForwardCommand(Player entity) : base(entity){
        currentPlayer = entity;
    }
    //Add Force to the passed rigid body of the player
    //the force is calculated as direction (Vector3) * speed (specified in the player class)
    public override void Execute()
    {
        currentPlayer.GetComponent<Rigidbody2D>().AddForce(currentPlayer.transform.up * currentPlayer.thrustSpeed);
    }
}
