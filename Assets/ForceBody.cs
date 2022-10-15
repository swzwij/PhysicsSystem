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
    [SerializeField] public Vector3 velocity;

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

        var newVelocity = -gravityScale;

        var desiredPosX = transform.position.x + newVelocity.x;
        var desiredPosY = transform.position.y + newVelocity.y;
        var desiredPosZ = transform.position.z + newVelocity.z;

        print(desiredPosX + ", " + desiredPosY + ", " + desiredPosZ);  

        var desiredPos = new Vector3(desiredPosX, desiredPosY, desiredPosZ);

        SetPosition(desiredPos);
    }

    private void ApplyVelocity()
    {
        var desiredPosX = transform.position.x + velocity.x;
        var desiredPosY = transform.position.y + velocity.y;
        var desiredPosZ = transform.position.z + velocity.z;

        print(desiredPosX + ", " + desiredPosY + ", " + desiredPosZ);

        var desiredPos = new Vector3(desiredPosX, desiredPosY, desiredPosZ);

        SetPosition(desiredPos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
