using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    [SerializeField] Vector3 offset = new Vector3(0, 25, 0);

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
