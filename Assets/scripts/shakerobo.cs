using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakerobo : MonoBehaviour
{
    public float speed = 1.0f; //how fast it shakes
    public float amount = 1.0f; //how much it shakes
    public bool audioSwitch;
    private AudioSource myaudio;
    public bool disableShake;
    
    // Start is called before the first frame update
    void Start()
    {
        myaudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (disableShake)
        {
            return;
        }

        var val = Mathf.Sin(Time.time * speed) * amount;
        var val2 = Mathf.Sin(Time.time * 1) * amount;
        this.transform.position = new Vector3(val2, val, 0);

        //Debug.Log("VAL: " + val);

        if (val < -0.95f)
        {
            if (!audioSwitch)
            {
                PlaySound();
                audioSwitch = true;
            }
        }

        if (val > 0)
        {
            audioSwitch = false;
        }
    }

    void PlaySound()
    {
        myaudio.Play();
    }
}
