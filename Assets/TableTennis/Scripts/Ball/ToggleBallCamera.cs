using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBallCamera : MonoBehaviour {

    public Texture stdTexture;
    public Texture bulletTimeTexture;

    public Camera ballCamera;
    public GameObject ballCameraPlane;

    public Renderer bulletTimeRacket;
    public Renderer normalRacket;

    private float initTS;

    private Renderer mRenderer;

    void Start() {
        bulletTimeRacket.enabled = false;
        ballCameraPlane.SetActive(false);
        //ballCamera.enabled = false;
        ballCamera.enabled = true;
        EventManager.StartListening("Serve", onServe);
        mRenderer = GetComponent<Renderer>();
    }
    int serveType = 0;
    void onServe(object obj) {
        if (!this.enabled) {
            return;
        }
        serveType = (int) obj;
        Invoke("bulletTimeStart", 0.2f);
        //Invoke("bulletTimeStart", 0.05f);
        //bulletTimeStart();
    }
    void bulletTimeStart() {
        bulletTimeRacket.enabled = true;
        normalRacket.enabled = false;
        //ballCamera.enabled = true;
        ballCameraPlane.SetActive(true);
        mRenderer.material.SetTexture("_MainTex", bulletTimeTexture);
        initTS = Time.timeScale;
        //Time.timeScale = 0.1f;
        Time.timeScale = 0.01f;
        if (serveType == 2) {
            Time.timeScale = 0.10f;
        }
        //Invoke("reset", 1f * Time.timeScale);
        StartCoroutine(AutoClick.InvokeRealtimeCoroutine(reset, 1f));
    }
    void reset() {
        normalRacket.enabled = true;
        bulletTimeRacket.enabled = false;
        //ballCamera.enabled = false;
        ballCameraPlane.SetActive(false);
        mRenderer.material.SetTexture("_MainTex", stdTexture);
        Time.timeScale = initTS;
    }
/*
    void FixedUpdate() {
        if (transform.position.z < 0) {
            reset();
        }
    }
*/
}
