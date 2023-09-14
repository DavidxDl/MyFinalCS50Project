using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance { get; private set; }

    [SerializeField] private Canvas canvas;
    [SerializeField] private AudioClip winClip;
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private List<Image> lives;
    [SerializeField] private GameObject livesUI;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Image liveImg;
    private Transform Panel;
    private Transform thisWasCS50;
    private AudioSource audioSource;
    private Animator camAnimator;
    private DuckController duck;
    private int coins = 0;
    private int counterToGetLife = 0;

    void Awake()
{
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
            
        audioSource = GetComponent<AudioSource>();
        duck = FindAnyObjectByType<DuckController>();
    }



    public void GameOver()
    {
        audioSource.Play();
        coins = 0;
        counterToGetLife = 0;
        coinsText.text = coins.ToString();
        lives.Clear();
        foreach(Transform child in livesUI.transform)
        {
            Destroy(child.gameObject);
        }
        AddLives(2);

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1  >= SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RemoveLife()
    {
        Debug.Log("Remove Life Get Called");
        int length = lives.Count;
        if (length == 0)
        {
            GameOver();
            return;
        }
        for(int i = length -1; i >= 0; i--)
        {
            Debug.Log("in loop");

            Image live = lives[i];
            Debug.Log("if statement");
            audioSource.Play();
            Destroy(livesUI.transform.GetChild(0).gameObject);
            lives.Remove(live);
            return;
        }
    }

    public void AddCoins()
    {
        coins++;
        counterToGetLife++;
        coinsText.text = coins.ToString();
        if(counterToGetLife >= 100)
        {
            AddLives(1);
            counterToGetLife = 0;
        }
    }

    public void Win()
    {
        FindReferences();
        thisWasCS50.gameObject.SetActive(true);
        duck.enabled = false;
        StartCoroutine(IDelayRestart());
    }

    private void FindReferences()
    {
        duck = FindAnyObjectByType<DuckController>();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        camAnimator = virtualCamera.GetComponent<Animator>();
        Panel = GameObject.FindGameObjectWithTag("cs50text").transform;
        Debug.Log(Panel);
        thisWasCS50 = Panel.Find("ThisWasCS50");
        Debug.Log(thisWasCS50);
    }

    public void AddLives(int Ammount)
    {
        for (int i = 0; i < Ammount; i++)
        {
            Image live = Instantiate(liveImg, livesUI.transform);
            live.gameObject.SetActive(true);
            
            lives.Add(liveImg);
        }
    }

    IEnumerator IDelayRestart()
    {
        yield return new WaitForSeconds(2);
        virtualCamera.Follow = Panel.transform;
        camAnimator.SetTrigger("Zoom");
    }
}
