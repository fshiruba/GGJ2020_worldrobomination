using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameoverCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var hit = col.contacts;

        foreach (var item in hit)
        {
            if (item.collider.gameObject.name.Contains("Bomb"))
            {
                var cpnt = item.collider.gameObject.GetComponent<Bullet>();

                if (cpnt != null)
                {
                    cpnt.exploding = true;
                    cpnt.CanBeCollected = true;
                }
                else
                {
                    var cpnt2 = item.collider.gameObject.GetComponent<BulletHoming>();
                    cpnt2.exploding = true;
                    cpnt2.CanBeCollected = true;
                }
            }
            else if (item.collider.gameObject.name == "Player")
            {
                SceneManager.LoadScene(2);
            }
            else if (item.collider.gameObject.name == "Robocore")
            {
                var cpnt = item.collider.gameObject.GetComponent<Robocore>();
                cpnt.SelfDestruct = true;
            }
        }


    }
}
