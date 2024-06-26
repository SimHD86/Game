using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Fire")]
public class Fire : Powerup
{
    
    public override void Apply(GameObject target)
    {
        
        target.GetComponent<PlayerMovement>().extraJumps = 1;
        target.GetComponent<PlayerMovement>().coyoteTime = 0.25f;
        target.GetComponent<PlayerMovement>().speed = 15;
        target.GetComponent<PlayerMovement>().jumpPower = 15;
        target.GetComponent<PlayerMovement>().hook = false;
        target.GetComponent<PlayerAttack>().enabled = true;
        target.GetComponent<PlayerMovement>().fire = true;

    }
}
