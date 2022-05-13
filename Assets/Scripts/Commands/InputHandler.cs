
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour{
    
    private bool isMovingForward;
    private bool isReversing;
    private bool isTurningLeft;
    private bool isTurningRight;
    private bool isShooting;

    private MoveBackCommand backCmd;
    private MoveForwardCommand thrustCmd;
    private TurnLeftCommand leftCmd;
    private TurnRightCommand rightCmd;
    private ShootCommand shootCmd;

    public KeyCode thrustInput;
    public KeyCode reverseInput;
    public KeyCode leftInput;
    public KeyCode rightInput;
    public KeyCode shootInput;

    private void Start() {
        Player player = gameObject.GetComponent<Player>();
        backCmd = new MoveBackCommand(player);
        thrustCmd = new MoveForwardCommand(player);
        leftCmd = new TurnLeftCommand(player);
        rightCmd = new TurnRightCommand(player);
        shootCmd = new ShootCommand(player);
        SetKeys();
    }

    void Update()
    {
        if (gameObject.GetComponent<Player>() != null) {
            isMovingForward = Input.GetKey(thrustInput); 
            isReversing = Input.GetKey(reverseInput); 
            isTurningLeft = Input.GetKey(leftInput); 
            isTurningRight = Input.GetKey(rightInput); 
            if (Input.GetKeyDown(shootInput)) shootCmd.Execute();
        }
    }
    private void FixedUpdate() {
            if (gameObject.GetComponent<Player>() != null) {
            if (isMovingForward) thrustCmd.Execute();
            if (isReversing) backCmd.Execute();
            if (isTurningLeft) leftCmd.Execute();
            if (isTurningRight) rightCmd.Execute();
        }
    }

    private void SetKeys() {
        thrustInput = CurrentProfile.Instance.thrustKey;
        reverseInput = CurrentProfile.Instance.backKey;
        leftInput = CurrentProfile.Instance.leftKey;
        rightInput = CurrentProfile.Instance.rightKey;
        shootInput = CurrentProfile.Instance.shootKey;
    }

}