using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ForceBody : MonoBehaviour
{
    [SerializeField] private float mass;

    [SerializeField] private bool hasGravity;
    [SerializeField] private Vector3 gravityScale;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float offset;

    private bool _isColliding;

    private void Update()
    {
        CheckCollision();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    private void SetPosition(Vector3 desiredPos)
    {
        Vector3 lastPos = transform.position;

        if (_isColliding) transform.position = lastPos += new Vector3(0,offset,0);
        else transform.position = desiredPos;
    }

    private void CheckCollision()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, groundLayer);
        _isColliding = hitColliders.Length > 0;
    }

    private bool CheckDesiredVelocity(Vector3 desiredPos)
    {
        var check = Physics.Raycast(desiredPos, desiredPos, .1f, groundLayer);
        print(check);
        return check;
    }

    private void ApplyGravity()
    {
        if (!hasGravity) return;

        var velocity = (-gravityScale / 5) / mass;
        var desiredPos = transform.position += velocity;

        //if (CheckDesiredVelocity(desiredPos)) return;
        SetPosition(desiredPos);
    }
}
