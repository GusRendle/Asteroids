
using UnityEngine;


public class TurnLeftCommand: Command{
    private Player currentPlayer;
    float turnDirection = 1.0f;
    public TurnLeftCommand(Player entity) : base(entity){
        currentPlayer = entity;
    }
    public override void Execute()
    {
        currentPlayer.GetComponent<Rigidbody2D>().AddTorque(turnDirection * currentPlayer.turnSpeed);
    }

}
