using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed = 1.5f;
    [SerializeField] protected int damage = 15;
    AudioSource audioSource;
    private Rigidbody enemyRb;
    private GameObject player;
    int health = 50;

    //ENCAPSULATION
    public virtual float Speed
    {
        get { return speed; }
    }

    //ENCAPSULATION
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        MoveToPlayer();
        CheckHealth();
    }

    public virtual void Attack(PlayerController attackedAim)
    {
        attackedAim.Health -= damage;
    }

    //ASTRACTION
    public void MoveToPlayer()
    {
        Vector3 movingDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(movingDirection * Speed);
        transform.LookAt(player.transform);
    }

    //ASTRACTION
    public void CheckHealth()
    {
        if (Health <= 0)
        {
            ParticleSystemManager.Instance.Explode(transform.position);
            Destroy(gameObject);
        }
    }
}
