#pragma warning disable 0108

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GhostPlacer : MonoBehaviour {

    public GameObject ghostRacket;
    public GameObject ghostBall;
    public GameObject ghostArrow;

    internal bool isActive = true;

    Rigidbody rigidbody;
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
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
        ghostRacket.SetActive(true);
        ghostBall.SetActive(true);
        ghostArrow.SetActive(true);
        Logger.Instance.write("GHOST enable");
    }
    public void disable() {
        isActive = false;
        ghostRacket.SetActive(false);
        ghostBall.SetActive(false);
        ghostArrow.SetActive(false);
        Logger.Instance.write("GHOST disable");
    }

    Vector3 lastVelocity;
    void FixedUpdate() {
        lastVelocity = rigidbody.velocity;
    }

	void OnCollisionExit(Collision collisionInfo) {
		if (collisionInfo.collider.tag == "racket") {
            ghostArrow.transform.rotation = Quaternion.LookRotation(rigidbody.velocity, Vector3.up);
		}
	}
	void OnCollisionEnter(Collision collisionInfo) {
		if (collisionInfo.collider.tag == "racket") {
            ghostRacket.transform.position = collisionInfo.gameObject.transform.position;
            ghostRacket.transform.rotation = collisionInfo.gameObject.transform.rotation;

            //ghostBall.transform.position = this.transform.position; // this is too ambiguous
            ghostBall.transform.rotation = this.transform.rotation;

			Vector3 collisionPoint = collisionInfo.contacts[0].point;
			Vector3 racketNormal = collisionInfo.contacts[0].normal;

            ghostBall.transform.position = collisionPoint + racketNormal * ghostBall.transform.localScale[0] /2;
            //ghostArrow.transform.rotation = Quaternion.LookRotation(rigidbody.velocity, Vector3.up);
            ghostArrow.transform.position = collisionPoint;
            //ghostArrow.transform.rotation = Quaternion.LookRotation(racketNormal, Vector3.up); // this is direction of racket
            //ghostArrow.transform.rotation = Quaternion.LookRotation(-lastVelocity, Vector3.up); // this is direction of ball before collision

            //float angle = Vector3.SignedAngle(-lastVelocity, racketNormal, Vector3.up);
            //float angle = Vector3.SignedAngle(-new Vector3(lastVelocity.x, 0, lastVelocity.z),
            //                                   new Vector3(racketNormal.x, 0, racketNormal.z), Vector3.up);
            Logger.Instance.write("RACKET         position " + collisionInfo.transform.position.ToString("F3"));
            Logger.Instance.write("RACKET         rotation " + collisionInfo.transform.rotation.ToString("F3"));
            Logger.Instance.write("RACKET         velocity " + collisionInfo.gameObject.GetComponent<VelocityManipulator>().lastAppliedVelocity.ToString("F3"));
            Logger.Instance.write("RACKET angular velocity " + collisionInfo.gameObject.GetComponent<VelocityManipulator>().lastAppliedAngularVelocity.ToString("F3"));
            float angle = Vector3.SignedAngle(Vector3.forward, new Vector3(racketNormal.x, 0, racketNormal.z), Vector3.up);

            //Logger.Instance.write("GHOST (" + (isActive?"active":"inactive") + ") ");
            Logger.Instance.write("RACKET angle " + angle);
            print(angle);
            //GameObject.Find("DebugText").GetComponent<Text>().text = "" + angle;

            // Beautify collision point by casting ray
            Vector3 point = collisionInfo.contacts[0].point;
            Vector3 dir = -collisionInfo.contacts[0].normal;
            point -= dir;
            RaycastHit hitInfo;
            if(collisionInfo.collider.Raycast(new Ray(point, dir), out hitInfo, 2)) {
                // collisionInfo.contacts[0].normal and hitInfo.normal seem to be the same!!!
                //Vector3 normal = hitInfo.normal; // collider surface normal
                //ghostArrow.transform.rotation = Quaternion.LookRotation(normal, Vector3.up); // this is direction of racket
                ghostArrow.transform.position = hitInfo.point;
                ghostBall.transform.position = hitInfo.point + racketNormal * ghostBall.transform.localScale[0] /2;
            }
		}
	}
}
