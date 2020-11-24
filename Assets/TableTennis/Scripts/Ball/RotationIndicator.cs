using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationIndicator : MonoBehaviour {

    private GameObject ball;
    private Rigidbody ballrb;
    private Renderer render;

    void Start() {
        ball = transform.parent.gameObject;
        ballrb = ball.GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
        render.enabled = false;
        currentEulerAngles = new Vector3(0, 0, 0);
    }

    private float thresh = 200f;
    private float x, y, z;
    private int rotationIdx = -1;
    private int lastrotationIdx = -1;
    private float direction = -1;
    private float lastdirection = -1;
    private float rotationVel = 1000f;
    void FixedUpdate() {
        x = ballrb.angularVelocity[0];
        y = ballrb.angularVelocity[1];
        z = ballrb.angularVelocity[2];
        lastrotationIdx = rotationIdx;
        lastdirection = direction;
        if (Mathf.Abs(x) > thresh || Mathf.Abs(y) > thresh || Mathf.Abs(z) > thresh) {
            if (Mathf.Abs(x) > Mathf.Abs(y) && Mathf.Abs(x) > Mathf.Abs(z)) {
                rotationIdx = 0;
            } else if (Mathf.Abs(y) > Mathf.Abs(z)) {
                rotationIdx = 1;
            } else {
                rotationIdx = 2;
            }
            direction = Mathf.Sign(ballrb.angularVelocity[rotationIdx]);
            if (lastrotationIdx != rotationIdx || direction != lastdirection) {
                //rotationVel = ball.angularVelocity[rotationIdx] * 1f;
                if (rotationIdx == 0) {
                    if (direction == 1f)
                        currentEulerAngles = new Vector3(0, 0, -90);
                    else
                        currentEulerAngles = new Vector3(0, 0, 90);
                } else if (rotationIdx == 1) {
                    if (direction == 1f)
                        currentEulerAngles = new Vector3(0, 0, 0);
                    else
                        currentEulerAngles = new Vector3(0, 0, 180);
                } else if (rotationIdx == 2) {
                    // TODO: unimplemented. mb not needed, this is the cork-rotation
                }
            }
        } else {
            rotationIdx = -1;
        }
        render.enabled = rotationIdx >= 0 && rotationIdx != 2; // 2 unimplemented
        //print(currentEulerAngles);
    }
    private Vector3 currentEulerAngles;
    private Quaternion currentRotation;
    void Update() {
        //transform.position = ball.transform.position;
        if (rotationIdx >= 0) {
            //transform.rotation[rotationIdx] += rotationVel;
            //currentEulerAngles += new Vector3(x, y, z) * Time.deltaTime * rotationSpeed;
            //currentEulerAngles[rotationIdx] += rotationVel * Time.deltaTime;
            currentEulerAngles[rotationIdx] += direction * rotationVel * Time.deltaTime;
        }
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;
        //transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
