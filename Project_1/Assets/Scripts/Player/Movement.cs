using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float jumpForce = 400;
    public float speed = 3;
    public float minHeight, maxHeight;

    private Rigidbody rb;
    private Animator anim;
    private Transform GroundCheck;
    private bool onGround;
    private bool isDead = false;
    private SpriteRenderer renderer;
    private bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        GroundCheck = gameObject.transform.Find("GroundCheck");
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if(Input.GetButtonDown("Jump") && onGround)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            if (!onGround)
            {
                v = 0;
            }
            if (h != 0)
            {
                renderer.flipX = h < 0;
            }
            rb.velocity = new Vector3(h * speed, rb.velocity.y, v * speed);
            
            if (jump)
            {
                jump = false;
                rb.AddForce(Vector3.up * jumpForce);
            }
            if (onGround)
            {
                anim.SetFloat("walk", Mathf.Abs(h));
                if (Input.GetKey(KeyCode.K))
                {
                    anim.SetTrigger("punch");
                }
                if (Input.GetKey(KeyCode.J))
                {
                    anim.SetTrigger("kick1");
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    anim.SetTrigger("jump");
                }

                float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
                float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;
                rb.position = new Vector3(Mathf.Clamp(rb.position.x, minWidth + 1, maxWidth - 1), rb.position.y, Mathf.Clamp(rb.position.z, minHeight, maxHeight));
            }

        }

    }
}
