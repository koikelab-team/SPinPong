using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixateRotation : MonoBehaviour {
    public Transform target;
    private Quaternion rotation;
    private Vector3 diff;
    void Start() {
        rotation = transform.rotation;
        diff = target.position - transform.position;
    }
    void Update() {
        transform.rotation = rotation;
        transform.position = target.position - diff;
    }
}
