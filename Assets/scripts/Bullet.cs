using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public Tilemap tilemap;
    public bool CanBeCollected = false;
    AudioSource myaudio;
    public SpriteRenderer MyRenderer;
    public SpriteRenderer ExplosionRenderer;
    public Rigidbody2D rbody;
    public int deathFrames;
    public bool exploding;    

    // Start is called before the first frame update
    void Start()
    {
        ExplosionRenderer.enabled = false;
        MyRenderer = GetComponent<SpriteRenderer>();
        myaudio = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -150)
        {
            CanBeCollected = true;
        }

        if (CanBeCollected == false && exploding == false)
        {
            MyRenderer.enabled = true;
            ExplosionRenderer.enabled = false;
            deathFrames = 0;
        }

        if (exploding)
        {            
            deathFrames++;

            if (deathFrames > 100)
            {
                CanBeCollected = true;
                transform.position = new Vector3(0, -125, 0);                
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (exploding)
        {
            return;
        }
        
        exploding = true;

        Vector3 hitPosition = Vector3.zero;

        var hit = col.contacts[0];

        if (hit.collider.gameObject.name.Contains("Bomb") || hit.collider.gameObject.name.Contains("Good"))
        {
            exploding = false;
            rbody.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            return;
        }
        else if (hit.collider.gameObject.name.Contains("Player"))
        {
            hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(rbody.velocity.x * 2, rbody.velocity.y / 2);
        }
        else if (hit.collider.gameObject.name == "Robocore")
        {
            hit.collider.gameObject.GetComponent<Robocore>().SelfDestruct = true;
        }

        hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
        hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
        tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);

        MyRenderer.enabled = false;
        ExplosionRenderer.enabled = true;

        myaudio.Play();
    }
}
