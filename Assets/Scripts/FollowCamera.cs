using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject targetThing;
    public float offset;
    private float targetPos = 0;
    public float smooth = 5;
    private Vector3 distance;

    public void Start()
    {
        distance = transform.position - targetThing.transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = distance + targetThing.transform.position;
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, targetPos, smooth * Time.fixedDeltaTime);
        pos.z = Mathf.Lerp(pos.z, targetPos, smooth * Time.fixedDeltaTime);
        transform.position = pos;
    }
}
