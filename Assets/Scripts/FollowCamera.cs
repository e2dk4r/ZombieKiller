using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;

    public float distance = 3.0f;

    public float height = 3.0f;

    public float damping = 5.0f;

    public bool followBehind = true;



    void Update()

    {

        Vector3 wantedPosition;

        if (followBehind)

            wantedPosition = target.TransformPoint(0, height, -distance);

        else

            wantedPosition = target.TransformPoint(0, height, distance);



        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

    }

}
