using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject FollowTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var vec = Vector3.Lerp(transform.position, FollowTarget.transform.position, Time.deltaTime);
        vec.z = -10;
        transform.position = vec;

    }
}
