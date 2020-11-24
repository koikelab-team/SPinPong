using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingData : MonoBehaviour
{

    public bool requestServe = false;

    // Start is called before the first frame update
    void Start() {
        reset();
    }
    public void reset() {
        requestServe = false;
    }
}
