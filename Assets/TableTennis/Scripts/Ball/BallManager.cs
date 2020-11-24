using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {
    public bool simpleBall;

    void Start() {
        EventManager.StartListening("Serve", serveBall);
    }

    public void throwBall(float factor) {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.MovePosition(new Vector3(0, 1.015f, 1.42f));
        rigidbody.velocity = new Vector3(0.0f, 1f * factor, 0f);
        rigidbody.angularVelocity = new Vector3(0, 0, 0);
    }

    int serveType;
    public void serveBall(object arg) {
        colcount = 0;

        serveType = (int) arg;
        //ball.GetComponent<AutoTimeChanger>().onBallServe();
        Logger.Instance.write("Serve ball.");
        Debug.Log("Serving ball.");
	    	Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (simpleBall) {
            rigidbody.MovePosition(new Vector3(0, 1.2f, 1.3f));
            rigidbody.velocity = new Vector3(0, -1f, -5.5f);
            rigidbody.angularVelocity = new Vector3(0, 0, 0);
            return;
        }

        rigidbody.MovePosition(new Vector3(0, 0.94f, 1.42f));
        //rigidbody.velocity = new Vector3(0, -1.4f, -4.5f);
        //rigidbody.angularVelocity = new Vector3(0, 5000, 0) / 60 * 2 * Mathf.PI;
        //rigidbody.angularVelocity = -NextBallRPM / 60 * 2 * Mathf.PI * 2;


        rigidbody.velocity = new Vector3(0, -1.6f, -7.5f);
        rigidbody.angularVelocity = -new Vector3(0, -8000, 0) / 60 * 2 * Mathf.PI * 2;
        if (serveType == 1)
            rigidbody.angularVelocity = -new Vector3(0, 8000, 0) / 60 * 2 * Mathf.PI * 2;



        rigidbody.MovePosition(new Vector3(0, 1.015f, 1.42f));
        //rigidbody.velocity = new Vector3(-0.05f, -1.79f, -6.9f);
        rigidbody.velocity = new Vector3(-0.0f, -1.79f, -8.0f);
        rigidbody.angularVelocity = -new Vector3(-0, -8000, 0) / 60 * 2 * Mathf.PI * 2;
        if (serveType == 1) {
            //rigidbody.velocity = new Vector3(-0.0f, -1.79f, -6.9f);
            rigidbody.velocity = new Vector3(-0.0f, -1.79f, -9.0f);
            rigidbody.angularVelocity = -new Vector3(0, 8000, 0) / 60 * 2 * Mathf.PI * 2;
        } else if (serveType == 2) {
            //rigidbody.velocity = new Vector3(-0.0f, -1.79f, -6.9f);
            //rigidbody.angularVelocity = -new Vector3(-8000, 0, 0) / 60 * 2 * Mathf.PI * 2;
            rigidbody.velocity = new Vector3(-0.25f, -1.83f, -7.6f);
            rigidbody.angularVelocity = -new Vector3(-8000, 0, 0) / 60 * 2 * Mathf.PI * 2;
            rigidbody.MovePosition(new Vector3(-0.02f, 1.015f, 1.55f));
            rigidbody.velocity = new Vector3(-0.20f, -1.83f, -7.6f);
            rigidbody.angularVelocity = -new Vector3(-8000, 0, 0) / 60 * 2 * Mathf.PI * 2;
        }

        /*
        //addVelocityRandomness(0.05f);
        //addAngularVelocityRandomness(0.02f);
        //addFlatHorizontalRandomness(0.3f);
        addVelocityRandomness(0.03f);
        addAngularVelocityRandomness(0.01f);
        addFlatHorizontalRandomness(0.1f);
        //*/
    }
    int colcount;
    void OnCollisionEnter(Collision collisionInfo) {
        if (!this.enabled)
            return;
        if (colcount >= 2) {
            return;
        }
        print("ball rot: " + GetComponent<Rigidbody>().angularVelocity);
        colcount++;
        if (collisionInfo.collider.name == "AiSideTable") {
            if (serveType == 0) {
                GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, 0.8f));
            } else if (serveType == 1) {
                GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, 1.2f));
            } else if (serveType == 2) {
                GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, -0.0f));
            }
        } else if (collisionInfo.collider.name == "UserSideTable") {
            if (serveType == 0) {
                GetComponent<Rigidbody>().AddForce(new Vector3(0.4f, 0f, 0f));
            } else if (serveType == 1) {
                GetComponent<Rigidbody>().AddForce(new Vector3(-0.4f, 0f, 0f));
            } else if (serveType == 2) {
                GetComponent<Rigidbody>().AddForce(new Vector3(0.05f, 0f, -0.8f));
            }
        }
    }
    private void addFlatHorizontalRandomness(float p) {
		Rigidbody rigidbody = GetComponent<Rigidbody>();
        // randomize velocity and angularvelocity within percentage p of their value set.
        rigidbody.velocity = new Vector3(
            rigidbody.velocity.x + (Random.value * 2f - 1f) * p,
            rigidbody.velocity.y,
            rigidbody.velocity.z
        );
    }
    private void addVelocityRandomness(float p) {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        // randomize velocity and angularvelocity within percentage p of their value set.
        rigidbody.velocity = new Vector3(
            rigidbody.velocity.x,
            //rigidbody.velocity.x * (1f + (Random.value * 2f - 1f) * p),
            rigidbody.velocity.y * (1f + (Random.value * 2f - 1f) * p),
            rigidbody.velocity.z * (1f + (Random.value * 2f - 1f) * p)
        );
    }
    private void addAngularVelocityRandomness(float p) {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        // randomize velocity and angularvelocity within percentage p of their value set.
        rigidbody.angularVelocity = new Vector3(
            rigidbody.angularVelocity.x * (1f + (Random.value * 2f - 1f) * p),
            rigidbody.angularVelocity.y * (1f + (Random.value * 2f - 1f) * p),
            rigidbody.angularVelocity.z * (1f + (Random.value * 2f - 1f) * p)
        );
    }

}
