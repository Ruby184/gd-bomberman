using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    public float smoothSpeed = 0.5f;

    private Transform target;

    void Awake()
    {
        target = player.GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(target.position.x, target.position.y, transform.position.z),
            smoothSpeed * Time.deltaTime
        );
    }
}
