using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour
{
    public int TOTAL_SETS = 20;
    public float INTERVAL = 4.0f;
    public GameObject ball;
    //private RecordingData recordingData;

    int serveType;

    private Vector3 NextBallRPM = new Vector3(2000, -8000, 0);

    // Start is called before the first frame update
    void Start() {
        //recordingData = gameObject.GetComponent<RecordingData>();
        serveType = 0;
    }

    // Update is called once per frame
    void Update() {
        /*if (recordingData.requestServe) {
            EventManager.TriggerEvent("RequestServe", serveType);
            recordingData.requestServe = false;
        }*/
        if (Input.GetKeyDown(KeyCode.Space)) {
            mainButton();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            condition(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            condition(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            condition(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            condition(4);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            secondaryButton();
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            tertiaryButton();
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            serveType = (serveType + 1) % 3;
            print("Serve Type: " + serveType);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        //if (Input.GetKeyUp(KeyCode.Space)) {
        //    Debug.Log("Space released.");
        //}
    }

    List<int> serves = new List<int>();

    private void condition(int cond) {
        if (serves.Count > 0) {
            return;
        }
        populateServes();
        Logger.Instance.write("Request condition " + cond);
        EventManager.TriggerEvent("RequestCondition", cond);
    }
    public void mainButton() {
        serves = new List<int>();
    }
    public void populateServes() {
        if (serves.Count > 0) {
            return;
        }
        //EventManager.TriggerEvent("RequestCondition", condition);

        //placeBallAboveRacket();
        //serveBall();
        //recordingData.requestServe = true;
        //EventManager.TriggerEvent("RequestServe", serveType);

        serves = new List<int>();
        for (int i = 0; i < TOTAL_SETS; i++) {
            serves.Add(0);
            serves.Add(1);
            serves.Add(2);
        }
        System.Random r = new System.Random();
        serves = serves.OrderBy(a => r.Next(serves.Count)).ToList();
        /*for (int i = 0; i < serves.Count; i++) {
            EventManager.TriggerEvent("RequestVideoServe", serves[i]);
            Time.Sleep(3f);
        }*/
        startServe();
    }
    private void startServe() {
        int serve = serves[0];
        serves.RemoveAt(0);
        Logger.Instance.write("Request Serve Type " + serve);
        EventManager.TriggerEvent("RequestVideoServe", serve);
        if (serves.Count > 0) {
            StartCoroutine(AutoClick.InvokeRealtimeCoroutine(startServe, INTERVAL));
            //Invoke("startServe", 3.0f);
        }
    }
    public void secondaryButton() {
        toggleSlowMo();
    }
    public void tertiaryButton() {
        toggleGhost();
    }
    private void toggleSlowMo() {
        ball.GetComponent<AutoTimeChanger>().toggle();
    }
    private void toggleGhost() {
        ball.GetComponent<GhostPlacer>().toggle();
    }
    public void changeTimeScale(int steps) {
        Time.timeScale = Mathf.Round(Time.timeScale*10f + 1f*steps) / 10f;
        Logger.Instance.write("USER timeScale " + Time.timeScale);
    }
    /*
    public void setNextBall(int h, int v) {
        NextBallRPM += 2000 * new Vector3(v, h, 0);
        for (int i=0; i<3; i++) {
            if (NextBallRPM[i] > 4000)
                NextBallRPM[i] = 4000;
            else if (NextBallRPM[i] < -4000)
                NextBallRPM[i] = -4000;
        }
        Debug.Log("set ball: " + NextBallRPM);
    }
    private void placeBallAboveRacket() {
        //ball.transform.position = this.transform.position + new Vector3(0, 1, 0);
        Debug.Log("Placing ball above racket.");
		Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
        rigidbody.MovePosition(this.transform.position + new Vector3(0, 1, 0));
		rigidbody.velocity = new Vector3(0, 0, 0);
		rigidbody.angularVelocity = new Vector3(0, 0, 0);
    }
    */
}
