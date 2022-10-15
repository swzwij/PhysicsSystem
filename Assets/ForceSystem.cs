using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSystem : MonoBehaviour
{
    private List<ForceBody> _forceBodies = new List<ForceBody>();

    private void Awake()
    {
        _forceBodies.Add(FindObjectOfType<ForceBody>());
    }
}
