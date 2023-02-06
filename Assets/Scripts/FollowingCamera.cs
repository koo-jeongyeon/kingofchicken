using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public float distanceAway;

    public float distanceUp;

    public Transform follow;
    
    // Start is called before the first frame update
    void Start()
    {
        distanceAway = 14f;
        distanceUp = 10f;
        transform.rotation = Quaternion.Euler(35, 0, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = follow.position + Vector3.up * distanceUp - Vector3.forward * distanceAway;
    }
}
