using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Track_Game Score"))
        {
            StartCoroutine(PrintScore());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PrintScore()
    {
        yield return new WaitForSeconds(1f);
        scoreText.text = FindObjectOfType<ScoreBoard>().previousScore.ToString("0000");
    }
}
