using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance { get; private set; }

    [SerializeField] private TextMeshProUGUI thisWasCS50;
    [SerializeField] private AudioClip winClip;
    private AudioSource audioSource;

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();

    }

    public void GameOver()
    {
        audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Win(DuckController duck)
    {
        float timer = 0;
        timer += Time.deltaTime;
        thisWasCS50.gameObject.SetActive(true);
        duck.enabled = false;
        StartCoroutine(IDelayRestart());



    }
    IEnumerator IDelayRestart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
