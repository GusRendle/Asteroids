
using UnityEngine;


public class MoveBackCommand: Command{
    private Player currentPlayer;
    public MoveBackCommand(Player entity) : base(entity){
        currentPlayer = entity;
    }
    public override void Execute()
    {
        currentPlayer.GetComponent<Rigidbody2D>().AddForce(-currentPlayer.transform.up * currentPlayer.thrustSpeed);
    }
}
