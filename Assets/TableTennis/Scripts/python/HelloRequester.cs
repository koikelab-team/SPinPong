using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;


/// <summary>
///     Example of requester who only sends Hello. Very nice guy.
///     You can copy this class and modify Run() to suits your needs.
///     To use this class, you just instantiate, call Start() when you want to start and Stop() when you want to stop.
/// </summary>
public class HelloRequester : RunAbleThread
{
    /// <summary>
    ///     Request Hello message to server and receive message back. Do it 10 times.
    ///     Stop requesting when Running=false.
    /// </summary>

    public Action<string> callback;

    protected override void Run()
    {
        ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use, not sure why yet
        using (RequestSocket client = new RequestSocket())
        {
            //client.Connect("tcp://localhost:5555");
            client.Connect("tcp://192.168.1.191:5566");
            while (Running) {
                //Debug.Log("Sending Hello");
                client.SendFrame("Hello");
                string message = null;
                bool gotMessage = false;
                while (Running)
                {
                    gotMessage = client.TryReceiveFrameString(out message); // this returns true if it's successful
                    if (gotMessage) break;
                }

                if (gotMessage) {
                    //Debug.Log("Received " + message);
                    callback(message);
                }
            }
        }

        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use, not sure why yet
    }
}