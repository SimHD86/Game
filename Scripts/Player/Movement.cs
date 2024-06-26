
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] public float speed;
    [SerializeField] public float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] public float coyoteTime; //How much time the player can hang in the air before jumping
    public float coyoteCounter; //How much time passed since the player ran off the edge

    [Header("Multiple Jumps")]
    [SerializeField] public int extraJumps;
    public int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX; //Horizontal wall jump force
    [SerializeField] private float wallJumpY; //Vertical wall jump force

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float wallJumpCooldown;
    private float horizontalInput;

    //fire
    public bool fire;
    //wind 
    public bool wind;
    //lighting
    public bool lightning;
    private SpriteRenderer spriteRend;

    //  HOOK
    public bool hook = false;

    
    
    int characterselect;
    private void Awake()
    {
        

        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");


        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0) 
        { 
        body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2); }

        if (fire) 
        {
            spriteRend = GetComponent<SpriteRenderer>();
            spriteRend.color = Color.white;
            anim.enabled = true;
            wind = false;
            lightning = false;
        }

        else if (wind)
        {
            spriteRend = GetComponent<SpriteRenderer>();
            spriteRend.color = Color.cyan;
            anim.enabled = false;
            fire = false;
            lightning = false;

        }

        else if (lightning)
        {
            spriteRend = GetComponent<SpriteRenderer>();
            spriteRend.color = Color.magenta;
            anim.enabled = false;
            wind = false;
            fire = false;

        }
            




        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 3;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; //Reset coyote counter when on the ground
                jumpCounter = extraJumps; //Reset jump counter to extra jump value
            }
            else
                coyoteCounter -= Time.deltaTime; //Start decreasing coyote counter when not on the ground
        }
    }

    private void Jump()
    {
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return;
        //If coyote counter is 0 or less and not on the wall and don't have any extra jumps don't do anything

        //SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
            WallJump();
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                //If not on the ground and coyote counter bigger than 0 do a normal jump
                if (coyoteCounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    if (jumpCounter > 0) //If we have extra jumps then jump and decrease the jump counter
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            //Reset coyote counter to 0 to avoid double jumps
            coyoteCounter = 0;
        }
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    

}
