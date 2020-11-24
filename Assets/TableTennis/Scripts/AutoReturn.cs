#pragma warning disable 0108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoReturn : MonoBehaviour {
    Rigidbody rigidbody;
    Vector3 startpos;
    Quaternion startrotation;
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        EventManager.StartListening("Serve", onServe);
        time = duration;
        startpos = transform.position;
        startrotation = transform.rotation;
    }
    float duration = 0.6f; // seconds
    float swingAmount = 0.20f; // meters

    int serveType;
    float time;
    float ychange;
    float prepareDuration = 0.44f;
    void onServe(object arg) {
        Invoke("delayed", prepareDuration);
        serveType = (int) arg;
        ychange = 0f;
        if (serveType == 0) {
            //startpos = new Vector3(-0.414f, 0.981f, -1.825f);
            //startrotation = Quaternion.Euler(-10f, -40f, -90f);
            startpos = new Vector3(-0.200f, 1.100f, -1.825f);
            startrotation = Quaternion.Euler(-5f, -40f, -180f);
        } else if (serveType == 1) {
            //startpos = new Vector3(0.2f, 0.981f, -1.6f);
            //startrotation = Quaternion.Euler(-10f, 40f, -90f);
            startpos = new Vector3(0.200f, 1.100f, -1.825f);
            startrotation = Quaternion.Euler(-5f, 40f, -180f);
        } else if (serveType == 2) {
            //startpos = new Vector3(-0.38f, 0.94f, -1.625f);
            //startrotation = Quaternion.Euler(-50f, 0f, -90f);
            startpos = new Vector3(-0.200f, 1.03f, -1.59f);
            startrotation = Quaternion.Euler(-45f, 0f, -180f);
            ychange = 0.13f;
        }
        time = -prepareDuration;
    }
    void delayed() {
        //startpos = transform.position;
        time = 0f;
    }

    float calcdelta(float p) {
        return -swingAmount * Mathf.Sin(2*Mathf.PI * p);
    }

    Vector3 prepareInitialPos = new Vector3(0f, 1.0f, -1.1f);
    Quaternion prepareInitialRot = Quaternion.Euler(0f, 0f, 180f);
    Vector3 lastpos;
    void FixedUpdate() {
        lastpos = transform.position;
        time += Time.fixedDeltaTime;
        if (time < 0) {
            transform.position = prepareInitialPos + (startpos - prepareInitialPos) * (prepareDuration + time) / prepareDuration;
            transform.rotation = Quaternion.Slerp(prepareInitialRot, startrotation, (prepareDuration + time) / prepareDuration);
            return;
        } else if (time < duration) {
            float z = calcdelta(time / duration);
            transform.position = startpos + new Vector3(0, z * ychange, z);
        } else {
            transform.position = startpos;
            lastpos = startpos;
        }
        transform.rotation = startrotation;
        rigidbody.velocity = - (lastpos - transform.position) * 200;
        rigidbody.angularVelocity = new Vector3(0, 0, 0);
        //print(rigidbody.velocity.ToString("F4"));
    }
}
