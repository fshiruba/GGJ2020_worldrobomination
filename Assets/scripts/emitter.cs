using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class emitter : MonoBehaviour
{
    public int currentFrametimer;
    public int frameTimer;
    public GameObject bombTemplate;
    public int LEVEL = 1;
    public bool SpawnNextDone;
    public bool disabled;
    public float HorRangeMin;
    public float HorRangeMax;
    public float VerRangeMin;
    public float VerRangeMax;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (disabled)
        {
            return;
        }

        currentFrametimer--;

        if (currentFrametimer < 0)
        {
            frameTimer -= 10;

            if (frameTimer < 100)
            {
                if (!SpawnNextDone)
                {
                    SpawnNextDone = true;
                    var emitters = FindObjectsOfType<emitter>();

                    var first = emitters.FirstOrDefault(a => a.disabled && a.LEVEL >= LEVEL);

                    if (first != null)
                    {
                        first.disabled = false;
                    }
                }

                frameTimer = 100;
            }
            currentFrametimer = frameTimer;

            SpawnBomb();
        }
    }

    private void SpawnBomb()
    {
        GameObject bomb = null;

        Bullet[] bombas = FindObjectsOfType<Bullet>();
        BulletHoming[] misseis = FindObjectsOfType<BulletHoming>();

        bool isMissel = false;

        if (LEVEL == 1)
        {
            foreach (var item in bombas)
            {
                if (item.GetComponent<Bullet>() != null && item.GetComponent<Bullet>().CanBeCollected)
                {
                    bomb = item.gameObject;
                }
            }
        }
        else
        {
            isMissel = true;

            foreach (var item in misseis)
            {
                if (item.GetComponent<BulletHoming>() != null && item.GetComponent<BulletHoming>().CanBeCollected)
                {
                    bomb = item.gameObject;
                }
            }
        }

        if (bomb == null)
        {
            var go = Instantiate(bombTemplate);

            if (!isMissel)
            {
                go.name = "Bomb-" + (bombas.Length + misseis.Length + 1);
            }
            else
            {
                go.name = "Bomb-" + (bombas.Length + misseis.Length + 1) + "-M";
            }


            go.transform.position = transform.position;
        }
        else
        {
            if (!isMissel)
            {
                Bullet comp = null;

                try
                {
                    comp = bomb.GetComponent<Bullet>();
                }
                catch (Exception)
                {
                    Debug.Log(bomb.name);
                }

                comp.exploding = false;
                comp.CanBeCollected = false;
                bomb.transform.position = transform.position;
                comp.rbody.velocity = new Vector2(UnityEngine.Random.Range(HorRangeMin, HorRangeMax), UnityEngine.Random.Range(VerRangeMin, VerRangeMax));
            }
            else
            {
                BulletHoming comp = null;

                try
                {
                    comp = bomb.GetComponent<BulletHoming>();
                }
                catch (Exception)
                {
                    Debug.Log(bomb.name);
                }

                comp.exploding = false;
                comp.CanBeCollected = false;
                bomb.transform.position = transform.position;
                comp.rbody.velocity = new Vector2(UnityEngine.Random.Range(HorRangeMin, HorRangeMax), UnityEngine.Random.Range(VerRangeMin, VerRangeMax));
            }


        }
    }
}
