using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [SerializeField]public GameManager manager;
    public float bpm;
    public float scrollSpeed;
    //public bool hasStarted;

  
    // Start is called before the first frame update
    void Start()
    {
        scrollSpeed = -5;
        bpm = manager.bpm;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            childTransform.Translate(0, scrollSpeed * Time.deltaTime, 0);
            childTransform.GetComponent<NoteObject>().DeathTimerTick();

        }
    }
}
