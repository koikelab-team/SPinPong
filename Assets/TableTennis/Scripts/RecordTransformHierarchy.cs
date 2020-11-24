#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class RecordTransformHierarchy : MonoBehaviour
{

    public AnimationClip clip;
    
    private GameObjectRecorder mRecorder;

    public bool record = false;

    // Start is called before the first frame update
    void Start() {
        mRecorder = new GameObjectRecorder(gameObject);
        mRecorder.BindComponentsOfType<RecordingData>(gameObject, true);
        mRecorder.BindComponentsOfType<Transform>(gameObject, true);
    }
    void LateUpdate() {
        if (clip == null) 
            return;
        if (record) {
            mRecorder.TakeSnapshot(Time.deltaTime);
        } else if (mRecorder.isRecording) {
            mRecorder.SaveToClip(clip);
            mRecorder.ResetRecording();
        }
    }
}
#endif
