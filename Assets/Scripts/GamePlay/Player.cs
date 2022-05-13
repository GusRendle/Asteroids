using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public Bullet bulletPrefab;

    private InputHandler handler;

    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    public Coroutine speedInc;
    public Coroutine tripleShot;
    public Coroutine turnInc;
    public Coroutine invincible;

    private  void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        handler = GetComponent<InputHandler>();
    }
    private  void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid"){
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity= 0.0f;
            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
