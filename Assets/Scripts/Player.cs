using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    float runTime;
    bool jumpSwitch;

    //State
    bool isAlive = true;

    //Cached
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyColldier;
    BoxCollider2D myFeet;
    // Start is called before the first frame update

    //Message then methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyColldier = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        FlipSprite();
        Die();
    }

    private void Run()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool horizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", horizontalSpeed);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            runTime += Time.deltaTime;
            if (runTime > 1)
            {
                jumpSwitch = true;
            }
            else
            {
                jumpSwitch = false;
            }
        }
        else
        {
            runTime = 0;
        }
    }

    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;
        if (jumpSwitch)
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed * 1.25f);
                myRigidBody.velocity += jumpVelocityToAdd;
            }
        } else { 
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                myRigidBody.velocity += jumpVelocityToAdd;
            }
        }
    }
    private void FlipSprite()
    //if the player is moving horizontally
    {
        bool horizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(horizontalSpeed)
        {
            //reverse the current scaling of x axis
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    private void Die()
    {
        if (myBodyColldier.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myBodyColldier.sharedMaterial = null;
            GetComponent<Rigidbody2D>().velocity = deathKick;
            myRigidBody.freezeRotation = false;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

}
