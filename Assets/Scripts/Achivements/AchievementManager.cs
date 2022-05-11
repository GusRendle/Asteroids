using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public static List<Achievement> achievements;
    public AchObj achObjPre;
    public Sprite[] sprites;
    public Image[] images;

    public int score;
    public int lives;
    public float playerSpeed;
    public int hasCash = 0;
    public int hasHeart = 0;
    public int hasWipe = 0;
    public int hasInvincible = 0;
    public int hasSpeed = 0;
    public int hasTriple = 0;
    public int hasTurn = 0;
    public int hasAll = 0;


    public bool AchievementUnlocked(string achievementName)
    {
        bool result = false;

        if (achievements == null) return false;

        Achievement[] achievementsArray = achievements.ToArray();
        Achievement a = Array.Find(achievementsArray, ach => achievementName == ach.title);

        if (a == null)
            return false;

        result = a.achieved;

        return result;
    }

    private void Start()
    {
        InitializeAchievements();
    }

    private void InitializeAchievements()
    {
        if (achievements != null)
            return;

        achievements = new List<Achievement>();
        achievements.Add(new Achievement(sprites[0], "Full of Life", "Have 5 lives", (object o) => lives >= 5));
        achievements.Add(new Achievement(sprites[1], "Like a cat", "Have 9 lives", (object o) => lives >= 9));
        achievements.Add(new Achievement(sprites[2], "First Bl- Gravel", "Destroy an asteroid", (object o) => score > 0));
        achievements.Add(new Achievement(sprites[3], "Going Places", "Reach a score of 1000", (object o) => score > 1000));
        achievements.Add(new Achievement(sprites[4], "Pro Survivor", "Reach a score of 5000", (object o) => score > 5000));
        achievements.Add(new Achievement(sprites[5], "Best of the Best", "Reach a score of 10000", (object o) => score > 5000));
        achievements.Add(new Achievement(sprites[6], "Speed Demon", "Reach maximum speed", (object o) => playerSpeed > 1000f));
        achievements.Add(new Achievement(sprites[7], "Nitro Boost", "Find a speed power-up", (object o) => hasSpeed > 0));
        achievements.Add(new Achievement(sprites[8], "Overwhelming firepower", "Find a triple shot power-up", (object o) => hasTriple > 0));
        achievements.Add(new Achievement(sprites[9], "Mega manoeuvrability", "Find a turn boost power-up", (object o) => hasTurn > 0));
        achievements.Add(new Achievement(sprites[10], "Can't touch this", "Find an invincibility power-up", (object o) => hasInvincible > 0));
        achievements.Add(new Achievement(sprites[11], "Living god", "Have all effects active at once", (object o) => hasAll == 20));
    }

    private void Update()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        Player player = FindObjectOfType<Player>();
        lives = gameManager.lives;
        score = gameManager.score;
        if (player != null) {
            playerSpeed = player.rigidBody.velocity.magnitude;
            if (player.speedInc != null) {
                hasSpeed = 5;
            } else {
                hasSpeed = 0;
            }
            if (player.turnInc != null) {
                hasTurn = 5;
            } else {
                hasTurn = 0;
            }
            if (player.tripleShot != null) {
                hasTriple = 5;
            } else {
                hasTriple = 0;
            }
            if (player.invincible != null) {
                hasInvincible = 5;
            } else {
                hasInvincible = 0;
            }
            hasAll = (hasSpeed + hasTurn + hasTriple + hasInvincible);
        }

        if (score > 0) {
            CheckAchievementCompletion();
        }
    }

    private void CheckAchievementCompletion()
    {
        if (achievements == null)
            return;

        foreach (var achievement in achievements)
        {
            if (achievement.UpdateCompletion()) {
                Vector3 position = new Vector3(0,0,0);
                Quaternion rotation = new Quaternion();
                AchObj achObj = Instantiate(achObjPre, position, rotation);
                achObj.titleString = achievement.title;
                achObj.descString = achievement.description;
                achObj.image.overrideSprite = achievement.img;
                Destroy(achObj.gameObject, 10f);
            }
        }
    }
}