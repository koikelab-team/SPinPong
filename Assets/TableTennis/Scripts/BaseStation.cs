using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Linq;
using Valve.VR;

public class BaseStation : MonoBehaviour
{
    public int ID;
    private XRNodeState trackedNode;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start() {
        initialPosition = transform.localPosition;
        Init();
    }
    void Init() {
        var nodeStates = new List<XRNodeState>();
        InputTracking.GetNodeStates(nodeStates);
        int i = 0;
        foreach (var trackedNode in nodeStates.Where(n => n.nodeType == XRNode.TrackingReference)) {
            bool hasPos = trackedNode.TryGetPosition(out var position);
            bool hasRot = trackedNode.TryGetRotation(out var rotation);
            if (hasPos && hasRot && i++ == ID) {
                this.trackedNode = trackedNode;
                transform.localPosition = position;
                transform.localRotation = rotation;
                print(i + ": " + position + " -> " + transform.position);
                if (initialPosition == position)
                    Invoke("Init", 2.0f);
            }
        }
    }

    // Update is called once per frame
    void Update() {
    }
}
