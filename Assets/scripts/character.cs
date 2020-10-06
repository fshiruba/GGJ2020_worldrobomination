using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class character : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D mybody;
    private Animator myanim;
    public Transform artTransform;
    public GameObject GoodBullet;

    public float HorizontalSpeed;
    public float MaxVerticalSpeed;
    public float FallSpeed;
    public bool JumpHeld;
    public float boostDown;
    public int fireCooldown;

    [SerializeField]
    private Vector2 CurrentSpeed;

    public float groundRayLength;

    public bool Grounded = true;

    [SerializeField]
    private float jumpForce;

    public static float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        //h = v^2/2g
        //2gh = v^2
        //sqrt(2gh) = v
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }

    void Start()
    {
        mybody = gameObject.GetComponent<Rigidbody2D>();
        myanim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        myanim.ResetTrigger("Fire");

        fireCooldown--;
        jumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, MaxVerticalSpeed);
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, -groundRayLength, 0), Color.red, 0f, false);
        var hitDown = Physics2D.Raycast(transform.position, Vector2.down, groundRayLength);

        if (hitDown)
        {
            Grounded = true;
            boostDown = 0;
            myanim.SetBool("grounded", true);
        }
        else
        {
            Grounded = false;
            myanim.SetBool("grounded", false);
        }

        CurrentSpeed = mybody.velocity;

        if (CurrentSpeed.y > 0)
        {
            //Debug.Log(string.Format("HEIGHT: {0} - SPD: {1}", transform.position.y, CurrentSpeed.y));
        }

        mybody.angularDrag = 0;

        myanim.SetBool("horizontal", false);

        if (Input.GetAxis("Horizontal") > 0)
        {
            mybody.velocity = new Vector2(HorizontalSpeed, CurrentSpeed.y);
            artTransform.localScale = new Vector3(.5f, .5f, 1);
            myanim.SetBool("horizontal", true);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            mybody.velocity = new Vector2(HorizontalSpeed * -1, CurrentSpeed.y);
            artTransform.localScale = new Vector3(-.5f, .5f, 1);
            myanim.SetBool("horizontal", true);
        }
        else
        {
            mybody.velocity = new Vector2(0, CurrentSpeed.y);
            myanim.SetBool("horizontal", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            JumpHeld = true;

            if (Grounded)
            {
                mybody.AddForce(Vector2.up * jumpForce * mybody.mass, ForceMode2D.Impulse);
            }
        }
        else if (Input.GetButtonUp("Jump"))
        {
            JumpHeld = false;
        }
        else if (Input.GetButtonDown("Fire1") && fireCooldown <= 0)
        {
            myanim.SetTrigger("Fire");

            fireCooldown = 100;

            var gBullet = Instantiate(GoodBullet);
            //gBullet.transform.SetParent(FindObjectOfType<Tilemap>().gameObject.transform);

            if (Input.GetAxis("Vertical") > 0)
            {
                gBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
                gBullet.transform.position = transform.position + new Vector3(0, 2, 0);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                gBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
                gBullet.transform.position = transform.position + Vector3.left;
            }
            else
            {
                if (artTransform.localScale.x == 0.5f)
                {
                    gBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
                    gBullet.transform.position = transform.position + Vector3.right;
                }
                else
                {
                    gBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
                    gBullet.transform.position = transform.position + Vector3.left;
                }

            }

        }

    }

    void FixedUpdate()
    {
        //Debug.Log(Vector2.Dot(mybody.velocity, Vector2.up));

        if (!Grounded)
        {
            if (!JumpHeld)
            {
                if (Mathf.Approximately(boostDown, 0))
                {
                    boostDown = FallSpeed;
                }
                else
                {
                    boostDown += FallSpeed * FallSpeed;
                }

                if (boostDown > 0)
                {
                    boostDown *= -1;
                }

                //Debug.Log("DOWN");
                mybody.AddForce(new Vector2(0, FallSpeed + 0) * mybody.mass);
            }
        }
    }
}
