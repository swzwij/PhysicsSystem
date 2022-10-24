using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Vector3 fromPos = transform.position;
        Vector3 toPos = transform.position + desiredVelocity;
        Vector3 dir = toPos - fromPos;
        Vector3 dir2 = fromPos - toPos;

        float length = 0;

        if (desiredVelocity.magnitude >= .55f) length = desiredVelocity.magnitude;
        else length = .6f;

        var p = Physics.Raycast(fromPos, dir, length);
        //Collider[] hitColliders = Physics.OverlapBox(toPos, transform.localScale * BoxSize, Quaternion.identity, groundLayer);
        visual.transform.position = toPos;

        if (p)
        {
            RaycastHit hit;
            Physics.Raycast(toPos, Vector3.up, out hit, length);
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
        var desiredVelocity = velocity;

        SetPosition(desiredVelocity);
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
