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
    public void Project(Vector2 direction)
    {
        bullet.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(this.gameObject);
    }

}
