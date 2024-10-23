using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int maxHP;
    private int currentHP;
    void Start() {
        currentHP = maxHP;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            Debug.Log("worm hurt");
            currentHP -= 1;
            if (currentHP == 0) {
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
