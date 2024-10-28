using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int maxHP;
    private int currentHP;
    void Start() {
        currentHP = maxHP;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            FindObjectOfType<ManageAudio>().Play("hitsound"); // play sound 
            Debug.Log("worm hurt");
            currentHP -= 1;
            if (currentHP == 0) {
                Destroy(gameObject, 0.1f);
            }
        }
    }
    
    void Update() {
        Debug.Log(currentHP);
    }
}
