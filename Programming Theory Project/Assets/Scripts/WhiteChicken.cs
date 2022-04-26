using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteChicken : Enemy
{
    [SerializeField] GameObject eggPrefab;
    Vector3 eggLaunchOffset;
    float eggSpeed = 15;
    float eggRotationSpeed = 30;
    float delayTime = 3;
    float shootInterval = 5;
    const float SPEED_COEFFICIENT = 1.05f;
    const int DAMAGE_COEFFICIENT = 2;

    public override float Speed
    {
        get { return speed * SPEED_COEFFICIENT; }
    }

    new void Start()
    {
        base.Start();
        eggLaunchOffset = new Vector3(0, 1, 0);
        InvokeRepeating("Shoot", delayTime, shootInterval);
    }

    public override void Attack(PlayerController attackedAim)
    {
        attackedAim.Health -= damage * DAMAGE_COEFFICIENT;
    }

    void Update()
    {
        MoveToPlayer();
        CheckHealth();
    }

    void Shoot()
    {
        Vector3 eggLaunchPos = transform.position + eggLaunchOffset;
        GameObject egg = Instantiate(eggPrefab, eggLaunchPos, transform.rotation);
        egg.GetComponent<Rigidbody>().AddForce(eggSpeed * transform.forward, ForceMode.Impulse);
        egg.transform.Rotate(transform.up, eggRotationSpeed * Time.deltaTime);
    }
}
