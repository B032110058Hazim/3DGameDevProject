using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinLoseUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Sounds")]
    public AudioSource source;
    public AudioClip click;
    public AudioClip bgm;

    // Start is called before the first frame update
    void Start()
    {
        HideAllPanels();

        //bgm sounds
        //source.clip = bgm;
        //source.Play();
    }

    void HideAllPanels()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void PAClicked()
    {
        source.PlayOneShot(click);

        HideAllPanels();

        SceneManager.LoadScene("MainMenu");

    }

    public void QuitClicked()
    {
        source.PlayOneShot(click);

        HideAllPanels();

        UnityEditor.EditorApplication.isPlaying = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(winPanel.activeSelf == false)
            {
                winPanel.SetActive(true);
                losePanel.SetActive(false);
            }
            else if (winPanel.activeSelf == true)
                winPanel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if(losePanel.activeSelf == false)
            {
                winPanel.SetActive(false);
                losePanel.SetActive(true);
            }
            else if (losePanel.activeSelf == true)
                losePanel.SetActive(false);
        }
    }
}
