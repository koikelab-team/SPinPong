using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{

    public GameObject ballArrow;
    public ToggleBallCamera toggleBallCamera;
    public GradualTimeChanger gradualTimeChanger;
    public TrajectoryLoader trajectoryLoader;
    public GameObject AutoRacket;


    // Start is called before the first frame update
    void Start() {
        EventManager.StartListening("RequestCondition", onCondition);        
    }

    /*
    haptics + video in all cases
    1. base: slowmo
    2. bullet time
    3. trajectory + slowmo + guide
    4. spin arrow + slowmo
    */

    void onCondition(object arg) {
        int condition = (int) arg;

        gradualTimeChanger.enabled = true;
        toggleBallCamera.enabled = false;
        trajectoryLoader.enabled = false;
        AutoRacket.SetActive(false);
        ballArrow.SetActive(false);

        if (condition == 2) {
            gradualTimeChanger.enabled = false;
            toggleBallCamera.enabled = true;
        } else if (condition == 3) {
            trajectoryLoader.enabled = true;
            AutoRacket.SetActive(true);
        } else if (condition == 4) {
            ballArrow.SetActive(true);
        }
    }
}
