using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleflicker1 : MonoBehaviour
{
    public int timeuntilNextBoom;
    SpriteRenderer sp;
    Light l;
    AudioSource myaudio;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.enabled = false;
        l = GetComponent<Light>();
        l.enabled = false;
        myaudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (l.enabled)
        {
            l.intensity = Random.Range(0.01f, 0.5f);
        }

        timeuntilNextBoom--;

        if (timeuntilNextBoom <= 0)
        {
            timeuntilNextBoom += Random.Range(300, 600);
            sp.enabled = true;
            l.enabled = true;
            myaudio.Play();

            StartCoroutine(StopStuff());
        }
    }

    IEnumerator StopStuff()
    {
        yield return new WaitForSeconds(1.5f);
        sp.enabled = false;
        l.enabled = false;
    }
}
