using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;

public class AutoClick : MonoBehaviour {
    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    public static IEnumerator InvokeRealtimeCoroutine(System.Action action, float seconds) {
        yield return new WaitForSecondsRealtime(seconds);
        if (action != null)
            action();
    }

    public void Start() {
        EventManager.StartListening("RequestServe", robotSimulServe);
    }

    int serveCount;
    private IntPtr foreground = IntPtr.Zero;
    private int serveType;
    public void robotSimulServe(object arg) {
        serveType = (int) arg;
        IntPtr roboWin = FindWindow(null, "Robo-Pong 3050XL");
        if (roboWin == IntPtr.Zero) {
            Invoke("serve", 0);
            return;
        }
        Rectangle rect = MouseTool.GetWindowRect(roboWin);
        if (rect.Width == 0) {
            Invoke("serve", 0);
            return;
        }
        serveCount = 0;
        foreground = GetForegroundWindow();
        Point p = new Point();
        p.X = (int) (rect.Width * 0.1f);
        p.Y = (int) (rect.Height * 0.76f);
        if (foreground != roboWin)
            MouseTool.SetForegroundWindow(roboWin);
        MouseTool.ClickOnPoint(roboWin, p, true);
        // time from newgy app until serve: about 10.15
        // time from serve until bounce on user side: about 0.4649997
        //Invoke("serve", 10.15f);
        //Invoke("serveMultiple", 10.15f);
        
        StartCoroutine(InvokeRealtimeCoroutine(serveMultiple, firstDelay));
        EventManager.TriggerEvent("ConutdownUntilServe", firstDelay);
        if (foreground != roboWin) {
            //Invoke("robotSimulServePostWork", 0.5f);
            StartCoroutine(InvokeRealtimeCoroutine(robotSimulServePostWork, 0.5f));
        }
    }
    float firstDelay = 10.15f;
    float secondDelay = 3.6f;
    private void robotSimulServePostWork() {
        MouseTool.SetForegroundWindow(foreground);
    }
    private void serveMultiple() {
        serve();
        if (serveCount < 5) {
            //Invoke("serveMultiple", 3.612f); // real/vr 3/3.612
            //StartCoroutine(InvokeRealtimeCoroutine(serveMultiple, 3.612f));
            StartCoroutine(InvokeRealtimeCoroutine(serveMultiple, secondDelay));
            EventManager.TriggerEvent("ConutdownUntilServe", secondDelay);
        }
    }
    private void serve() {
        EventManager.TriggerEvent("Serve", serveType);
        serveCount++;
    }


    public class MouseTool
    {

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs,  int cbSize);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point pos);
        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        #pragma warning disable 649
        internal struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        internal struct INPUT {
            public UInt32 Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }
        internal struct MOUSEINPUT {
            public Int32 X;
            public Int32 Y;
            public UInt32 MouseData;
            public UInt32 Flags;
            public UInt32 Time;
            public IntPtr ExtraInfo;
        }
        #pragma warning restore 649

        public static Point CurPos() {
            Point oldPos = new Point();
            GetCursorPos(out oldPos);
            return oldPos;
        }
        public static void SetCursorTo(IntPtr wndHandle, Point clientPoint) {
            /// get screen coordinates
            ClientToScreen(wndHandle, ref clientPoint);
            SetCursorPos(clientPoint.X, clientPoint.Y);
        }
        public static void Click(IntPtr wndHandle) {
            var inputMouseDown = new INPUT();
            inputMouseDown.Type = 0; /// input type mouse
            inputMouseDown.Data.Mouse.Flags = 0x0002; /// left button down

            var inputMouseUp = new INPUT();
            inputMouseUp.Type = 0; /// input type mouse
            inputMouseUp.Data.Mouse.Flags = 0x0004; /// left button up

            var inputs = new INPUT[] { inputMouseDown, inputMouseUp };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }
        public static void ClickOnPoint(IntPtr wndHandle , Point clientPoint, bool backToStart) {
            Point oldPos = CurPos();
            SetCursorTo(wndHandle, clientPoint);
            Click(wndHandle);
            if (backToStart) // return mouse
                SetCursorPos(oldPos.X, oldPos.Y);
        }
        public static Rectangle GetWindowRect(IntPtr handle) {
            RECT r;
            if (!GetWindowRect(handle, out r)) {
                return new Rectangle();
            }
            Rectangle rect = new Rectangle();
            rect.X = r.Left;
            rect.Y = r.Top;
            rect.Width = r.Right - r.Left;
            rect.Height = r.Bottom - r.Top;
            return rect;
        }
    }
}