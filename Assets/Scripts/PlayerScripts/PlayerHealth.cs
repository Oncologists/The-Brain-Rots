using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    public Animator animator;
    public Dashing dashing;
    public DeathAnimation animate;
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

    private IEnumerator NextPhase() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Phase 2");
    }


    public void Kill() {
        if (gameObject.CompareTag("Player")) {
            SceneManager.LoadSceneAsync("Main Menu");
        }
        
        if (gameObject.CompareTag("Enemy")) {
            if (SceneManager.GetActiveScene().name == "Phase 1") {
                animator.SetBool("Blink", true);
                StartCoroutine(NextPhase());
            } else {
                foreach (Transform child in transform) {
                    Destroy(child.gameObject);
                }
                dashing.setSpeed(0, 0);
                Destroy(gameObject);
                FindObjectOfType<ManageAudio>().Play("deathnoise");
                animate.startAnimation();
            }
        }
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
        if (currentHP <= 0) {
            Kill();
        }
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