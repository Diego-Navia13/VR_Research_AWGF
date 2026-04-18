using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleBoundary : MonoBehaviour
{
    public Rigidbody target;
    public Transform centerPoint;

    public float radius = 5f;
    public float height = 3f;
    public float boundaryForce = 6f;

    void FixedUpdate()
    {
        Vector3 localPos = target.position - centerPoint.position;

        float halfHeight = height * 0.5f;

        Vector3 horizontal = new Vector3(localPos.x, 0, localPos.z);
        float horizontalDist = horizontal.magnitude;

        float vertical = localPos.y;

        Vector3 force = Vector3.zero;

        // Side walls
        if (horizontalDist > radius)
        {
            Vector3 dir = horizontal.normalized;
            float overflow = horizontalDist - radius;
            force += -dir * overflow * boundaryForce;
        }

        // Top
        if (vertical > halfHeight)
        {
            force += Vector3.down * (vertical - halfHeight) * boundaryForce;
        }

        // Bottom
        if (vertical < -halfHeight)
        {
            force += Vector3.up * (-halfHeight - vertical) * boundaryForce;
        }

        target.AddForce(force, ForceMode.Acceleration);
    }
}