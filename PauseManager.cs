using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text scoreTextLose;
    [SerializeField] private Slider manaSlider;
    [SerializeField] private float sliderPlus;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject newRecordObj;
    [SerializeField] private float difficultyPlus = 0.001f;
    private bool newRecord = false;
    private Camera _cam;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private GameObject musicOffObj;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.UpdateState(GameState.Game);
        GameManager.mana = 1f;
        GameManager.difficultyMultiply = 1f;
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        newRecordObj.SetActive(newRecord);
        musicOffObj.SetActive(!GameManager.musicOn);
        if (GameManager.state == GameState.Game)
        {
            if (GameManager.musicOn == true)
            {
                if (musicSource.isPlaying == false)
                {
                    musicSource.Play();
                }
            }
            else
            {
                if (musicSource.isPlaying == true)
                {
                    musicSource.Stop();
                }
            }
            if (((player.transform.position.x + 2.5f) / 2) * difficultyPlus < 1f)
            {
                GameManager.difficultyMultiply = 1 + ((player.transform.position.x + 2.5f) / 2) * difficultyPlus;

            }
            else
            {
                GameManager.difficultyMultiply = 2f;
            }
            //scoreText.color = new Color(0, 0.6f + (((player.transform.position.x + 2.5f) / 2) * 0.001f) / 0.5f * 0.4f, 1f);
            if (((int)(player.transform.position.x + 2.5f) / 2) <= 250)
            {
                _cam.backgroundColor = new Color(0.6f, 0.9f - (((player.transform.position.x + 2.5f) / 2) * 0.002f) / 0.5f * 0.2f, 1f);
                //scoreText.color = new Color(0, 0.6f + (((player.transform.position.x + 2.5f) / 2) * 0.001f) / 0.5f * 0.4f, 1f);
            }
            else if (((int)(player.transform.position.x + 2.5f) / 2) <= 750)
            {
                _cam.backgroundColor = new Color(0.6f, 0.7f - (((player.transform.position.x + 2.5f) / 2 - 250f) * 0.001f) / 0.5f * 0.4f, 1f - (((player.transform.position.x + 2.5f ) / 2 - 250f) * 0.001f) / 0.5f * 0.4f);
            }
            else if (((int)(player.transform.position.x + 2.5f) / 2) <= 1000)
            {
                _cam.backgroundColor = new Color(0.6f - (((player.transform.position.x + 2.5f ) / 2 - 750f) * 0.002f) / 0.5f * 0.3f, 0.3f - (((player.transform.position.x + 2.5f ) / 2 - 750f) * 0.002f) / 0.5f * 0.3f, 0.6f - (((player.transform.position.x + 2.5f) / 2 - 750f) * 0.002f) / 0.5f * 0.2f);
            }
            else {
                _cam.backgroundColor = new Color(0.3f, 0f, 0.4f);
            }
            scoreText.text = (((int)(player.transform.position.x + 2.5f) / 2)).ToString() + "M";
            scoreTextLose.text = (((int)(player.transform.position.x + 2.5f) / 2)).ToString() + "M";
        }
        else {
            if (musicSource.isPlaying == true)
            {
                musicSource.Stop();
            }
        }
        if (GameManager.state == GameState.Pause)
        {
            pausePanel.SetActive(true);
        }
        else {
            pausePanel.SetActive(false);
        }
        if (GameManager.state == GameState.Lose)
        {
            if (losePanel.active == false) {
                audioSource.PlayOneShot(loseSound, 0.75f);
            }
            losePanel.SetActive(true);
            if (PlayerPrefs.GetInt("Record") < (((int)(player.transform.position.x + 2.5f) / 2))) {
                PlayerPrefs.SetInt("Record", (((int)(player.transform.position.x + 2.5f) / 2)));
                newRecord = true;
            }
        }
        else
        {
            losePanel.SetActive(false);
        }
        if (Mathf.Abs(GameManager.mana - manaSlider.value) < 0.025f)
        {
            manaSlider.value = GameManager.mana;
        }
        else {
            if (manaSlider.value < GameManager.mana)
            {
                manaSlider.value += sliderPlus * Time.deltaTime;
                if (manaSlider.value > GameManager.mana)
                {
                    manaSlider.value = GameManager.mana;
                }
            }
            if (manaSlider.value > GameManager.mana)
            {
                manaSlider.value -= sliderPlus * Time.deltaTime;
                if (manaSlider.value < GameManager.mana)
                {
                    manaSlider.value = GameManager.mana;
                }
            }
        }
        
    }

    public void PauseBut() {
        GameManager.UpdateState(GameState.Pause);
    }

    public void ResumeBut() {
        GameManager.UpdateState(GameState.Game);
    }

    public void RestartBut()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.springOnes = new List<SpringOne>();
    }

    public void MenuBut()
    {
        SceneManager.LoadScene("MenuScene");
        GameManager.springOnes = new List<SpringOne>();
    }

    public void MusicBut() {
        GameManager.musicOn = !GameManager.musicOn;
    }
}
