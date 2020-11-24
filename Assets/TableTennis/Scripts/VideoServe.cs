using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoServe : MonoBehaviour {

    private VideoPlayer vPlayer;

    public VideoClip clip1;
    public int startFrame1;
    public int serveFrame1;
    public VideoClip clip2;
    public int startFrame2;
    public int serveFrame2;
    public VideoClip clip3;
    public int startFrame3;
    public int serveFrame3;

    public BallManager ball;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start() {
        initialPosition = transform.position;
        vPlayer = GetComponent<VideoPlayer>();
        vPlayer.prepareCompleted += onVidPrepared;
        serveType = 0;
        EventManager.StartListening("RequestVideoServe", initVideoServe);
    }
    int serveType;
    private float after = 0.5f; // sec
    private int framediff = 0;
    public void initVideoServe(object arg) {
        transform.position = initialPosition;
        serveType = (int) arg;
        if (serveType == 0) {
            vPlayer.clip = clip1;
            vPlayer.frame = startFrame1;
            framediff = serveFrame1 - startFrame1;
        } else if (serveType == 1) {
            vPlayer.clip = clip2;
            vPlayer.frame = startFrame2;
            framediff = serveFrame2 - startFrame2;
            //framediff = 65;
        } else if (serveType == 2) {
            transform.position += new Vector3(0, -0.075f, 0);
            vPlayer.clip = clip3;
            vPlayer.frame = startFrame3;
            framediff = serveFrame3 - startFrame3;
        }
        vPlayer.Prepare();
    }
    void onVidPrepared(UnityEngine.Video.VideoPlayer vP) {
        vPlayer.Play();
        Invoke("onActuallyPrepared", 0.35f);
    }
    void onActuallyPrepared() {
        float timeUntilServe = 1 / vPlayer.frameRate * framediff;
        //timeUntilServe *= 1.5f;
        //Debug.Log(">>>>> " + timeUntilServe);
        //Debug.Log(">>>>> " + Time.time);
        ball.throwBall(5.5f * framediff / 58f);
        Invoke("doServe", timeUntilServe);
        Invoke("endServe", timeUntilServe + after);
    }
    void doServe() {
        //Debug.Log(">>>>> " + Time.time);
        EventManager.TriggerEvent("Serve", serveType);
    }
    void endServe() {
        vPlayer.Stop();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
