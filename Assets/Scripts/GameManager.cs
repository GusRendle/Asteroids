using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour {
    //Instance of the player
    public Player player;
    public Asteroid asteroidPre;

    public AsteroidSpawner spawner;
    //Reference to the explosion effect
    public ParticleSystem explosion;
    //Time after which the player can respawn
    public float respawnTime = 2.0f;
    //Time for which a player cannot collide with any objects in the scene
    public float respawnInvisibility = 3.0f;

    public bool paused = false;


    public GameObject gameOverUI;
    public GameObject gamePausedUI;

    //Lives of the player
    public int score { get; private set; }
    public Text scoreText;

    public int lives { get; private set; }
    public Text livesText;

    //position at the center of the screen
    public Vector3 initialPosition = new Vector3(404f,123f,0);


    private void Start() {
        NewGame();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) Pause();
    }

    //Account for the player death
    public void PlayerDied(){
        //Play the explosion effect at the position of the player's death
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        //Decrement the amount of lives
        SetLives(lives - 1);
        if (this.lives <= 0){
            GameOver();
        }else{
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        //Play the explosion effect at the position of the asteroid's death
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        //small - 100 points
        if(asteroid.size < asteroid.minSize+20.0f){
            SetScore(score+100);
        //medium size - 50 points
        }else if(asteroid.size < 125.0f){
             SetScore(score + 50);
        //large size - 25 points
        }else{
            SetScore(score + 25);
        }
    }
    

    //Respawn the player when they have lives left
    public void Respawn()
    {
        //Reset the player position to be the center of the board
        this.player.transform.position = initialPosition;
        //Re-activate the player game object
        this.player.gameObject.SetActive(true);
        //Change the layer of the player object to Respawn which will ignore all collisions
        this.player.gameObject.layer = LayerMask.NameToLayer("Respawn");
        //After 3 seconds turn on collisions
        Invoke(nameof(TurnOnCollisions), this.respawnInvisibility);
    }

    //Reset the player object layer back to having normal collisions (asteroids)
    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    //Enable the Game Over UI
    private void GameOver()
    {
        Profile profile = ProfileManager.FindProfile(ProfileSingleton.instance.profileId);

        if(profile.score < score) profile.score = score;
        if(profile.newPlayer) profile.newPlayer = false;
        ProfileSingleton.instance.newPlayer = false;
        ProfileManager.SaveProfile(profile);
        gameOverUI.SetActive(true);
    }


    //Load the Main Menu Scene
    public void BackToMainMenu(){
        SceneManager.LoadScene(1);
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
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

    public void ClearAsteroids(){
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        for (int i = 0; i < asteroids.Length; i++) {
            Destroy(asteroids[i].gameObject);
        }
    }

    
    public void ClearPowerUps(){
        PowerUp[] powerUps = FindObjectsOfType<PowerUp>();

        for (int i = 0; i < powerUps.Length; i++) {
            Destroy(powerUps[i].gameObject);
        }
    }

    public void NewGame()
    {
        Profile profile = ProfileManager.FindProfile(ProfileSingleton.instance.profileId);

        if(profile.newPlayer){
            SceneManager.LoadScene("Tutorial");
        }
        ClearAsteroids();
        ClearPowerUps();

        gameOverUI.SetActive(false);
        gamePausedUI.SetActive(false);

        SetScore(0);
        SetLives(1);
        Respawn();
    }


    public void SaveGame(){
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
        SaveSystem.SaveGame(asteroids, this, player, ProfileSingleton.instance.profileId);
    }
    

    public void LoadGame(){
        //Unpause the game
        Pause();
        //Load Player
        GameSave saveFile = SaveSystem.LoadGame(ProfileSingleton.instance.profileId);
        PlayerData data = saveFile.playerData;

        SetLives(data.lives);
        SetScore(data.score);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        this.player.transform.position = position;

        //Clear asteroids if any
        ClearAsteroids();
        //Clear any powerups
        ClearPowerUps();
        //Load existing asteroid data
        Asteroids asteroidsData = saveFile.asteroidsData;
        AsteroidData [] asteroids = asteroidsData.asteroids;
        for (int i = 0; i < asteroids.Length; i++) {
            Vector3 positionAsteroid;
            positionAsteroid.x = asteroids[i].position[0];
            positionAsteroid.y = asteroids[i].position[1];
            positionAsteroid.z = asteroids[i].position[2];
            
            Quaternion rotation;
            rotation.x = asteroids[i].rotation[0];
            rotation.y = asteroids[i].rotation[1];
            rotation.z = asteroids[i].rotation[2];
            rotation.w = asteroids[i].rotation[3];

            Vector2 trajectory;
            trajectory.x = asteroids[i].trajectory[0];
            trajectory.y = asteroids[i].trajectory[1];

            Asteroid asteroid = Instantiate(spawner.asteroidPre, positionAsteroid, rotation);
            asteroid.size = asteroids[i].size;
            asteroid.trajectory = trajectory;
            asteroid.SetTrajectory(trajectory);
        }
        //Remove the paused game UI
        gamePausedUI.SetActive(false);
    }
    
    public void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    public void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }
}