using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class Robocore : MonoBehaviour
{
    public bool SelfDestruct;
    public bool Exploding;
    public SpriteRenderer BoomRenderer;
    public SpriteRenderer MyRenderer;
    public Tilemap tilemap;
    public shakerobo shakerobo;
    public SpriteRenderer[] explosions;
    public PixelPerfectCamera ppCam;
    public scroll bgscroll;

    // Start is called before the first frame update
    void Start()
    {
        MyRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SelfDestruct == true && Exploding == false)
        {
            Exploding = true;
            MyRenderer.enabled = false;
            BoomRenderer.enabled = true;
            shakerobo.gameObject.transform.position = Vector3.zero;
            shakerobo.disableShake = true;
            bgscroll.Brake();

            ppCam.enabled = false;
            Camera.main.orthographicSize = 40;

            foreach (var item in explosions)
            {
                item.enabled = true;
            }

            GetComponent<BoxCollider2D>().enabled = false;

            StartCoroutine(Destroy());

            GetComponent<AudioSource>().Play();
            //GetComponent<AudioSource>().clip = explosionClip;
        }
    }

    private IEnumerator Destroy()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        float pausa = 1;

        for (int n = tilemap.cellBounds.xMin; n < tilemap.cellBounds.xMax; n++)
        {
            for (int p = tilemap.cellBounds.yMin; p < tilemap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tilemap.transform.position.y));
                Vector3 place = tilemap.CellToWorld(localPlace);
                if (tilemap.HasTile(localPlace))
                {
                    //Tile at "place"
                    //Debug.DrawLine(FindObjectOfType<character>().gameObject.transform.position, place, Color.cyan, (n + p) * 10);
                    BoomRenderer.gameObject.transform.position = place;
                    BoomRenderer.GetComponent<AudioSource>().Play();
                    yield return new WaitForSeconds(pausa);

                    tilemap.SetTile(localPlace, null);

                    //Debug.Log(pausa);
                    pausa -= 0.05f;

                    if (pausa < 0.05f)
                    {
                        pausa = 0.05f;
                    }
                }
                else
                {
                    //No tile at "place"
                }


            }
        }
    }
}
