using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int maxHP;
    public string opp = "filler";
    public string oppoison = "filler";
    private int currentHP;
    private bool isPosioned;
    public float poisonDuration;
    private float poisonDurationReset; 
    public int posionDamage;

    void Start() {
        currentHP = maxHP;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        poisonDurationReset = poisonDuration;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(opp) || other.CompareTag(oppoison)) {
            DecreaseHP(1);

            if (currentHP <= 0) {
                Destroy(gameObject, 0.1f);
            }

            if (other.CompareTag(oppoison)) {
                poisonDuration = poisonDurationReset;
                isPosioned = true;
                StartCoroutine(CheckConditionAndRun());
            }
        }
    }

    public int GetCurrentHP() {
        return currentHP;
    } 

    public void DecreaseHP(int damage) {
        FindObjectOfType<ManageAudio>().Play("hitsound");
        Debug.Log("Player hurt");
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    public void IncreaseHP(int heal) {
        currentHP += heal;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }
    
    void Update() {
        Debug.Log(currentHP);
    }

    private IEnumerator CheckConditionAndRun() {
        while (isPosioned) {
            Debug.Log("poison");
            DecreaseHP(posionDamage);
            yield return new WaitForSeconds(1f);
            poisonDuration -= 1f;
            if (poisonDuration <= 0) {
                isPosioned = false;
            }
        }
    }
}
