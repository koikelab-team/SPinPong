using UnityEngine;

public class BallTrackClient : MonoBehaviour
{
    private HelloRequester _helloRequester;

    private void Start()
    {
        _helloRequester = new HelloRequester();
        _helloRequester.callback += onMessage;
        _helloRequester.Start();
    }

    private void onMessage(string message) {
        //Debug.Log("Received " + message);
        if (message.Length > 2) {
            string msg = message.Substring(1, message.Length - 2);
            string[] arr = msg.Split(',');
            Vector3 res = new Vector3(
                //float.Parse(arr[0]),
                //-float.Parse(arr[1]),
                //float.Parse(arr[2])
                //float.Parse(arr[0]),
                //-float.Parse(arr[2]),
                //-float.Parse(arr[1])
                float.Parse(arr[0]),
                -float.Parse(arr[4]),
                -float.Parse(arr[2])
            );
            Vector3 vel = new Vector3(
                float.Parse(arr[1]),
                -float.Parse(arr[5]),
                -float.Parse(arr[3])
            );
            UnityMainThreadDispatcher.Instance().Enqueue(() => {
                //Debug.Log("parsed: " + res.ToString("F4"));
                this.transform.position = res;
                this.GetComponent<Rigidbody>().velocity = vel;
            });
        }
    }
    private void OnDestroy()
    {
        _helloRequester.Stop();
    }
}