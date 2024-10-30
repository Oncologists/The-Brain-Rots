using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoolDown : MonoBehaviour
{
    [SerializeField] private float coolDown;
    private float nextFire;
    
    public bool IsCoolingDown => Time.time < nextFire;
    public void StartCooldown() => nextFire = Time.time + coolDown;
}
