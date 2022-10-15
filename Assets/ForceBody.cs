using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ForceBody : MonoBehaviour
{
    [Header(" ")]
    [SerializeField] private bool hasGravity;
    [SerializeField] private Vector3 gravityScale;

    [Header(" ")]
    [SerializeField] private LayerMask groundLayer;

    [Header(" ")]
    [SerializeField] private float offset;

    [Header(" ")]
    [SerializeField] public Vector3 velocity;

    [Header(" ")]
    [SerializeField] private float BoxSize = 2;

    [SerializeField] private GameObject visual;

    private void FixedUpdate()
    {
        ApplyGravity();
        ApplyVelocity();
    }

    private void SetPosition(Vector3 desiredPos)
    {
        Vector3 lastPos = transform.position += new Vector3(gravityScale.x * offset, gravityScale.y * offset, gravityScale.z * offset);

        if (CheckCollision(desiredPos)) transform.position = lastPos;
        else transform.position = desiredPos;
    }

    private bool CheckCollision(Vector3 desiredPos)
    {
        Collider[] hitColliders = Physics.OverlapBox(desiredPos, transform.localScale * BoxSize, Quaternion.identity, groundLayer);
        visual.transform.position = desiredPos;
        return hitColliders.Length > 0;
    }

    private void ApplyGravity()
    {
        if (!hasGravity) return;

        var desiredPos = transform.position + -gravityScale;

        SetPosition(desiredPos);
    }

    private void ApplyVelocity()
    {
        var desiredVelocity = transform.position + velocity;

        SetPosition(desiredVelocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
