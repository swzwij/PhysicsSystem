using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moment : MonoBehaviour
{
    private PhysicsBody body;

    private void Awake()
    {
        body = GetComponent<PhysicsBody>();
    }

    private void Update()
    {
        int s = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            s = 1;
        }
        else s = 0;
        body.SetVelocity(new Vector3(Input.GetAxisRaw("Horizontal"), s, Input.GetAxisRaw("Vertical")));
    }
}
