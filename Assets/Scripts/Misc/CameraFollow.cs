using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject follower;

    private Vector3 offSet;

    private void Awake()
    {
        offSet = transform.position - follower.transform.position;
    }

    private void Update()
    {
        transform.position = follower.transform.position + offSet;
    }
}
