using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialVelocity : MonoBehaviour
{
	public Vector3 initialVelocity;
	public Vector3 initialRotation;

    void Start()
    {
        /*
		parameters to consider:
		- dimensions of table / ball / bat
		- surface / friction of table / ball / bat
		- bounciness of ball
		- (initial/max) velocity of ball
		- weight of table / ball / bat - done?
		*/

		// 8000 rpm / 60 * 2 pi

		//rb.angularVelocity = new Vector3(10000.0f, 0, 0);
		//rb.angularVelocity = new Vector3(0, 10000.0f, 0);
		//rb.angularVelocity = new Vector3(0, 0, -10000.0f);
		//rb.angularVelocity = new Vector3(0, 0, -837f);
		GetComponent<Rigidbody>().angularVelocity = initialRotation / 60 * 2 * Mathf.PI;
		GetComponent<Rigidbody>().velocity = initialVelocity;
    }
}
