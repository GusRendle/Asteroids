using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {
    public Player player;
    public Asteroid asteroidPre;

    public AsteroidSpawner spawner;
    public ParticleSystem explosion;
    public float respawnTime = 2.0f;
    public float respawnInvisibility = 3.0f;

    public bool paused = false;


    public GameObject gameOverUI;
    public GameObject gamePausedUI;
    public int score { get; private set; }
    public Text scoreText;

    public int lives { get; private set; }
    public Text livesText;
    public Vector3 initialPosition = new Vector3(0f,0f,0);


    private void Start() {
        NewGame();
    }

    private void NewGame()
    {
        if(CurrentProfile.Instance.newPlayer){
            SceneManager.LoadScene("Tutorial");
        }
        ClearAsteroids();
        ClearPowerUps();

        gameOverUI.SetActive(false);
        gamePausedUI.SetActive(false);

        SetScore(0);
        SetLives(2);
        Respawn();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) Pause();

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
    public void PlayerDied(){
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        SetLives(lives - 1);
        if (this.lives <= 0){
            GameOver();
        }else{
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();
        if(asteroid.size < asteroid.minSize+20.0f){
            SetScore(score+100);
        }else if(asteroid.size < 125.0f){
             SetScore(score + 50);
        }else{
            SetScore(score + 25);
        }
    }
    
    public void Respawn()
    {
        this.player.transform.position = initialPosition;
        this.player.gameObject.SetActive(true);
        this.player.gameObject.layer = LayerMask.NameToLayer("Invincible");
        Invoke(nameof(TurnOnCollisions), this.respawnInvisibility);
    }
    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        if(CurrentProfile.Instance.score < score) CurrentProfile.Instance.score = score;
        if(CurrentProfile.Instance.newPlayer) CurrentProfile.Instance.newPlayer = false;
        CurrentProfile.Instance.updateProfileFile();
        ProfileManager.SaveProfiles();
        gameOverUI.SetActive(true);
    }

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