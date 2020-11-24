using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    
    Animator myAnim;       

	
	void Start () {       
        myAnim = GetComponent<Animator>();           

    }		

	public float currentTime;
	public float targetTime;
	public string targetParameter;
	public bool startTimer;

	void Update () {


//        myAnim.SetBool("Hitleft", false);
//        myAnim.SetBool("HitRight", false);
//        myAnim.SetBool("HitHighLeft", false);
//        myAnim.SetBool("HitHighRight", false);

		/*
        var mousePos = Input.mousePosition;
        var sWidth = Screen.width;
        var sHeight = Screen.height;        
        

        if ((int)mousePos.x < sWidth / 2 && (int)mousePos.x > 0 && (int)mousePos.y < sHeight / 2 && (int)mousePos.y > 0)
        {
            myAnim.SetBool("HitRight", true);

        }

        if ((int)mousePos.x > sWidth / 2 && (int)mousePos.x < sWidth && (int)mousePos.y < sHeight / 2 && (int)mousePos.y > 0)
        {
            myAnim.SetBool("Hitleft", true);
        }
        if ((int)mousePos.x < sWidth / 2 && (int)mousePos.x > 0 && (int)mousePos.y < sHeight && (int)mousePos.y > sHeight / 2)
        {
            myAnim.SetBool("HitHighRight", true);

        }
        if ((int)mousePos.x > sWidth / 2 && (int)mousePos.x < sWidth && (int)mousePos.y < sHeight && (int)mousePos.y > sHeight / 2)
        {
            myAnim.SetBool("HitHighLeft", true);
        }   
        */



		if(startTimer) {
			
			currentTime += Time.deltaTime;

			if(currentTime >= targetTime) {
				
				myAnim.SetBool(targetParameter, false);
				startTimer = false;

			}

		} else {

			if(Input.GetKeyDown(KeyCode.A)) {
				SetAnimation(0.6f,"HitRight");
			}
			if(Input.GetKeyDown(KeyCode.B)) {
				SetAnimation(0.6f,"HitLeft");
			}
			if(Input.GetKeyDown(KeyCode.C)) {
				SetAnimation(0.6f,"HitHighRight");
			}
			if(Input.GetKeyDown(KeyCode.D)) {
				SetAnimation(0.6f,"HitHighLeft");
			}

		}

    }

	void SetAnimation (float tarTIme, string parameter) {
		startTimer = true;
		currentTime = 0;
		targetTime = tarTIme;
		targetParameter = parameter;
		myAnim.SetBool(parameter, true);
	}

}


