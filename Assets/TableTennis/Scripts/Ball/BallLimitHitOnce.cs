using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLimitHitOnce : MonoBehaviour
{
	Collider racketCollider = null;
	void Start () {
	}
	void OnCollisionExit(Collision collisionInfo) {
		// in slow-mo, racket can continue to be faster than the ball. make sure we dont hit the ball twice.
		if (racketCollider == null && collisionInfo.collider.name == "racket_model") {
			racketCollider = collisionInfo.collider;
			if (this.enabled)
				Physics.IgnoreCollision(GetComponent<Collider>(), racketCollider, true);
		} else if (racketCollider != null && collisionInfo.collider.name != "racket_model") {
			Physics.IgnoreCollision(GetComponent<Collider>(), racketCollider, false);
			racketCollider = null;
		}
	}
}
