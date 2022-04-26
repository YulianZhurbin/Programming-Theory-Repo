using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    static ParticleSystemManager instance;
    ParticleSystem explosion;

    public static ParticleSystemManager Instance
    {
        get { return instance; }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        explosion = GetComponentInChildren<ParticleSystem>();
    }

    public void Explode(Vector3 position)
    {
        explosion.transform.position = position;
        explosion.Play();
    }
}
