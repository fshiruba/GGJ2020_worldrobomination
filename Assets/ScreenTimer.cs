using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class ScreenTimer : MonoBehaviour
{
    public Robocore robocore;
    public TextMeshProUGUI Timer;
    public Stopwatch sw;
    bool isAnimating;

    // Start is called before the first frame update
    void Start()
    {
        sw = new Stopwatch();
        sw.Start();
        isAnimating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (robocore.Exploding)
        {
            if (!isAnimating)
            {
                isAnimating = true;
                StartCoroutine(Flash());
            }
        }
        else
        {
            var tspan = sw.Elapsed;

            var val1 = tspan.Minutes.ToString().PadLeft(2, '0');
            var val2 = tspan.Seconds.ToString().PadLeft(2, '0');
            var val3 = tspan.Milliseconds.ToString().PadLeft(4, '0');

            Timer.text = string.Format("{0}:{1}:{2}", val1, val2, val3);
        }

    }

    IEnumerator Flash()
    {
        Timer.fontSize = 60;
        yield return new WaitForSeconds(0.2f);
        Timer.fontSize = 50;
        yield return new WaitForSeconds(0.2f);
        Timer.fontSize = 60;
        yield return new WaitForSeconds(0.2f);
        Timer.fontSize = 50;
    }
}
