using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject musicOffObj;
    [SerializeField] private TMP_Text recordText;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Dno", true);
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        musicOffObj.SetActive(!GameManager.musicOn);
        recordText.text = PlayerPrefs.GetInt("Record").ToString() + "M";
    }

    public void MusicBut() { 
        GameManager.musicOn = !GameManager.musicOn;
    }

    public void PlayBut() {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitBut()
    {
        Application.Quit();
    }
}
