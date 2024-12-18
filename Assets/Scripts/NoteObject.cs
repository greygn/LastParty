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


    float deathTimer = 3.4f;

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
                    GameManager.PerfectNoteAction(gameManager.UIComboAnim);
                    Debug.Log("perfect");
                    gameManager.UIComboAnim.SetInteger("State", 1);
                }
                else if (Mathf.Abs(parentCoord.y - transform.position.y) < 0.45)
                {
                    GameManager.GoodNoteAction(gameManager.UIComboAnim);
                    Debug.Log("good");
                    gameManager.UIComboAnim.SetInteger("State", 1);
                }
                else if (Mathf.Abs(parentCoord.y - transform.position.y) < 0.6)
                {
                    GameManager.BadNoteAction(gameManager.UIComboAnim);
                    Debug.Log("bad");
                }
                Destroy(gameObject);
            }
        }
        
        if (deathTimer < 0)
        {
            GameManager.MissedNoteAction(gameManager.UIComboAnim);
            Destroy(gameObject);
            Debug.Log("miss");
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
