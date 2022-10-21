using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PhysicsBody : MonoBehaviour
{
    [Header(" ")]
    [SerializeField] private bool hasGravity;
    [SerializeField] private Vector3 gravityScale;

    [Header(" ")]
    [SerializeField] private LayerMask groundLayer;

    [Header(" ")]
    [SerializeField] private Vector3 offset;

    [Header(" ")]
    [SerializeField] public Vector3 velocity;

    [Header(" ")]
    [SerializeField] private float BoxSize = 2;

    [SerializeField] private GameObject visual;

    private void FixedUpdate()
    {
        ApplyVelocity();
    }

    private void SetPosition(Vector3 desiredVelocity)
    {
        var a = CheckCollision(desiredVelocity);
        transform.position = a;
    }

    private Vector3 CheckCollision(Vector3 desiredVelocity)
    {
        Vector3 fromPos = transform.position;
        Vector3 toPos = transform.position + desiredVelocity;
        Vector3 dir = toPos - fromPos;
        Vector3 dir2 = fromPos - toPos;

        float length = 0;

        if (desiredVelocity.magnitude >= .6f) length = desiredVelocity.magnitude;
        else length = .6f;

        var p = Physics.Raycast(fromPos, dir, length, groundLayer);
        //Collider[] hitColliders = Physics.OverlapBox(toPos, transform.localScale * BoxSize, Quaternion.identity, groundLayer);
        visual.transform.position = toPos;

        if (p)
        {
            RaycastHit hit;
            Physics.Raycast(toPos, Vector3.up, out hit, length, groundLayer);
            var b = hit.point + fromPos;
            print(b += offset);
            return b += offset;
        }
        else
        {
            return toPos;
        }
    }

    private void ApplyVelocity()
    {
        var desiredVelocity = velocity + -gravityScale;

        SetPosition(desiredVelocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
