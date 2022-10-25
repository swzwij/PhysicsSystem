using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UIElements;

public class PhysicsBody : MonoBehaviour
{
    [SerializeField] private GameObject visual;


    [Header("Gravity")]
    [SerializeField] private bool hasGravity;
    [SerializeField] private float gravityScale;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Vector3 offset;


    private Vector3 velocity;

    private int sleepCounter;
    public bool isSleeping => sleepCounter > 0;

    private void FixedUpdate()
    {
        if(isSleeping)
        {
            sleepCounter--;
            return;
        }

        ApplyVelocity();
        ApplyGravity();
    }

    private void SetPosition(Vector3 desiredVelocity)
    {
        var a = CheckCollision(desiredVelocity);
        transform.position = a;
    }

    private Vector3 CheckCollision(Vector3 desiredVelocity)
    {
        //* get length from current position to desired position
        // move a cast along the lentgh with steps
        // if finaly a step returns true (hits collider) set position to last step
        // if it went trough all steps without returning true move to desired pos
        
        Vector3 currentPos = transform.position;
        Vector3 desiredPos = transform.position + desiredVelocity;
        Vector3 dir = desiredPos - currentPos;

        float length = 1;

        if(Physics.Raycast(currentPos, dir, length))
        {
            return currentPos;
        }
        else
        {
            return desiredPos;
        }
    }

    private void ApplyVelocity()
    {
        SetPosition(velocity.normalized);
    }

    private void ApplyGravity()
    {
        var desiredVelocity = new Vector3(0, -gravityScale, 0);

        SetPosition(desiredVelocity);
    }

    public void SetVelocity(Vector3 vel)
    {
        velocity = vel;
    }

    #region Sleeping
    public void Sleep(int frames)
    {
        if (!isSleeping) sleepCounter = frames;
    }

    public void WakeUp()
    {
        sleepCounter = 0;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
