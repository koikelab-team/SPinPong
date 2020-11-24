using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentText : MonoBehaviour
{

    int returns;
    int serves;

    Text text;
    void Start() {
        text = GetComponent<Text>();
        EventManager.StartListening("RequestCondition", onNewCondition);
        EventManager.StartListening("Serve", onServe);
        EventManager.StartListening("SuccessfulReturn", onSuccReturn);
    }
    void onNewCondition(object arg) {
        serves = 0;
        returns = 0;
        updateText();
    }
    void onServe(object arg) {
        serves++;
        updateText();
    }
    void onSuccReturn(object arg) {
        returns++;
        Logger.Instance.write("Succesful return: " + returns);
        updateText();
    }
    void updateText() {
        text.text = "Succ. returns: " + returns + "/" + serves;
    }

    //void Update() {
        // do this in Update instead of in FixedUpdate to allow seeing the time change to 0.0f
        //text.text = "Ghost:\n" + (ghost.isActive?"enabled":"disabled") + "\n\n"
        //          + "Time:\n" + Time.timeScale + "\n" + (autotime.isActive?(autotime.perStepCount+"/"+autotime.perStepTotal):"");
        // Mathf.Round(Time.timeScale * 10f) / 10
    //}
}
