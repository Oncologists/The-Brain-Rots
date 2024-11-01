using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private CoolDown coolDown;
    public Animator animator;
    public GameObject projectile;
    public Transform squirter;
    public float force;

    private IEnumerator SquirtAnimation() {
        animator.SetBool("Squirt", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Squirt", false);
    }

    public void Fire() {
        StartCoroutine(SquirtAnimation());
        if (coolDown.IsCoolingDown) {
            return;
        }
        GameObject bullet = Instantiate(projectile, squirter.position, squirter.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(squirter.up * force, ForceMode2D.Impulse);
        coolDown.StartCooldown();
    }
}