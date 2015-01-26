using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

    public float move;
    public float maxSpeed = 10f;
    public bool turnedLeft;
    public float jumpForce = 700f;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public static int starsCollected = 0;
    public static int deaths = 0;
    public int maxDeaths = 3;

    private bool grounded;
    private Vector3 startPos;
    private bool hitWall;

    void FixedUpdate()
    {
        
        move = Input.GetAxis("Horizontal");
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
       
    }

	// Use this for initialization
	void Start () 
    {
        startPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (!hitWall)
            rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

        if (!turnedLeft && move < 0)
            Flip();
        else if (turnedLeft && move > 0)
            Flip();

        if (grounded && Input.GetKeyDown(KeyCode.UpArrow) && !hitWall)
            Jump();
        if (Input.GetKeyDown(KeyCode.F2))
            RestartLevel();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Saw" || col.gameObject.name == "DeadEnd")
        {
            if (deaths < maxDeaths)
            {
                transform.position = startPos;
                deaths++;
            }
            else
            {
                RestartLevel();
            }
        }
        if (col.gameObject.tag == "Wall" && !grounded)
        {
            hitWall = true;
        }

        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Star")
        {
            Destroy(col.gameObject);
            starsCollected++;
        }

        if (col.gameObject.tag == "LevelEnd")
            Application.LoadLevel(Application.loadedLevel + 1);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall" && !grounded)
        {
            hitWall = true;
        }
        else if (grounded)
            hitWall = false;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
            hitWall = false;
    }

    private void Flip()
    {
        turnedLeft = !turnedLeft;
        rigidbody2D.transform.localScale = new Vector3(rigidbody2D.transform.localScale.x * -1,
            rigidbody2D.transform.localScale.y, rigidbody2D.transform.localScale.z);
    }

    private void Jump()
    {
        rigidbody2D.AddForce(new Vector2(0f, jumpForce));
    }

    private void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
        deaths = 0;
        starsCollected = 0;
    }
}
