using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScript : MonoBehaviour
{
    SerialScript ss;

    // Start is called before the first frame update
    void Start()
    {
        ss = this.GetComponent<SerialScript>();//シリアル通信(マイコンとの通信)用スクリプト読み込み
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))//テンキー1, spin1の振動を駆動
        {
            ss.Send(1);
        }
        if (Input.GetKeyUp(KeyCode.Keypad2))//テンキー2, spin2の振動を駆動
        {
            ss.Send(2);
        }
        if (Input.GetKeyUp(KeyCode.Keypad3))//テンキー3, spin3の振動を駆動
        {
            ss.Send(3);
        }
    }
}
