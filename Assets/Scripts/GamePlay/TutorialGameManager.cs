using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialGameManager : MonoBehaviour{

    public Player player;
    public Asteroid asteroidPre;
    public PowerUp powerUpPre;
    public AsteroidSpawner spawner;
    public ParticleSystem explosion;
    public float respawnTime = 2.0f;
    public GameObject gamePausedUI;

    public InputHandler inputHandler;

    public GameObject progressBar;
    public Image progressImage;
    public string currentKey = "";
    public Text tutorialText;
    string[] tutorialTextArray = new string[5];
    private bool resistance = true;

    private float waitTime = 20.0f;
    private bool fillComplete = false;
    private int stage = 0;
    public bool paused = false;

    public Vector3 initialPosition = new Vector3(-300f,-300f,0);
    public Vector3 powerUpPosition = new Vector3(404f,400.0f,0);

    private void Update() {
        if (!IsTargetVisible(Camera.main, player.gameObject))
        {
            player.transform.position = GetNewPosition(player.transform.position);
        }
    }

    bool IsTargetVisible(Camera c,GameObject go)
     {
         var planes = GeometryUtility.CalculateFrustumPlanes(c);
         var point = go.transform.position;
         foreach (var plane in planes)
         {
             if (plane.GetDistanceToPoint(point) < 0)
                 return false;
         }
         return true;
     }

    public Vector3 GetNewPosition(Vector3 position)
    {
        Vector3 screenCenter = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
        return new Vector3(screenCenter.x - position.x, screenCenter.y - position.y, 0);
    }

    private void Start() {
        tutorialText.text = "Welcome to Asteroids! Let's start with movement.";
        gamePausedUI.SetActive(false);
        spawner.gameObject.SetActive(false);
        progressBar.gameObject.SetActive(false);
        tutorialTextArray[0] = "Well Done! Now to do that in reverse...";
        tutorialTextArray[1] = "Ok, lets move onto turning";
        tutorialTextArray[2] = "Now the other direction...";
        tutorialTextArray[3] = "OK, time to make this an action game!";
        tutorialTextArray[4] = "Congrats! That's the controls done";
        Invoke("NewStage", 3);
    }

    private void NewStage(){
        stage++;
        fillComplete = false;
        resistance = true;
        
        switch(stage){
            case 1:
                player.gameObject.layer = LayerMask.NameToLayer("Invincible");

                tutorialText.gameObject.SetActive(true);
                progressBar.gameObject.SetActive(true);

                currentKey = CurrentProfile.Instance.thrustKey.ToString().ToLower();
                tutorialText.text = "press "+ CurrentProfile.Instance.thrustKey.ToString();
                break;
            case 2:
            tutorialText.gameObject.SetActive(true);
                progressBar.gameObject.SetActive(true);
                currentKey = CurrentProfile.Instance.backKey.ToString().ToLower();
                tutorialText.text = "press "+ CurrentProfile.Instance.backKey.ToString();
                break;
            case 3:
                progressBar.gameObject.SetActive(true);
                currentKey = CurrentProfile.Instance.leftKey.ToString().ToLower();
                tutorialText.text = "press "+ CurrentProfile.Instance.leftKey.ToString();
                break;
            case 4:
                progressBar.gameObject.SetActive(true);
                currentKey = CurrentProfile.Instance.rightKey.ToString().ToLower();
                tutorialText.text = "press "+ CurrentProfile.Instance.rightKey.ToString();
                break;
            case 5:
                progressBar.gameObject.SetActive(true);
                currentKey = CurrentProfile.Instance.shootKey.ToString().ToLower();
                tutorialText.text = "press "+ CurrentProfile.Instance.shootKey.ToString();
                break;
            case 6:
                tutorialText.text = "Now try and shoot an asteroid!";
                spawner.gameObject.SetActive(true);
                spawner.tutorialMode = true;
                break;
            case 7:
                tutorialText.text = "Now Get a Speed PowerUp!";
                this.player.transform.position = initialPosition;
                CreatePowerUp(0);
                spawner.gameObject.SetActive(true);
                spawner.Spawn();
                break;
            case 8:
                this.player.transform.position = initialPosition;
                tutorialText.text = "Now Get a Triple Bullet PowerUp!";
                CreatePowerUp(1);
                spawner.gameObject.SetActive(true);
                spawner.Spawn();
                break;
            case 9:
                spawner.gameObject.SetActive(false);
                tutorialText.text = "The tutorial is over now. Enjoy playing!";
                Invoke("TutorialOver", 10.0f);
                break; 
        }
    }


    private void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.Return)) Pause();

        if (currentKey != "")
        {
            if(Input.GetKey(currentKey)) AddProgress();
        }

        if(resistance){
            progressImage.fillAmount-= 1.0f / waitTime * Time.deltaTime;
        }
        
    }

    
    public void TutorialOver(){
        CurrentProfile.Instance.newPlayer = false;
        CurrentProfile.Instance.updateProfileFile();
        ProfileManager.SaveProfiles();
        SceneManager.LoadScene("MainMenu");
    }


    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();
        spawner.gameObject.SetActive(false);
        NewStage();
    }

    public void CreatePowerUp(int num){
        Vector3 position = transform.position;
        Quaternion rotation = new Quaternion();
        PowerUp powerUp = Instantiate(powerUpPre, position, rotation);
        switch (num)
        {
            case 0:
            powerUp.powerUpEffect = new SpeedPowerUp(50);
            powerUp.spriteNum = 0;
            break;
            case 1:
            powerUp.powerUpEffect = new TripleShotPowerUp();
            powerUp.spriteNum = 1;
            break;
        }
    }
    

        public void AddProgress(){
            progressImage.fillAmount+=0.01f;
            if(progressImage.fillAmount==1) fillComplete = true;
            if(fillComplete){
                fillComplete = false;
                progressImage.fillAmount = 0.0f;
                resistance = false;
                progressBar.gameObject.SetActive(false);
                tutorialText.gameObject.SetActive(true);
                player.transform.position = initialPosition;
                tutorialText.text = tutorialTextArray[stage - 1];
                currentKey = "";
                Invoke("NewStage",2.5f);
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