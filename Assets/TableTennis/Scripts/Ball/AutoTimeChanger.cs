using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTimeChanger : MonoBehaviour
{

    /** Idea:
     * start at 'minimum' timeScale.
     * increase by timeStep as player gets better at lower speeds
     * observe this by checking how many times he returns the ball successfully at every timeStep
     */

    // code for managing activation status
    internal bool isActive;
    private float lastValue;

	void Start () {
        perStepTotal = perStepTotalDefault;
        disable();
        lastValue = minimum;
        //lastValue = 0.7f;
        EventManager.StartListening("Serve", onBallServe);
	}

    public void toggle() {
        if (isActive) {
            disable();
        } else {
            enable();
        }
    }
    public void enable() {
        isActive = true;
        Time.timeScale = lastValue;
        ballAlive = false;
        Logger.Instance.write("AUTO enable (timeScale: " + Time.timeScale + ")");
    }
    public void disable() {
        lastValue = Time.timeScale;
        isActive = false;
        Time.timeScale = 1f;
        ballAlive = false;
        Logger.Instance.write("AUTO disable (timeScale: " + Time.timeScale + ")");
    }

    // code for adjusting time
    float timestep = 0.1f;
    float minimum = 0.4f;
    float maximum = 2.0f;
    public int perStepTotalDefault;
    internal int perStepTotal;
    internal int perStepCount;
    /*
     * only change time if we saw 'perStepTotal' successful returns in sequence
     */
    private void increase() {
        // count successful returns now
        if (perStepCount < 0)
            perStepCount = 0;
        perStepCount += 1;
        // if we reach the step's limit, increase step if lower than max
        if (perStepCount >= perStepTotal) {
            Time.timeScale = Mathf.Round((Time.timeScale + timestep)*10f) / 10f;
            if (Time.timeScale > maximum)
                Time.timeScale = maximum;
            else
                perStepCount = 0;
        }
    }
    private void decrease() {
        // count unsuccessful returns now
        if (perStepCount > 0)
            perStepCount = 0;
        /*
        perStepCount -= 1;
        if (perStepCount <= -perStepTotal) {
            Time.timeScale = Mathf.Round((Time.timeScale - timestep)*10f) / 10f;
            if (Time.timeScale < minimum)
                Time.timeScale = minimum;
            else
                perStepCount = 0;
        }
        */
    }

    // code to manage automatic change of time
    bool ballAlive;
    float remaining;
    int tableHit;
    void FixedUpdate() {
        btime += Time.fixedDeltaTime;
        remaining -= Time.fixedDeltaTime;
        if (remaining <= 0f)
            checkResult();
    }
    float btime = 0;
    public void onBallServe(object obj) {
        // if user wants to serve the next ball without seeing its result, count it as 'missed'.
        checkResult();
        btime = 0;
    }
    void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.collider.name == "UserSideTable") {
            print("Virtual time until table hit: " + btime);
        } else if (collisionInfo.collider.tag == "racket") {
            print("Virtual time until racket hit: " + btime);
        }
        if (!isActive)
            return;
        // start counting after racket hit
		if (collisionInfo.collider.tag == "racket") {
            remaining = 1f; // set to 1 seconds
            ballAlive = true;
            return;
        }
        // if we hit net, still ok
		if (collisionInfo.collider.name == "net")
            return;
        // if we hit anything other than racket or net, check result
        checkResult();
    }
    private void checkResult() {
        if (ballAlive) {
            if (tableHit >= 1)
                increase();
            else
                decrease();
            Logger.Instance.write("AUTO timeScale " + Time.timeScale);
            // in this experiment, go to normal when we hit 1.0 speed
            if (Time.timeScale >= 1.0)
                this.disable();
        }
        checkStepTotal();
        tableHit = 0;
        ballAlive = false;
    }
    private void checkStepTotal() {
        // double steptotal beyond 1.0f
        if (Time.timeScale >= 1.0f)
            perStepTotal = perStepTotalDefault * 2;
        else
            perStepTotal = perStepTotalDefault;
    }

}
