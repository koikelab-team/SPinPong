using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradualTimeChanger : MonoBehaviour
{
    System.Diagnostics.Stopwatch sw1 = new System.Diagnostics.Stopwatch();
    System.Diagnostics.Stopwatch sw2 = new System.Diagnostics.Stopwatch();
    float duration = 0.1f;
    float startTS;
    float targetTS = 1.0f;

    void Start() {
        EventManager.StartListening("Serve", onServe);
    }

    void onServe(object obj) {
        if (!this.enabled) {
            return;
        }
        Time.timeScale = 0.5f;
        sw1.Reset();
        sw1.Start();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (sw2.IsRunning) {
            Time.timeScale = (targetTS - startTS) * sw2.ElapsedMilliseconds / (duration * 1000) + startTS;
            if (sw2.ElapsedMilliseconds > duration * 1000) {
                sw2.Stop();
                Time.timeScale = targetTS;
            }
        }
    }

    void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.collider.name == "UserSideTable") {
            Debug.Log("Real time until table hit: " + sw1.ElapsedMilliseconds/1000f);
            sw2.Reset();
            startTS = Time.timeScale;
            sw2.Start();
        }
		if (collisionInfo.collider.tag == "racket") {
            Debug.Log("Real time until racket hit: " + sw1.ElapsedMilliseconds/1000f);
        }
    }
}
