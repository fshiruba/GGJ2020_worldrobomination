using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.ParticleSystem;

public class Bullet2 : MonoBehaviour
{
    public Tilemap tilemap;

    AudioSource myaudio;
    public SpriteRenderer MyRenderer;
    public SpriteRenderer ExplosionRenderer;
    public Rigidbody2D rbody;
    public ParticleSystem particle;
    public EmissionModule paremiss;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        ExplosionRenderer.enabled = false;
        MyRenderer = GetComponent<SpriteRenderer>();
        myaudio = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody2D>();

        paremiss = particle.emission;
        paremiss.enabled = false;
        particle.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -150)
        {
            Destroy(this.gameObject);
        }

        //if (CanBeCollected == false && exploding == false)
        //{
        //    MyRenderer.enabled = true;
        //    ExplosionRenderer.enabled = false;
        //    deathFrames = 0;
        //}

        //if (exploding)
        //{
        //    transform.position = savepos;
        //    deathFrames++;

        //    if (deathFrames > 100)
        //    {
        //        CanBeCollected = true;
        //        this.transform.position = new Vector3(0, -200, 0);
        //        savepos = transform.position;
        //    }
        //}
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("Bonk : " + col.collider.gameObject.name);
        paremiss.enabled = true;
        particle.Play();

        particle.gameObject.transform.position = col.contacts[0].point;
        particle.gameObject.transform.position += new Vector3(0, 0, -5);

        Vector3 hitPosition = Vector3.zero;

        var hit = col.contacts[0];

        if (hit.collider.gameObject.name.Contains("Bomb"))
        {
            myaudio.Play();
            MyRenderer.enabled = false;
            Destroy(this.gameObject,0.5f);
        }
        else if (hit.collider.gameObject.name == "Player")
        {
            return;
        }
        else if (hit.collider.gameObject.name == "Robocore")
        {
            return;
        }

        hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
        hitPosition.y = hit.point.y - 0.01f * hit.normal.y;

        bool left = false;
        bool right = false;

        var tilepos = tilemap.WorldToCell(hitPosition);

        Tile tile;

        tile = (Tile)tilemap.GetTile(tilepos);

        Debug.DrawLine(this.transform.position, hitPosition, Color.blue, 50);
        Debug.DrawLine(hitPosition, tilepos, Color.cyan, 50);

        var totheleft = tilepos + Vector3Int.left;
        var totheright = tilepos + Vector3Int.right;

        Debug.DrawLine(tilepos, totheleft, Color.red, 5);
        Debug.DrawLine(tilepos, totheright, Color.yellow, 5);

        if (tilemap.HasTile(totheleft))
        {
            //Debug.Log("Tile to the Left");
            left = true;
        }

        if (tilemap.HasTile(totheright))
        {
            //Debug.Log("Tile to the Right");
            right = true;
        }

        bool done = false;

        do
        {
            if (left && !right)
            {
                //Debug.Log("SET RIGHT");
                //right
                tilemap.SetTile(totheright, tile);
                done = true;
            }
            else if (right && !left)
            {
                //Debug.Log("SET LEFT");
                //left
                tilemap.SetTile(totheleft, tile);
                done = true;
            }
            else if (!left && !right)
            {
                bool isleft = Random.value > 0.5f;
                left = isleft;
                right = !isleft;
                //Debug.Log("BOTH : LFT: " + left + " - RGT: " + right);
            }
            else
            {
                //Debug.Log("SET NONE");
                done = true;
            }

        } while (!done);

        myaudio.Play();

        //MyRenderer.enabled = false;
        //ExplosionRenderer.enabled = true;
        MyRenderer.enabled = false;
        Destroy(this.gameObject, 0.5f);

    }
}
