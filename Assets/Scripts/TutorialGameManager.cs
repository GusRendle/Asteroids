using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialGameManager : MonoBehaviour{

    public Player player;
    public Asteroid asteroidPre;
    public PowerUp powerUpPre;
    public AsteroidSpawner spawner;
    //Reference to the explosion effect
    public ParticleSystem explosion;
    //Time after which the player can respawn
    public float respawnTime = 2.0f;
    //Time for which a player cannot collide with any objects in the scene
    public GameObject gamePausedUI;

    public InputHandler inputHandler;

    public GameObject upControl;
    public GameObject backControl;
    public GameObject leftControl;
    public GameObject rightControl;
    public GameObject shootControl;
    public Image upImage;
    public Image backImage;
    public Image leftImage;
    public Image rightImage;
    public Image shootImage;
    public Text mainText;
    private bool resistance = true;

    private float waitTime = 20.0f;
    private bool fillComplete = false;
    private int direction=0;

    public Vector3 initialPosition = new Vector3(404f,123f,0);
    public Vector3 powerUpPosition = new Vector3(404f,400.0f,0);




    public void NewGame(){
        this.player.gameObject.layer = LayerMask.NameToLayer("Invincible");

        mainText.gameObject.SetActive(false);
        upControl.SetActive(true);
        backControl.SetActive(false);
        leftControl.SetActive(false);
        rightControl.SetActive(false);
        

        upControl.transform.Find("Text").GetComponent<Text>().text = CurrentProfile.Instance.thrustKey.ToString();
        backControl.transform.Find("Text").GetComponent<Text>().text = CurrentProfile.Instance.backKey.ToString();
        leftControl.transform.Find("Text").GetComponent<Text>().text = CurrentProfile.Instance.leftKey.ToString();
        rightControl.transform.Find("Text").GetComponent<Text>().text = CurrentProfile.Instance.rightKey.ToString();
        shootControl.transform.Find("Text").GetComponent<Text>().text = CurrentProfile.Instance.shootKey.ToString();


    }
    public bool paused = false;


    private void Start() {
        mainText.text = "Welcome to the tutorial! Let's try some navigation!";
        gamePausedUI.SetActive(false);
        spawner.gameObject.SetActive(false);
        Invoke("NewGame",2);
    }

    private void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.Return)) Pause();
        if(upControl.gameObject.active){
            if(Input.GetKey(CurrentProfile.Instance.thrustKey.ToString())) AddProgress(upImage, upControl);
        }else if(backControl.gameObject.active){
            if(Input.GetKey(CurrentProfile.Instance.backKey.ToString())) AddProgress(backImage, backControl);
        }else if(leftControl.gameObject.active){
            if(Input.GetKey(CurrentProfile.Instance.leftKey.ToString())) AddProgress(leftImage, leftControl);
        }else if(rightControl.gameObject.active){
            if(Input.GetKey(CurrentProfile.Instance.rightKey.ToString())) AddProgress(rightImage, rightControl);
        }else if(shootControl.gameObject.active){
            if(Input.GetKey(CurrentProfile.Instance.shootKey.ToString())) AddProgress(shootImage, shootControl);
        }

        if(resistance){
            upImage.fillAmount-= 1.0f / waitTime * Time.deltaTime;
            backImage.fillAmount-= 1.0f / waitTime * Time.deltaTime;
            leftImage.fillAmount-= 1.0f / waitTime * Time.deltaTime;
            rightImage.fillAmount-= 1.0f / waitTime * Time.deltaTime;
        }
        
    }

    private void NewMethod(){

        direction++;
        fillComplete = false;
        resistance = true;
        switch(direction){
            case 1:
                mainText.text = "press "+CurrentProfile.Instance.backKey.ToString();
                backControl.SetActive(true);
                break;
            case 2:
                mainText.text = "press "+CurrentProfile.Instance.leftKey.ToString();
                leftControl.SetActive(true);
                break;
            case 3:
                mainText.text = "press "+CurrentProfile.Instance.rightKey.ToString();
                rightControl.SetActive(true);
                break;
            case 4:
                mainText.text = "press "+CurrentProfile.Instance.shootKey.ToString();
                shootControl.SetActive(true);
                break;
            case 5:
                mainText.text = "Now try and shoot an asteroid!";
                spawner.gameObject.SetActive(true);
                spawner.tutorialMode = true;
                break;
            case 6:
                mainText.text = "Now Get a Speed PowerUp!";
                this.player.transform.position = initialPosition;
                CreatePowerUp(0);
                spawner.gameObject.SetActive(true);
                spawner.Spawn();
                break;
            case 7:
                this.player.transform.position = initialPosition;
                mainText.text = "Now Get a Triple Bullet PowerUp!";
                CreatePowerUp(1);
                spawner.gameObject.SetActive(true);
                spawner.Spawn();
                break;
            case 8:
                spawner.gameObject.SetActive(false);
                mainText.text = "The tutorial is over now. Enjoy playing!";
                Invoke("TutorialOver", 10.0f);
                break;

            
        }
        Debug.LogError("New Method!");
    }

    public void TutorialOver(){
        CurrentProfile.Instance.newPlayer = false;
        CurrentProfile.Instance.updateProfileFile();
        ProfileManager.SaveProfiles();
        SceneManager.LoadScene("MainMenu");
    }


    public void AsteroidDestroyed(Asteroid asteroid)
    {
        //Play the explosion effect at the position of the asteroid's death
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();
        spawner.gameObject.SetActive(false);
        NewMethod();
    }

    public void CreatePowerUp(int num){
        Quaternion rotation = new Quaternion();
        PowerUp powerUp = Instantiate(powerUpPre, powerUpPosition, rotation);
        int rnd = Random.Range(0,2);
        switch (num)
        {
            case 0:
            powerUp.powerUpEffect = new SpeedPowerUp(400);
            powerUp.spriteNum = 0;
            break;
            case 1:
            powerUp.powerUpEffect = new TripleShotPowerUp();
            powerUp.spriteNum = 1;
            break;
        }
    }
    

        public void AddProgress(Image imageFill, GameObject control){
            imageFill.fillAmount+=0.01f;
            if(imageFill.fillAmount==1) fillComplete = true;
            if(fillComplete){
                imageFill.fillAmount = 0.0f;
                resistance = false;
                control.gameObject.SetActive(false);
                mainText.gameObject.SetActive(true);
                this.player.transform.position = initialPosition;
                mainText.text = "Congrats! Let's try a few more things..";
                Invoke("NewMethod",2);
            }
        }


        public void Pause(){
        paused = !paused;
        if (paused){
            Time.timeScale = 0;
            gamePausedUI.SetActive(true);
        } else{
            Time.timeScale = 1;
            gamePausedUI.SetActive(false);
        }
    }

}