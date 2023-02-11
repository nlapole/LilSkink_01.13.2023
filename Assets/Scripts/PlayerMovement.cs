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

    //public AudioClip jumpAudio;
    public AudioClip[] jumpAudio;
    public AudioClip landAudio;
    public AudioSource audioSource;



    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!skinkerAlive) { return; }
        Run();
        FlipSprite();
        Falling();
        skinkerHit();
        skinkerKO();
    }

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
        
        //Print dialogue test for audio debugging
            //Debug.Log("Kerpow");

        //get audio source component
            audioSource = GetComponent<AudioSource>();

        //randomized audio for standard jump
            audioSource.clip = jumpAudio[Random.Range(0, jumpAudio.Length)];
            audioSource.Play();
        
        //audio that works but is really boring
            //audioSource.PlayOneShot(jumpAudio, 0.7F);
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

        //Old code that doesn't play audio correctly (keep until resolved)    
            //audioSource = GetComponent<AudioSource>();
            //audioSource.PlayOneShot(landAudio, 0.7F);
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