using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletHellController : MonoBehaviour
{
    enum Patterns {Spin, HeartBeat, Explode, ExplosiveBlood, Stop};
    [SerializeField] float frequency;
    [SerializeField] float swapFrequency;
    [SerializeField] Patterns patterns;
    [SerializeField] int explodeAmount;
    private Vector2 direction;

    public GameObject Bullet;
    public GameObject Player;
    private GameObject SpawnedBullet;
    private float timer = 0f;
    private float swapTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        timer += Time.deltaTime;
        swapTimer += Time.deltaTime;
        if(swapTimer > swapFrequency) {
            int rand = Random.Range(0, 4);
            if (rand == 0) {
                patterns = Patterns.Spin;
            } else if (rand == 1) {
                patterns = Patterns.Explode;
            } else if (rand == 2) {
                patterns = Patterns.HeartBeat;
            } else if (rand == 3) {
                patterns = Patterns.ExplosiveBlood;
            }
            swapTimer = 0f;
            swapFrequency = Random.Range(3, 5);
        }
        if (patterns == Patterns.Spin) {
            frequency = 0.08f;
            transform.eulerAngles = new Vector3(0f,0f,transform.eulerAngles.z+1f);
        }
        if (patterns == Patterns.Explode) {
            frequency = 2f;
        }
        if (patterns == Patterns.HeartBeat) {
            frequency = 1f;
        }
        if (patterns == Patterns.ExplosiveBlood) {
            frequency = 1.5f;
        }
        if (timer > frequency ) {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot() {
        if (patterns == Patterns.Spin) {
            SpawnedBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            float angleRad = transform.eulerAngles.z * Mathf.Deg2Rad;
            direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            SpawnedBullet.GetComponent<BulletScript>().SetDirection(direction);
        }
        if (patterns == Patterns.Explode) {
            for (int i = 0; i < explodeAmount; i++) {
                SpawnedBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
                Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                SpawnedBullet.GetComponent<BulletScript>().SetDirection(direction);
            }
        }
        if (patterns == Patterns.HeartBeat) {
            for (int i = 0; i < 10; i++) {
                //First Bullet
                SpawnedBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
                direction = new Vector2(Player.transform.position.x, Player.transform.position.y);
                SpawnedBullet.transform.position = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
                SpawnedBullet.GetComponent<BulletScript>().SetSpeed(Random.Range(2f, 4f), false, true, true);
                SpawnedBullet.GetComponent<BulletScript>().SetDuration(Random.Range(5f, 7f));
                SpawnedBullet.GetComponent<BulletScript>().SetDirection(direction);
                //Second Bullet
                SpawnedBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
                direction = -direction;
                SpawnedBullet.transform.position = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
                SpawnedBullet.GetComponent<BulletScript>().SetSpeed(Random.Range(2f, 4f), false, true, true);
                SpawnedBullet.GetComponent<BulletScript>().SetDuration(Random.Range(5f, 7f));
                SpawnedBullet.GetComponent<BulletScript>().SetDirection(direction);
            }
        }
        if (patterns == Patterns.ExplosiveBlood) {
            //First Bullet
                SpawnedBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
                direction = new Vector2(Player.transform.position.x, Player.transform.position.y);
                SpawnedBullet.GetComponent<BulletScript>().SetSpeed(Random.Range(2f, 4f), false, true, true);
                SpawnedBullet.GetComponent<BulletScript>().SetDuration(5f);
                SpawnedBullet.GetComponent<BulletScript>().SetExplode(true);
                SpawnedBullet.GetComponent<BulletScript>().SetDirection(direction);
        }
    }
}
