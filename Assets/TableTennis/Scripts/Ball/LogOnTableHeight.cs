#pragma warning disable 0108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOnTableHeight : MonoBehaviour {

    Rigidbody rigidbody;
    bool reported;
    bool groundhit;
    int aitable;

    void Start() {
        EventManager.StartListening("Serve", onBallServe);
        rigidbody = GetComponent<Rigidbody>();
        reported = true;
        groundhit = true;
    }
    void onBallServe(object arg) {
        if (!reported) {
            Logger.Instance.write("OnTableHeight: undefined");
        }
        groundhit = false;
        reported = false;
        aitable = 0;
    }
    void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.collider.name == "Wall") {
            groundhit = true;
        } else if (collisionInfo.collider.name == "AiSideTable") {
            if (aitable == 2) {
                float x = collisionInfo.contacts[0].point[0];
                float z = collisionInfo.contacts[0].point[2];
                Logger.Instance.write("OnTableHeight: success? (" + x + ", " + z + ")");
                reported = true;
                groundhit = true;
            }
            aitable++;
        }
    }

    void FixedUpdate() {
        if (groundhit) {
            return;
        }
        float x = transform.position[0];
        float y = transform.position[1];
        float z = transform.position[2];
        float vy = rigidbody.velocity[1];
        if (!reported && vy < 0 && y < 0.76) {
            Logger.Instance.write("OnTableHeight: (" + x + ", " + z + ")");
            reported = true;
        } else if (y >= 0.76) {
            reported = false;
        }
    }
}
