using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bullet;
    public float speed = 500.0f;

    public float maxLifetime = 10.0f;

    private void Awake()
    {
        bullet = GetComponent<Rigidbody2D>();
    }

    //Once the bullet is projected it should get destroyed after a certain amount of time if it hasn't hit any asteroids
    public void Project(Vector2 direction)
    {
        bullet.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }
    //Destroy the bullet whenever it collides with any game object 
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(this.gameObject);
    }

}
