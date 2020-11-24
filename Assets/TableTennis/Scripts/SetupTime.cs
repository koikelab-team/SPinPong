using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupTime : MonoBehaviour
{
    public static float initialTimeScale;
    public static float initialFixedDeltaTime;
    public static float initialMaximumDeltaTime;

    void Start()
    {
        // default unity settings:
        // Time.timeScale = 1.0f;
        // Time.maximumDeltaTime = 1f / 3f;
        // Time.fixedDeltaTime = 1f / 1000f;

        /* divide Time.fixedDeltaTime by target framerate. valve index can go up to 144hz.
         * make sure interpolation is enabled for the racket as we want to use slow motion, i.e. changing Time.timeScale
         * the alternative is to change Time.fixedDeltaTime in accordance to Time.timeScale,
         * but then sum of small inaccuracies within the physics results in different trajectory of the ball during slow motion.
         */
        Time.fixedDeltaTime = 1f / 200f;
        //Time.fixedDeltaTime = 1f / 144f;
        //Time.fixedDeltaTime = maxSlowMo / 144f;
        //Time.maximumDeltaTime = Time.fixedDeltaTime * 1.25f;
        //Time.fixedDeltaTime = 0.04f;
        print("fixedDeltaTime: " + Time.fixedDeltaTime);

        initialTimeScale = Time.timeScale;
        initialFixedDeltaTime = Time.fixedDeltaTime;
        initialMaximumDeltaTime = Time.maximumDeltaTime;
    }
}
