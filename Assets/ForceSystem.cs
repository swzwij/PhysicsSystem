using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSystem : MonoBehaviour
{
    private List<PhysicsBody> _forceBodies = new List<PhysicsBody>();

    private void Awake()
    {
        _forceBodies.Add(FindObjectOfType<PhysicsBody>());
    }
}
