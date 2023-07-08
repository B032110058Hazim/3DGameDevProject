using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int score = 0;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            score = 0;
            scoreText.text = "0";
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            score = 1;
            scoreText.text = "1";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            score = 2;
            scoreText.text = "2";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            score = 3;
            scoreText.text = "3";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            score = 4;
            scoreText.text = "4";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            score = 5;
            scoreText.text = "5";
        }
    }
}
