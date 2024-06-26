using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(menuName ="Powerups/Wind")] 
public class Wind : Powerup
{
    
    public override void Apply(GameObject target)
    {
        
        target.GetComponent<PlayerMovement>().extraJumps = 2;
        target.GetComponent<PlayerMovement>().coyoteTime = 0.3f;
        target.GetComponent<PlayerMovement>().speed = 20;
        target.GetComponent<PlayerMovement>().jumpPower = 20;
        target.GetComponent<PlayerMovement>().hook = false;
        target.GetComponent<PlayerAttack>().enabled = false;
        target.GetComponent<PlayerMovement>().wind = true;



    }
}
