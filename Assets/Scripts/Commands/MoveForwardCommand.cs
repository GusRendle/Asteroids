
using UnityEngine;


public class MoveForwardCommand: Command{
    private Player currentPlayer;
    public MoveForwardCommand(Player entity) : base(entity){
        currentPlayer = entity;
    }
    public override void Execute()
    {
        currentPlayer.GetComponent<Rigidbody2D>().AddForce(currentPlayer.transform.up * currentPlayer.thrustSpeed);
    }
}
