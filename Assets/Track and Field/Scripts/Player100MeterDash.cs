using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Player100MeterDash : MonoBehaviour
{

    [SerializeField] public float acceleration = 2f;
    [SerializeField] public float deceleration = 1f;
    [SerializeField] public float hurdleSlow = 5f;
    [SerializeField] public float maxSpeed = 12f;
    [SerializeField] public float minSpeed = 2f;
    [SerializeField] public AudioClip Running;

    public Slider slider;
    public Animator myAnimator;
    AudioSource audiosource;

    [SerializeField] public float currentSpeed;

    [SerializeField] public float walkSpeed = 1f;
    public Vector3 targetPosition = new Vector2(-4.6f, -3.5f);

    private float referenceScreenWidth = 1920f;

    public bool walkingToLine = true;
    private bool canRun = true;
    private bool firstStep = true;
    Rigidbody2D myRigidbody2D;
    CapsuleCollider2D myCapsuleCollider2D;


    void Start()
    {
        canRun = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        audiosource = GetComponent<AudioSource>();



    }


    void Update()

    {   // Determine the screen width
        float screenWidth = Screen.width;

        // Calculate the speed scale based on the current screen width
        float speedScale = screenWidth / referenceScreenWidth;

        // Calculate the adjusted acceleration and deceleration
        float adjustedAcceleration = acceleration * speedScale;
        float adjustedDeceleration = deceleration * speedScale;

        if (walkingToLine)
        {
            StartCoroutine(WalkToLine());
        }


        if (canRun)
        {


            if(firstStep == true && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                currentSpeed = minSpeed;
                firstStep = false;
            }

            if(currentSpeed == 0)
            {
                firstStep = true;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                audiosource.PlayOneShot(Running, .8f);

                currentSpeed += adjustedAcceleration * Time.deltaTime;
                currentSpeed = Mathf.Min(currentSpeed, maxSpeed);



                if (FindObjectOfType<ScoreBoard>().isTimerRunning == false)
                {
                    FindObjectOfType<ScoreBoard>().Fault();
                    canRun = false;
                }
            }
            else
            {
                currentSpeed -= adjustedDeceleration * Time.deltaTime;

                currentSpeed = Mathf.Max(currentSpeed, 0f);
            }


            Vector2 newVelocity = new Vector2(currentSpeed, myRigidbody2D.velocity.y);
            myRigidbody2D.velocity = newVelocity;

        }

        //END OF RACE DECELERATION
        else
        {
            currentSpeed -= adjustedDeceleration * Time.deltaTime * 4f;
            currentSpeed = Mathf.Max(currentSpeed, 0f);

            Vector2 newVelocity = new Vector2(currentSpeed, myRigidbody2D.velocity.y);
            myRigidbody2D.velocity = newVelocity;



        }


        float playerVelocity = myRigidbody2D.velocity.magnitude;

        // Normalize the velocity to fit within the Slider's value range (0 to 1)
        float normalizedVelocity = playerVelocity / maxSpeed;

        // Update the Slider value
        slider.value = normalizedVelocity;


        //----------------------------RACOON ANIMATION---------------------------------------

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            myAnimator.SetTrigger("Jump");
        }
        if (playerVelocity > 0 && playerVelocity < 2)
        {
            myAnimator.SetBool("Walking", true);
        }
        else
        {
            myAnimator.SetBool("Walking", false);
        }
        if (playerVelocity >= 2)
        {
            myAnimator.SetBool("RunningSlow", true);
        }
        else
        {
            myAnimator.SetBool("RunningSlow", false);
        }
        if (playerVelocity > 8)
        {
            myAnimator.SetBool("RunningFast", true);
        }
        else
        {
            myAnimator.SetBool("RunningFast", false);
        }





        // delete after fox sprite, testing only.
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    myCapsuleCollider2D.offset = new Vector2(0f, 0.42f);
        //    myCapsuleCollider2D.size = new Vector2(1f, 1.12f);
        //    transform.localScale = new Vector2(0.8f, 0.5f);
        //    Vector3 newPosition = transform.localPosition;
        //    newPosition.y = -3f;
        //    transform.localPosition = newPosition;
        //}
        //else
        //{
        //    myCapsuleCollider2D.offset = new Vector2(0f, 0f);
        //    myCapsuleCollider2D.size = new Vector2(1f, 2f);
        //    transform.localScale = new Vector2(0.8f, 1f);
        //    Vector3 newPosition = transform.localPosition;
        //    newPosition.y = -3.5f;
        //    transform.localPosition = newPosition;
        //}


        



    }

    IEnumerator PlayRunningSound()
    {
        yield return new WaitForSeconds(.5f);

    }
    public void JumpColliderOn()
    {
        myCapsuleCollider2D.enabled = true;
    }
    public void JumpColliderOff()
    {
        myCapsuleCollider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Finish"))
        {
            print("you crossed the finish line!");
            canRun = false;


            FindObjectOfType<ScoreBoard>().StopTimer();
            FindObjectOfType<ScoreBoard>().UpdateScore();


        }

        if (collider.CompareTag("Obstacle"))
        {
            print("hurdle hit");
            
            currentSpeed -= hurdleSlow;
            
        }
    }





    IEnumerator WalkToLine()
    {
        yield return new WaitForSeconds(2f);

            // Calculate the new position gradually
        Vector2 newPosition = Vector2.Lerp(transform.position, targetPosition, walkSpeed * Time.deltaTime);

            // Set the character's position to the new position
        transform.position = newPosition;

        myAnimator.SetBool("Walking", true);

        if (myRigidbody2D.position.x >= -4.85f)
        {
            myAnimator.SetBool("Walking", false);
        }

        if (myRigidbody2D.position.x >= -4.62f)
        {
            //Debug.Log("testing");
            transform.position = targetPosition;
            walkingToLine = false;
 
        }
    }
}
