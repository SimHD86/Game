using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Lightning")]
public class Lightning : Powerup
{

    public override void Apply(GameObject target)
    {

        target.GetComponent<PlayerMovement>().extraJumps = 1;
        target.GetComponent<PlayerMovement>().coyoteTime = 0.25f;
        target.GetComponent<PlayerMovement>().speed = 17.5f;
        target.GetComponent<PlayerMovement>().jumpPower = 10;
        target.GetComponent<PlayerMovement>().hook = true;
        target.GetComponent<PlayerAttack>().enabled = false;
        target.GetComponent<PlayerMovement>().lightning = true;



    }
}