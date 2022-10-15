using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ForceBody : MonoBehaviour
{
    [SerializeField] private bool hasGravity;
    [SerializeField] private Vector3 gravityScale;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float offset;
    [SerializeField] private Vector3 velocity;

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    private void SetPosition(Vector3 desiredPos)
    {
        Vector3 lastPos = transform.position += new Vector3(0, offset, 0);

        if (CheckCollision(desiredPos)) transform.position = lastPos;
        else transform.position = desiredPos;
    }

    private bool CheckCollision(Vector3 desiredPos)
    {
        Collider[] hitColliders = Physics.OverlapBox(desiredPos, transform.localScale / 2, Quaternion.identity, groundLayer);
        return hitColliders.Length > 0;
    }

    private void ApplyGravity()
    {
        if (!hasGravity) return;

        var newVelocity = velocity + (-gravityScale/10);
        var desiredPos = transform.position += newVelocity;

        SetPosition(desiredPos);
    }
}
