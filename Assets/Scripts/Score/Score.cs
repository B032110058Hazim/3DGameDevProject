using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            scoreText.text = "0";
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            scoreText.text = "1";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            scoreText.text = "2";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            scoreText.text = "3";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            scoreText.text = "4";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            scoreText.text = "5";
        }
    }
}
