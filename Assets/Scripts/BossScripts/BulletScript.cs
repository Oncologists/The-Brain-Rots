using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Sprite;
    bool speedup = false;
    bool speeddown = false;
    bool cap = false;
    bool explode = false;
    Vector2 direction = new Vector2(0, 0);
    [SerializeField] float speed = 1f;
    [SerializeField] float duration = 1f;
    int explodeAmount = 4;
    private float timer = 0f;
    private GameObject SpawnedBullet;
    // Update is called once per frame
    void Update()
    
    {
        // bullets face the direction shot
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Sprite.transform.rotation = rotation;
        
        if (speedup) {
            speed += Time.deltaTime; 
        }
        if (speeddown) {
            if (cap && speed > 0) {
                speed -= Time.deltaTime;
            } else if (!cap) {
                speed -= Time.deltaTime; 
            }
        }
        if(timer > duration) {
            if (explode) {
                explodeBullet();
            }
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
        transform.Translate(direction * speed * Time.deltaTime, Space.Self);
    }

    public void SetDirection(Vector2 newDirection) {
        newDirection.Normalize();
        direction = newDirection;
    }

    public void SetDuration(float newDuration) {
        duration = newDuration;
    }

    public void SetExplode(bool explosive) {
        explode = explosive;
        transform.localScale *= 2;
    }

    public void SetSpeed(float newSpeed, bool up, bool down, bool max) {
        speed = newSpeed;
        speedup = up;
        speeddown = down;
        cap = max;
    }

    void explodeBullet() {
        for (int i = 0; i < explodeAmount; i++) {
            SpawnedBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            Vector2 direction = Vector2.up;
            if (i == 0) {
                direction = Vector2.up;
            }
            if (i == 1) {
                direction = Vector2.down;
            }
            if (i == 2) {
                direction = Vector2.left;
            }
            if (i == 3) {
                direction = Vector2.right;
            }
            SpawnedBullet.transform.localScale /= 2;
            SpawnedBullet.GetComponent<BulletScript>().SetSpeed(3, true, false, false);
            SpawnedBullet.GetComponent<BulletScript>().SetDirection(direction);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Enemy")) {
            Destroy(this.gameObject);
        }
    }
}