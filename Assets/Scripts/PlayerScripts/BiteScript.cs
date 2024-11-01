using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteScript : MonoBehaviour
{
    public int damageAmount = 3;
    public string targetTag;
    public int regenAmount = 1;
    public PlayerHealth regen;

    public void Bite() {
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;

        Collider2D thisCollider = GetComponent<Collider2D>();
        thisCollider.OverlapCollider(contactFilter, colliders);

        foreach (Collider2D collider in colliders) {
            if (collider.CompareTag(targetTag)) {
                PlayerHealth hpScript = collider.GetComponent<PlayerHealth>();
                if (hpScript != null) {
                    hpScript.DecreaseHP(damageAmount);
                    regen.IncreaseHP(regenAmount);
                }
            }
        }
    }
}
