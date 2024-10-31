using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private CoolDown coolDown;
    public GameObject projectile;
    public Transform squirter;
    public float force;
    public void Fire() {
        if (coolDown.IsCoolingDown) {
            return;
        }
        GameObject bullet = Instantiate(projectile, squirter.position, squirter.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(squirter.up * force, ForceMode2D.Impulse);
        coolDown.StartCooldown();
    }
}