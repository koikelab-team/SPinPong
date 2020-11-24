#pragma warning disable 0108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRotationFix : MonoBehaviour
{

    public static float factor = 0.0002f;

    private Rigidbody rigidbody;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate() {
        //print(rigidbody.angularVelocity);
        //print(rigidbody.inertiaTensorRotation);
        Vector3 momentum = Vector3.Cross(rigidbody.angularVelocity, rigidbody.velocity);
        rigidbody.velocity = rigidbody.velocity + Time.fixedDeltaTime * momentum * factor;
        //print(rigidbody.velocity);
    }
    /*void Update() {
        //Debug.DrawRay(transform.position, rigidbody.angularVelocity, Color.white, 0, true);
        //Debug.DrawRay(transform.position, Vector3.Cross(rigidbody.angularVelocity, rigidbody.velocity), Color.green, 0, true);

        //Debug.DrawRay(transform.position, rigidbody.inertiaTensorRotation, Color.red, 0, true);
    }*/
}
