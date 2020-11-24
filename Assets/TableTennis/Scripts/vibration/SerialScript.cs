using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO.Ports;
using System.Threading;
using System;

public class SerialScript : MonoBehaviour
{

    //エディターでパラメータは操作可能
    public string portName = "COM4";
    public int baudRate = 921600;

    private SerialPort serial;
    public bool isRunning = false;

    public bool run = false;

    // Start is called before the first frame update
    void Start()
    {
        Open();
    }
    public void Open()//シリアルポート開放
    {
        serial = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        serial.Open();

        if (serial.IsOpen)
        {
            isRunning = true;
            Debug.Log("Open!");

        }

    }

    // Update is called once per frame
    void Update() {
        if (run) {
            run = false;
            this.Send(1);
        }
    }
    public void Send(int command)
    {
        if (serial.IsOpen)
        {
            serial.Write(command.ToString());//Command List: 1=spin1, 2=spin2, 3=spin3
        }
    }

    private void OnDestroy()
    {
        Close();
    }
    private void Close()
    {
        isRunning = false;

        if (serial != null && serial.IsOpen)
        {
            serial.Close();
            serial.Dispose();
        }
    }
}
