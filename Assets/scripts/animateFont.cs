using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class animateFont : MonoBehaviour
{
    TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Appear());
        tmp.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(2);

        tmp.enabled = true;

        while (tmp.alpha < 1)
        {
            tmp.alpha += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
