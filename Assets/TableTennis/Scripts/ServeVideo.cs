using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ServeVideo : MonoBehaviour {
    private VideoPlayer vPlayer;

    void Start() {
        vPlayer = GetComponent<VideoPlayer>();
        vPlayer.frame = sframe;
        vPlayer.Play();
        Debug.Log("frameconut " + vPlayer.frameCount);
        Debug.Log("frameRate " + vPlayer.frameRate);
        Debug.Log("length " + vPlayer.length);
        EventManager.StartListening("ConutdownUntilServe", countdownUntilServe);
    }

    // 25fps video
    // start serve @ 1m 1s 18th frame
    // hit serve @ 1m 2s 22th frame
    // end serve @ 1m 3s 15th frame
    static float fps = 25f;
    static int sframe = (int) ((1 * 60 + 1) * fps + 18);
    static int hframe = (int) ((1 * 60 + 2) * fps + 22);
    static int eframe = (int) ((1 * 60 + 3) * fps + 15);
    float constantT = 0.2f + 0.572f;

    void countdownUntilServe(object arg) {
        float time = (float) arg;

        float secondsUntilHit = (hframe - sframe)/fps;
        float secondsAfterHit = (eframe - hframe)/fps;
        Debug.Log("remaining until serve: " + time + " phase1: " + secondsUntilHit + " phase2: " + secondsAfterHit);
        StartCoroutine(AutoClick.InvokeRealtimeCoroutine(PlayVideo, time - secondsUntilHit - constantT));
        StartCoroutine(AutoClick.InvokeRealtimeCoroutine(StopVideo, time + secondsAfterHit - constantT));
    }
    void PlayVideo() {
        Debug.Log("Start video!");
        vPlayer.frame = sframe;
        vPlayer.Play();
    }
    void StopVideo() {
        Debug.Log("Stop video!");
        vPlayer.Stop();
    }
}
