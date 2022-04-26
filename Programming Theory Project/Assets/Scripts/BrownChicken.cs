using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class BrownChicken : Enemy
{
    const float SPEED_COEFFICIENT = 1.0f;
    const int DAMAGE_COEFFICIENT = 1;

    public override float Speed
    {
        get { return speed * SPEED_COEFFICIENT; }
    }

    private void Update()
    {
        MoveToPlayer(); 
        CheckHealth();
    }

    //POLYMORPHISM
    public override void Attack(PlayerController attackedAim)
    {
        attackedAim.Health -= damage * DAMAGE_COEFFICIENT;
    }
}
