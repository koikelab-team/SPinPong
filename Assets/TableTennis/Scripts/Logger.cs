using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Logger : Singleton<Logger>
{
    protected Logger() {
        writer = new StreamWriter(path, true);
        write("Starting Table Tennis log");
    }

    string path = "Assets/TableTennis/Logs/log.txt";
    StreamWriter writer;
    void OnDestroy() {
        write("Goodbye!");
        writer.Close();
    }
    long logCounter = 0;
    public void write(string s) {
        writer.WriteLine("{0:yyy-MM-dd HH:mm:ss.fff} {1,-6} {2}", System.DateTime.Now, logCounter++, s);
    }
    public void writeVector(string s, Vector3 v) {
        this.write(string.Format("{0, -30} {1}", s, v.ToString("F3")));
    }

}
