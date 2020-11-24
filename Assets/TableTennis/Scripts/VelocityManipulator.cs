#pragma warning disable 0108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VelocityManipulator : MonoBehaviour
{

    public SteamVR_Behaviour_Pose trackedObj;

    internal Vector3 lastAppliedVelocity = new Vector3(0, 0, 0);
    internal Vector3 lastAppliedAngularVelocity = new Vector3(0, 0, 0);

    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void updateVelocities() {
        if (!trackedObj.isValid)
            return;
        // multiply by timeScale so that the amounnt of force required to counter the ball remains the same as without slow-mo
        //rigidbody.velocity = trackedObj.GetVelocity() * Time.timeScale * 2;
        //rigidbody.angularVelocity = trackedObj.GetAngularVelocity() * Time.timeScale * 2;
        rigidbody.velocity = trackedObj.GetVelocity() * 3;
        rigidbody.angularVelocity = trackedObj.GetAngularVelocity() * 2;
        //rigidbody.angularVelocity = new Vector3(0, 0, 0);
        lastAppliedVelocity = rigidbody.velocity;
        lastAppliedAngularVelocity = rigidbody.angularVelocity;
    }
    void FixedUpdate() {
        updateVelocities();
    }
	void OnCollisionEnter(Collision collisionInfo) {
        //updateVelocities();
    }
	void OnCollisionStay(Collision collisionInfo) {
        //updateVelocities();
    }
	void OnCollisionExit(Collision collisionInfo) {
        //updateVelocities();
    }
	// void OnCollisionEnter(Collision collisionInfo) {
    //     if (collisionInfo.collider.name == "Ball") {
    //         Debug.Log("!vel : " + rigidbody.velocity + " vs " + trackedObj.GetVelocity());
    //     }
    // }

}