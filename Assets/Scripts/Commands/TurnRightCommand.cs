
using UnityEngine;


public class TurnRightCommand: Command{
    private Player currentPlayer;
    float turnDirection = -1.0f;
    public TurnRightCommand(Player entity) : base(entity){
        currentPlayer = entity;
    }
    public override void Execute()
    {
        currentPlayer.GetComponent<Rigidbody2D>().AddTorque(turnDirection * currentPlayer.turnSpeed);
    }

}
