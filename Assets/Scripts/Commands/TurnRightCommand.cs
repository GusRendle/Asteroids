
using UnityEngine;


public class TurnRightCommand: Command{

    //Player
    private Player currentPlayer;
    //Set direction to right
    float turnDirection = -1.0f;

    //Pass the entity to the base class
    public TurnRightCommand(Player entity) : base(entity){
        currentPlayer = entity;
    }
    //Add torque to the passed rigid body of the player
    //the torque is calculated as direction (Vector3) * speed (specified in the player class)
    public override void Execute()
    {
        currentPlayer.GetComponent<Rigidbody2D>().AddTorque(turnDirection * currentPlayer.turnSpeed);
    }

}
