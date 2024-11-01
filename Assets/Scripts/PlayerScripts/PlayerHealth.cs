using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int maxHP;
    public string[] opp = {"filler"};
    public string oppoison = "filler";
    private int currentHP;
    private bool isPoisoned;
    public float poisonDuration;
    private float poisonDurationReset; 
    public int poisonDamage;
    private Boolean isPlayer;


    void Start() { 
        if (gameObject.CompareTag("Player")) {
            isPlayer = true;
        } else {
            isPlayer = false;
        }

        currentHP = maxHP;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        poisonDurationReset = poisonDuration;
    }

    void OnTriggerEnter2D(Collider2D other) {
        foreach (string tag in opp) {
            if (other.CompareTag(tag)) {
                DecreaseHP(1);
            }
        } 
        if (other.CompareTag(oppoison)) {
            if (other.CompareTag(oppoison)) {
                poisonDuration = poisonDurationReset;
                isPoisoned = true;
                StartCoroutine(CheckConditionAndRun());
            }
        }
    }

    public void Kill() {
        Destroy(gameObject);
    }

    public int GetMaxHP() {
        return maxHP;
    }

    public int GetCurrentHP() {
        return currentHP;
    } 

    public void DecreaseHP(int damage) {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        if (isPlayer) {
            FindObjectOfType<ManageAudio>().Play("hitsound");
        } else {
            FindObjectOfType<ManageAudio>().Play("bosshurtsound");
        }
    }

    public void IncreaseHP(int heal) {
        currentHP += heal;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }
    
    void Update() {
        // Debug.Log(currentHP);
    }

    private IEnumerator CheckConditionAndRun() {
        while (isPoisoned) {
            // Debug.Log("poison");
            DecreaseHP(poisonDamage);
            yield return new WaitForSeconds(1f);
            poisonDuration -= 1f;
            if (poisonDuration <= 0) {
                isPoisoned = false;
            }
        }
    }
}
