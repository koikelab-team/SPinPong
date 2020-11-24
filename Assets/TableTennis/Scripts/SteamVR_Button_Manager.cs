using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamVR_Button_Manager : MonoBehaviour {
    public SteamVR_Input_Sources handType;

    public SteamVR_Action_Vibration hapticOut;
    public SteamVR_Action_Boolean ServeBoolean;
    public SteamVR_Action_Boolean SlowmoBoolean;
    public SteamVR_Action_Boolean UpBoolean;
    public SteamVR_Action_Boolean DownBoolean;
    public SteamVR_Action_Boolean LeftBoolean;
    public SteamVR_Action_Boolean RightBoolean;
    public SteamVR_Action_Boolean GhostBoolean;

    private InputManager inputManager;

    void Start() {        
        inputManager = GameObject.Find("/Global").GetComponent<InputManager>();
        ServeBoolean.AddOnStateUpListener(TriggerUp, handType);

        SlowmoBoolean.AddOnStateDownListener(TriggerDown, handType);
        UpBoolean.AddOnStateDownListener(TriggerDown, handType);
        DownBoolean.AddOnStateDownListener(TriggerDown, handType);
        LeftBoolean.AddOnStateDownListener(TriggerDown, handType);
        RightBoolean.AddOnStateDownListener(TriggerDown, handType);

        GhostBoolean.AddOnStateDownListener(TriggerDown, handType);
    }

    public void vibrate(float power) {
        if (power > 1f)
            power = 1f;
        //hapticOut.Execute(0f, 0.05f, 160, 0.2f, handType);
        print("power: " + power);
        hapticOut.Execute(0.02f + 0.15f*power, 0.1f, 160, 0.3f*power, handType);
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        if (fromAction == SlowmoBoolean)
            inputManager.secondaryButton();
        else if (fromAction == UpBoolean)
            inputManager.changeTimeScale(1);
            //inputManager.setNextBall(0, 1);
        else if (fromAction == DownBoolean)
            inputManager.changeTimeScale(-1);
            //inputManager.setNextBall(0, -1);
        //else if (fromAction == LeftBoolean)
        //    inputManager.setNextBall(1, 0);
        //else if (fromAction == RightBoolean)
        //    inputManager.setNextBall(-1, 0);
        else if (fromAction == GhostBoolean)
            inputManager.tertiaryButton();
    }
    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        if (fromAction == ServeBoolean) {
            inputManager.mainButton();
        }
    }
}
