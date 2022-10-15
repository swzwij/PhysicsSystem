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

    [SerializeField] private float BoxSize = 2;

    [SerializeField] private GameObject visual;

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
        //Collider[] hitColliders = Physics.OverlapBox(desiredPos, transform.localScale * BoxSize, Quaternion.identity, groundLayer);
        var a = Physics.Raycast(transform.position, desiredPos, (transform.position - desiredPos).magnitude, groundLayer);
        Debug.DrawRay(transform.position, desiredPos, Color.blue);
        visual.transform.position = desiredPos;
        return a;
    }

    private void ApplyGravity()
    {
        if (!hasGravity) return;

        var newVelocity = (velocity / 10) + (-gravityScale / 10);
        var desiredPos = transform.position += newVelocity;

        SetPosition(desiredPos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
