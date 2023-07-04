using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUIHandler : MonoBehaviour
{
    [Header("Panels")]
    public GameObject playPanel;
    public GameObject playerDetailsPanel;
    public GameObject sessionBrowserPanel;
    public GameObject createSessionPanel;
    public GameObject statusPanel;

    [Header("Player settings")]
    public TMP_InputField playerNameInputField;

    [Header("New game session")]
    public TMP_InputField sessionNameInputField;

    [Header("Sounds")]
    public AudioSource source;
    public AudioClip click;
    public AudioClip bgm;

    // Start is called before the first frame update
    void Start()
    {
        //bgm sounds
        source.clip = bgm;
        source.Play();

        if (PlayerPrefs.HasKey("PlayerNickname"))
            playerNameInputField.text = PlayerPrefs.GetString("PlayerNickname");
    }

    void HideAllPanels()
    {
        playPanel.SetActive(false);
        playerDetailsPanel.SetActive(false);
        sessionBrowserPanel.SetActive(false);
        statusPanel.SetActive(false);
        createSessionPanel.SetActive(false);
    }

    public void PlayClicked()
    {
        source.PlayOneShot(click);

        HideAllPanels();

        playerDetailsPanel.SetActive(true);
    }

    public void OnFindGameClicked()
    {
        source.PlayOneShot(click);

        PlayerPrefs.SetString("PlayerNickname", playerNameInputField.text);
        PlayerPrefs.Save();

        GameManager.instance.playerNickName = playerNameInputField.text;

        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();

        networkRunnerHandler.OnJoinLobby();

        HideAllPanels();

        sessionBrowserPanel.gameObject.SetActive(true);
        FindObjectOfType<SessionListUIHandler>(true).OnLookingForGameSessions();
    }

    public void OnCreateNewGameClicked()
    {
        source.PlayOneShot(click);

        HideAllPanels();

        createSessionPanel.SetActive(true);
    }

    public void OnStartNewSessionClicked()
    {
        source.PlayOneShot(click);

        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();

        networkRunnerHandler.CreateGame(sessionNameInputField.text, "World1");

        HideAllPanels();

        statusPanel.gameObject.SetActive(true);
    }

    public void OnJoiningServer()
    {
        source.PlayOneShot(click);

        HideAllPanels();

        statusPanel.gameObject.SetActive(true);

        source.Stop();
    }
}
