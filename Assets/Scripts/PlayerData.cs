
[System.Serializable]
public class PlayerData{
    public int lives;
    public int score;

    public float[] position;

    public PlayerData(GameManager manager, Player player){

        score = manager.score;
        lives = manager.lives;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }
}