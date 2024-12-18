using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;
    public bool isStarted = true;
    public BeatScroller scroller;
    [SerializeField] private Stopwatch watch;
    public double elapsedTime;
    [SerializeField] Transform parentObj;
    public float bpm;
    [SerializeField] private BeatCounter beatCounter; // ссылка на экземпл€р класса BeatCounter
    [SerializeField] public static int maxCombo;
    [SerializeField] public static int combo;
    public static int maxHealth;
    [SerializeField] public static int health;
    [SerializeField] public int copyHealth;
    public static int score;
    [SerializeField] public Animator UIComboAnim;
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public bool isGameOver;
    public bool isWin;


    // Animator anim;
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        // gameObject.GetComponent<Text>().text = 
        //ps.Play();
        isGameOver = false;
        gameOverUI.SetActive(false);
        beatCounter.SetBPM(120);
        combo = 0;
        maxHealth = 200;
        health = maxHealth;
        theMusic.Play();
        isWin = false;
        //watch = new Stopwatch();
        //StartTimer();
        //UIComboAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        copyHealth = health;
        //anim.SetInteger("State", 1);
        if (!isStarted)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                isStarted = true;

            }
        }

        if (health <= 0 && !isGameOver)
        {
            gameOver();
        }

        if(!theMusic.isPlaying && !isGameOver && !isWin)
        {
            win();
        }


        //UnityEngine.Debug.Log(health);
        //public void StartTimer()
        //{
        //    watch.Start();
        //}
    }

    public static void SetMaxCombo(int value)
    {
        if (value > maxCombo) maxCombo = value;
    }

    static public void PerfectNoteAction(Animator UIComboAnim)
    {
        
        combo++;
        UIComboAnim.SetInteger("State", 2);
        score += 500;
        //LateAction(UIComboAnim);
    }

    static public void GoodNoteAction(Animator UIComboAnim)
    {
        combo++;
        UIComboAnim.SetInteger("State", 2);
        score += 300;
        //LateAction(UIComboAnim);
    }

    static public void BadNoteAction(Animator UIComboAnim)
    {
        SetMaxCombo(combo);
        UIComboAnim.SetInteger("State", 0);
        combo = 0;
        health -= 5;
        score += 50;
    }

    static public void MissedNoteAction(Animator UIComboAnim)
    {
        SetMaxCombo(combo);
        UIComboAnim.SetInteger("State", 0);
        combo = 0;
        health -= 10;
    }

    static public void LateAction(Animator UIComboAnim)
    {
        UIComboAnim.SetInteger("State", 0);

    }

    public void gameOver()
    {
        if (theMusic != null)
        {
            theMusic.Stop();
        }

        gameOverUI.SetActive(true);
        isGameOver = true;
        
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void openMenu()
    {
        theMusic.Stop();
        SceneManager.LoadScene("MainMenu");
    }

    public void quit()
    {
        theMusic.Stop();
        Application.Quit();
    }

    public void win()
    {
        isWin = true;
        gameWinUI.SetActive(true);
    }
}
