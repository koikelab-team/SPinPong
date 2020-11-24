using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionSound : MonoBehaviour
{

    public AudioClip racketSound;
    public AudioClip tableSound;
    AudioSource audioSource;

    string lastHit = "";

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    public void Update() {
        audioSource.pitch = Time.timeScale;
    }
    private float getSoundLevel(float f) {
        return Mathf.Sqrt(f / 7f);
    }
    void OnCollisionEnter(Collision collisionInfo) {
		if (!this.enabled)
			return;
		if (collisionInfo.collider.tag == "racket") {
            float lvl = getSoundLevel(collisionInfo.relativeVelocity.magnitude);
            audioSource.PlayOneShot(racketSound, lvl);
            Logger.Instance.writeVector("HIT racket", collisionInfo.contacts[0].point);
        } else if (collisionInfo.collider.name == "UserSideTable" || collisionInfo.collider.name == "AiSideTable") {
            float lvl = getSoundLevel(collisionInfo.relativeVelocity.magnitude);
            audioSource.PlayOneShot(tableSound, lvl);
            if (collisionInfo.collider.name == "AiSideTable")
                Logger.Instance.writeVector("HIT table enemy", collisionInfo.contacts[0].point);
            if (collisionInfo.collider.name == "UserSideTable")
                Logger.Instance.writeVector("HIT table user", collisionInfo.contacts[0].point);
        } else {
            if (lastHit != collisionInfo.collider.name)
                Logger.Instance.writeVector("HIT " + collisionInfo.collider.name, collisionInfo.contacts[0].point);
        }
        lastHit = collisionInfo.collider.name;
    }
}
