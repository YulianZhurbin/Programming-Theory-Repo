using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    readonly int damage = 50;

    public void Damage(Enemy enemy)
    {
        enemy.Health -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            Damage(enemy);
            Destroy(gameObject);
        }

        if(other.CompareTag("Egg"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
