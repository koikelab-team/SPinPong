using UnityEngine;
using System.Collections;

public class BallCollisionVibration : MonoBehaviour {

	public SteamVR_Button_Manager controller;
    SerialScript ss;

    void Start() {
        ss = this.GetComponent<SerialScript>(); //シリアル通信(マイコンとの通信)用スクリプト読み込み
        EventManager.StartListening("Serve", onBallServe);
    }

    private int serveType;
    public void onBallServe(object arg) {
        serveType = (int) arg;
    }

	void OnCollisionEnter(Collision collisionInfo) {
		if (!this.enabled)
			return;
		if (collisionInfo.collider.name == "racket_model") {
			//float impact = collisionInfo.relativeVelocity.magnitude;
			//Vector3 rvel = collisionInfo.gameObject.GetComponent<VelocityManipulator>().latestAppliedVelocity;
			//print(" rvel: " + rvel.magnitude + " impact: " + impact + " afterV: " + rigidbody.velocity.magnitude);
			//controller.vibrate((impact - 2.4f) / 3f);
            float lvl = collisionInfo.relativeVelocity.magnitude / 8f;
			controller.vibrate(lvl);
			//controller.vibrate(0.5f);
		} else if (collisionInfo.collider.name == "racket_model ViveTracker") {
			ss.Send(serveType + 1);
            //ss.Send(1);
            //ss.Send(2);
            //ss.Send(3);
		}
	}
}
