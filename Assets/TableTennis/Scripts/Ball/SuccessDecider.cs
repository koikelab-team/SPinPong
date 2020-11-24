using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessDecider : MonoBehaviour
{
    void Start() {
        EventManager.StartListening("Serve", onServe);
    }

    int tableHitCount;
    void onServe(object arg) {
        tableHitCount = 0;
    }

    void OnCollisionEnter(Collision collisionInfo) {
        if (!this.enabled)
            return;
        if (collisionInfo.collider.name == "AiSideTable" || collisionInfo.collider.name == "UserSideTable" || collisionInfo.collider.name == "Wall") {
            if (collisionInfo.collider.name == "AiSideTable") {
                if (tableHitCount == 2) {
                    EventManager.TriggerEvent("SuccessfulReturn");
                }
            } else if (collisionInfo.collider.name == "UserSideTable") {
            }
            tableHitCount++;
        }
    }

}
