using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score = 0;
    int lastScore = 0;

    // Update is called once per frame
    void Update()
    {
        if(score != lastScore)
        {
            lastScore++;
        }
    }
}
