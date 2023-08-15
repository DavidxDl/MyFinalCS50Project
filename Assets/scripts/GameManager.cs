using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance { get; private set; }

    [SerializeField] private TextMeshProUGUI thisWasCS50;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private DuckController duck;
    [SerializeField] private List<Image> lives;
    [SerializeField] private TextMeshProUGUI coinsText;
    private AudioSource audioSource;
    private Animator camAnimator;
    private int coins = 0;

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        camAnimator = virtualCamera.GetComponent<Animator>();
    }



    public void GameOver()
    {
        audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RemoveLife()
    {
        int length = lives.Count;
        if (length == 0)
        {
            GameOver();
            return;
        }
        for(int i = length -1; i >= 0; i--)
        {
            Image live = lives[i];
            if(live.gameObject.activeInHierarchy == true)
            {
                audioSource.Play();
                live.gameObject.SetActive(false);
                lives.Remove(live);
                return;
            }
        }
    }

    public void AddCoins()
    {
        coins++;
        coinsText.text = coins.ToString();
    }

    public void Win(DuckController duck)
    {
        
        thisWasCS50.gameObject.SetActive(true);
        duck.enabled = false;
        StartCoroutine(IDelayRestart());



    }
    IEnumerator IDelayRestart()
    {
        yield return new WaitForSeconds(2);
        virtualCamera.Follow = thisWasCS50.transform;
        camAnimator.SetTrigger("Zoom");
    }
}
