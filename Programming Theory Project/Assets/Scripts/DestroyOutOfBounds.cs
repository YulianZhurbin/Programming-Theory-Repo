using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float xLimit = 70;
    private float zLimit = 70;

    // Update is called once per frame
    void Update()
    {
        bool isOutOfBounds = transform.position.z > zLimit || transform.position.z < -zLimit ||
                             transform.position.x > xLimit || transform.position.x < -xLimit;

        if (isOutOfBounds)
        {
            Destroy(gameObject);
        }
    }
}
