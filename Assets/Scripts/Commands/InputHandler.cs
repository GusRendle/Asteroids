
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour{
    
    private bool movingForward;
    private bool movingBack;
    private bool turningLeft;
    private bool turningRight;
    private bool shooting;

    private MoveBackCommand back;
    private MoveForwardCommand forward;
    private TurnLeftCommand turnLeft;
    private TurnRightCommand turnRight;

    private ShootCommand shoot;

    private int progress=0;



    public string inputForward = "w";
    public string inputBack = "s";
    public string inputLeft = "a";
    public string inputRight = "d";
    public string inputShoot = "space";

    private void Start() {

        back = new MoveBackCommand(this.gameObject.GetComponent<Player>());
        forward = new MoveForwardCommand(this.gameObject.GetComponent<Player>());
        turnLeft = new TurnLeftCommand(this.gameObject.GetComponent<Player>());
        turnRight = new TurnRightCommand(this.gameObject.GetComponent<Player>());
        shoot = new ShootCommand(this.gameObject.GetComponent<Player>());
    }
    public void AssignCommand(string inputForward, string inputBack, string inputLeft, string inputRight, string inputShoot){
        this.inputForward = inputForward;
        ProfileSingleton.instance.up = inputForward;
        this.inputBack = inputBack;
        ProfileSingleton.instance.back = inputBack;
        this.inputLeft = inputLeft;
        ProfileSingleton.instance.left = inputLeft;
        this.inputRight = inputRight;
        ProfileSingleton.instance.right = inputRight;
        this.inputShoot = inputShoot;
        ProfileSingleton.instance.shoot = inputShoot;
    }


    void Update()
    {
        movingForward = (Input.GetKey(inputForward)); 
        movingBack = (Input.GetKey(inputBack)); 
        turningLeft = (Input.GetKey(inputLeft)); 
        turningRight = (Input.GetKey(inputRight)); 
        if (Input.GetKeyDown(inputShoot)) shoot.Execute();
        
    }
    public int GetProgress(int progress, string direction){
        if(direction.Equals("up")){
            if(movingForward) progress++;
        }
        return progress;
    }
    private void FixedUpdate() {
        Profile profile = ProfileManager.FindProfile(ProfileSingleton.instance.profileId);

        if (movingForward) forward.Execute();
        if (movingBack) back.Execute();
        if (turningLeft) turnLeft.Execute();
        if (turningRight) turnRight.Execute();
    }
}