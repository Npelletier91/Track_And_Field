using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU100MeterDash : MonoBehaviour
{

    [SerializeField] public float maxCPUSpeed = 5f;
    [SerializeField] public float decelerateSpeed = 1f;
    [SerializeField] public float accelerationTime = 5f;

    public Animator myAnimator;



    public float currentSpeed;
    private bool isDecelerating = false;
    private float accelerationTimer = 0f;

    public bool canRun = false;

    [SerializeField] public float walkSpeed = 1f;
    public Vector3 targetPosition = new Vector3(-4.6f, -0.55f, 0f);

    Rigidbody2D myRigidbody2D;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        currentSpeed = 0f;


    }

    void Update()
    {
        if (canRun)
        {
            accelerationTimer += Time.deltaTime;
            currentSpeed = Mathf.Lerp(2f, maxCPUSpeed, accelerationTimer / accelerationTime);
            myRigidbody2D.velocity = new Vector2(currentSpeed, 0f);

            if( currentSpeed >= maxCPUSpeed)
            {
                currentSpeed = maxCPUSpeed;
            }

        }

        if (walkingToLine)
        {
            StartCoroutine(WalkToLine());
        }

        if (isDecelerating)
        {
            // Gradually decrease the CPU's speed over time.
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, decelerateSpeed * Time.deltaTime);

            // Update the rigidbody's velocity with the new speed.
            myRigidbody2D.velocity = new Vector2(currentSpeed, 0f);

            // Check if the speed has reached zero.
            if (currentSpeed <= 0.1f)
            {
                myRigidbody2D.velocity = new Vector2(0f, 0f);
                isDecelerating = false; // Stop deceleration when the speed reaches zero.
                canRun = false;
            }
        }
        if (currentSpeed > 1)
        {
            myAnimator.SetBool("Walking", true);
        }
        else
        {
            myAnimator.SetBool("Walking", false);
        }
        if (currentSpeed >= 2)
        {
            myAnimator.SetBool("RunningSlow", true);
        }
        else
        {
            myAnimator.SetBool("RunningSlow", false);
        }
        if (currentSpeed > 6)
        {
            myAnimator.SetBool("RunningFast", true);
        }
        else
        {
            myAnimator.SetBool("RunningFast", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Finish"))
        {
            print("CPU crossed the finish line!");

            FindObjectOfType<ScoreBoard>().CPUStopTimer();


            StartDeceleration();
            canRun = false;
        }

        if (collider.CompareTag("Effect"))
        {
            myAnimator.SetTrigger("Jump");
        }

    }

    void StartDeceleration()
    {
        isDecelerating = true;
    }

    public bool walkingToLine = true;
    IEnumerator WalkToLine()
    {

        yield return new WaitForSeconds(2f);

        // Calculate the new position gradually
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, walkSpeed * Time.deltaTime);

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
            canRun = false;
        }
    }
}
