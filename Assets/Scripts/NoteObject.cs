using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    [SerializeField] bool pressable;

    [SerializeField] KeyCode keyToPress;
    BeatScroller beatScroller;
    GameManager gameManager;


    float deathTimer = 2.05f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pressable = false;
        BeatScroller beatScroller = transform.parent.GetComponent<BeatScroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (pressable)
            {
                Vector3 parentCoord = transform.parent.transform.position;
                if (Mathf.Abs(parentCoord.y - transform.position.y) < 0.15)
                {
                    GameManager.PerfectNoteAction(gameManager.UIComboAnim, gameManager.performanceTextUI);
                    gameManager.UIComboAnim.SetInteger("State", 1);
                }
                else if (Mathf.Abs(parentCoord.y - transform.position.y) < 0.45)
                {
                    GameManager.GoodNoteAction(gameManager.UIComboAnim, gameManager.performanceTextUI);
                    gameManager.UIComboAnim.SetInteger("State", 1);
                }
                else // if (Mathf.Abs(parentCoord.y - transform.position.y) < 0.45)
                {
                    GameManager.BadNoteAction(gameManager.UIComboAnim, gameManager.performanceTextUI);
                }
                Destroy(gameObject); 
            }
        }
        
        if (deathTimer < 0 && !gameManager.isGameOver)
        {
            GameManager.MissedNoteAction(gameManager.UIComboAnim, gameManager.performanceTextUI);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            pressable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            pressable = false;
        }
    }

    public void DeathTimerTick()
    {
        deathTimer -= Time.deltaTime;
    }
}
