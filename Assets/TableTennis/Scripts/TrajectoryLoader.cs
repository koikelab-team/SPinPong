using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrajectoryLoader : MonoBehaviour
{
    public GameObject TrajectoryElement;
    public TextAsset trajectory1;
    public TextAsset trajectory2;
    public TextAsset trajectory3;

    public Boolean fix;

    private List<GameObject> instantiated = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        EventManager.StartListening("Serve", onServe);
    }
    void onServe(object arg) {
        if (!this.enabled) {
            return;
        }
        int serveType = (int) arg;
        removeTrajectory();
        Invoke("removeTrajectory", 1.0f);
        generate(serveType);
    }
    void generate(int id) {
        if (instantiated.Count > 0) {
            return;
        }
        TextAsset text = trajectory1;
        if (id == 0) {
            text = trajectory1;
        } else if (id == 1) {
            text = trajectory2;
        } else if (id == 2) {
            text = trajectory3;
        }
        //string path = Application.dataPath + "/" + "TableTennis/3DTrajectoryPts.txt";
        //TextAsset text = Resources.Load(path) as TextAsset;
        string[] data = text.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        foreach (string s in data) {
            string[] col = s.Split(new[] { " ", "\t" }, StringSplitOptions.None);
            float x = float.Parse(col[0]);
            float y = float.Parse(col[1]);
            float z = float.Parse(col[2]) + 0.02f;
            //Vector3 vec = new Vector3(x, y, z);
            Vector3 vec = new Vector3(x, z, -y);
            if (fix) {
                vec = new Vector3(x, z, -y);
                //Quaternion rotation = Quaternion.Euler(1f, 0, 10); // fix rotation
                Quaternion rotation = Quaternion.Euler(0, 0, 0); // fix rotation
                Matrix4x4 m = Matrix4x4.Rotate(rotation);
                vec = m.MultiplyPoint3x4(vec);
                vec = vec + new Vector3(0.17f, 0.04f, 0); // fix translation
            }
            GameObject duplicate = Instantiate(TrajectoryElement);
            duplicate.name = "TracjectoryElement";
            duplicate.transform.position = vec;
            instantiated.Add(duplicate);
        }
    }
    //void OnEnable() {
    //    generate();
    //}
    void removeTrajectory() {
        List<GameObject> temporary = new List<GameObject>(instantiated);
        foreach (GameObject go in temporary) {
            Destroy(go);
            instantiated.Remove(go);
        }
    }
    void OnDisable() {
        //Debug.Log(instantiated.);
        removeTrajectory();
    }
}
