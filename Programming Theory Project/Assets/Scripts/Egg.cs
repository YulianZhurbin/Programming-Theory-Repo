using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    readonly int damage = 5;

    public void Damage(PlayerController player)
    {
        player.Health -= damage;
    }
}
