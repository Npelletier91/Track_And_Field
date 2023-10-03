using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class ScoreBoard : MonoBehaviour
{

    private float startTime;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI CPUtimerText;
    public bool isTimerRunning = false;
    public bool CPUTimerRunner = false;
    public bool noFault = true;


    public float previousScore;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI countDown;
    [SerializeField] TextMeshProUGUI faultText;
    [SerializeField] TextMeshProUGUI qualifyingTime;
    [SerializeField] TextMeshProUGUI didNotQualify;

    [SerializeField] AudioClip Go, GameOver, Congratulations, Round, Round2, Ready, Set , One, Finish, Wrong;
    AudioSource audioSource;

    private void Awake()
    {
        int numberOfSessions = FindObjectsOfType<ScoreBoard>().Length;

        if (numberOfSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();


        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Track_Level Dash"))
        {
            StartCoroutine(Round1Sound());
        }

        StartCoroutine(CountDown1());
    }


    void Update()
    {
        if (isTimerRunning)
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimerText(elapsedTime);
        }
        if (CPUTimerRunner)
        {
            float elapsedTime = Time.time - startTime;
            CPUUpdateTimerText(elapsedTime);
        }


    }
    private void UpdateTimerText(float time)
    {
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = Mathf.Floor(time % 60).ToString("00");
        string milliseconds = Mathf.Floor((time * 1000) % 1000).ToString("000");

        timerText.text = seconds + ":" + milliseconds;
    }

    private void CPUUpdateTimerText(float time)
    {
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = Mathf.Floor(time % 60).ToString("00");
        string milliseconds = Mathf.Floor((time * 1000) % 1000).ToString("000");

        CPUtimerText.text = seconds + ":" + milliseconds;
    }
    public void UpdateScore()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Track_Level Dash"))
        {
            StartCoroutine(DashEndScore());
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Track_Level Hurdles"))
        {
            StartCoroutine(HurdleEndScore());
        }

    }
    public void StopTimer()
    {
        isTimerRunning = false;
    }
    public void CPUStopTimer()
    {
        CPUTimerRunner = false;
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        CPUTimerRunner = true;
        startTime = Time.time;
    }

    public void Fault()
    {
        audioSource.PlayOneShot(Wrong);
        countDown.text = " ";
        faultText.text = "FAULT";
        noFault = false;
        StartCoroutine(twoSecondsReset());
    }
    IEnumerator twoSecondsReset()
    {
        yield return new WaitForSeconds(2f);
        ResetGame();
    }
    private void ResetGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        StartCoroutine(CountDown1());
        faultText.text = " ";
        noFault = true;
    }

    IEnumerator Round1Sound()
    {
        audioSource.PlayOneShot(Round);
        yield return new WaitForSeconds(.5f);
        audioSource.PlayOneShot(One);
    }
    IEnumerator CountDown1()
    {
        
        yield return new WaitForSeconds(3f);

        countDown.enabled = true;

        if (noFault)
        {
            countDown.text = "ON YOUR MARK";
            audioSource.PlayOneShot(Ready);
            StartCoroutine(CountDown2());
        }
    }
    IEnumerator CountDown2()
    {
        yield return new WaitForSeconds(3f);

        if (noFault)
        {
            countDown.text = "GET SET..";
            audioSource.PlayOneShot(Set);
            StartCoroutine(CountDownGo());
        }
    }
    
    IEnumerator CountDownGo()
    {

        yield return new WaitForSeconds(3f);

        if (noFault)
        {
            countDown.color = Color.red;
            countDown.fontSize = 100;
            audioSource.PlayOneShot(Go);
            countDown.text = "GO!";
            
           

            startTime = Time.time;
            isTimerRunning = true;
            CPUTimerRunner = true;

            FindObjectOfType<CPU100MeterDash>().canRun = true;

            yield return new WaitForSeconds(1f);
            countDown.enabled = false;

        }
    }


    
    IEnumerator DashEndScore()
    {
            float timer = Time.time - startTime;

            if (timer <= 10)
            {
            audioSource.PlayOneShot(Finish);
                yield return new WaitForSeconds(4f);

                FindObjectOfType<FansAnimation>().Cheer();

                float score = 5000 - (timer * 150);
                previousScore = score;

                scoreText.text = score.ToString("0000");
                audioSource.PlayOneShot(Congratulations);



                // LOAD NEXT SCENE
                yield return new WaitForSeconds(4f);

                SceneManager.LoadScene("Track_Level Hurdles");
                StartCoroutine(CountDown1());
                timerText.text = "00:000";
                CPUtimerText.text = "00:000";
                qualifyingTime.text = "Qualifying      13:000";
                countDown.color = Color.yellow;
                countDown.fontSize = 40;
                audioSource.PlayOneShot(Round2);
            }
            else
            {
                didNotQualify.text = "Did Not Qualify :(";
                audioSource.PlayOneShot(GameOver);
                yield return new WaitForSeconds(3f);
                audioSource.PlayOneShot(GameOver);
                SceneManager.LoadScene("Track_Game Over");
                Destroy(gameObject);
            }
    }
    IEnumerator HurdleEndScore()
    {
        float timer = Time.time - startTime;

        if(timer <= 13)
        {
            audioSource.PlayOneShot(Finish);
            yield return new WaitForSeconds(4f);

            FindObjectOfType<FansAnimation>().Cheer();

            float score = 5000 - (timer * 200);

            previousScore += score;

            scoreText.text = previousScore.ToString("0000");

            audioSource.PlayOneShot(Congratulations);

            //LOAD NEXT SCENE
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("Track_Game Score");


            


            //destroy score board manager to be able to start over
            yield return new WaitForSeconds(1.5f);

            Destroy(gameObject);
        }       
        else
        {
            didNotQualify.text = "Did Not Qualify :(";
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Track_Game Over");
            yield return new WaitForSeconds(1f); 
            didNotQualify.text = " ";
            Destroy(gameObject);
        }
    }
}
