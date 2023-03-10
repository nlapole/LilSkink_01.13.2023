using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 enemyBump = new Vector2(10f, 25f);
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Animator myAnimator;

    bool skinkerAlive = true;
    public AudioClip[] jumpAudio;
    public AudioClip moveAudio;
    public AudioClip landAudio;
    public AudioSource audioSource;
    private bool isGrounded = true;
    public float volumeLand = 1.0f;
    

    

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
 
    }

    void Update()
    {
        if (!skinkerAlive) { return; }
        Run();
        FlipSprite();
        Falling();
        skinkerHit();
        skinkerKO();

//Start of Audio

        if (Input.GetKeyDown("space"))
        {
            audioSource.clip = jumpAudio[Random.Range(0, jumpAudio.Length)];
            audioSource.Play();
        } 
        if (Input.GetKeyDown("d") | Input.GetKeyDown("a") && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            audioSource.clip = moveAudio;
            audioSource.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !isGrounded)
        {
            AudioSource.PlayClipAtPoint(landAudio, transform.position, volumeLand);
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && isGrounded)
        {
            isGrounded = false;
        }
    }

//End of Audio

    void OnMove(InputValue value)
    {
        if (!skinkerAlive) { return; }
        moveInput = value.Get<Vector2>();
       
    }

    void OnJump(InputValue value)
    {
        if (!skinkerAlive) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);

        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }

    }

    void Falling()
    {
        if (
           myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))
        )
        {
            myAnimator.SetBool("isJumping", false);
            myAnimator.SetBool("isLanding", false);
            return;
        }

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        if (myRigidbody.velocity.y >= 0)
        {
            myAnimator.SetBool("isJumping", playerHasVerticalSpeed);
            myAnimator.SetBool("isLanding", false);
        }
        else
        {
            myAnimator.SetBool("isJumping", false);
            myAnimator.SetBool("isLanding", playerHasVerticalSpeed);

        }
    }

    void skinkerHit()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            myRigidbody.velocity = enemyBump;
        }
         
    }

    void skinkerKO()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Wave")))
        {
            skinkerAlive = false;
            myAnimator.SetTrigger("isKO");
            GameOver();
        }
    }
    void GameOver()
    {
        float t = Time.deltaTime / 1f;
        SceneManager.LoadScene("GameOver");
    }

    
}