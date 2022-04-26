using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class Rooster : Enemy
{
    const float SPEED_COEFFICIENT = 1.1f;
    const int DAMAGE_COEFFICIENT = 2;
    const int HEALTH_INCREASE_COEFFICIENT = 2;

    public override float Speed
    {
        get { return speed * SPEED_COEFFICIENT; }
    }

    private new void Start()
    {
        base.Start();
        Health *= HEALTH_INCREASE_COEFFICIENT;
    }

    //POLYMORPHISM
    public override void Attack(PlayerController attackedAim)
    {
        attackedAim.Health -= damage * DAMAGE_COEFFICIENT;
    }

    private void Update()
    {
        MoveToPlayer();
        CheckHealth();
    }
}
