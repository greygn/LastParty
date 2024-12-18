using System;
using System.IO;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    [SerializeField] GameObject arrowLeft, arrowRight, arrowUp, arrowDown, holder;
    [SerializeField] Vector3[] spawnPosition;
    [SerializeField] long CodedSequence;
    [SerializeField] int rhytm, lateRhytm;
    [SerializeField] private GameManager manager;
    [SerializeField] private StreamReader reader;
    [SerializeField] private String rawText;
    [SerializeField] private float bpm;
    [SerializeField] private float timePerBeat;
    [SerializeField] private BeatCounter beatCounter;
    public int notesCount = 0;
    private int currentNote = 0;
    double elapsedTime;
    private ArrowRecord[] arrows;

    public float timer;
    bool readyToSpawn = true;
    void Start()
    {
        OpenFile();
        arrows = ScanArrows(rawText);
        bpm = manager.bpm;
        timePerBeat = 60f / bpm * 2;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = manager.elapsedTime;
        
        //timer += Time.deltaTime;
        //rhytm = Mathf.FloorToInt(timer);
        if (currentNote < notesCount && ArrowSpawner(arrows[currentNote]))
        {
            currentNote++;
            beatCounter.ResetTicks();
        }
        //lateRhytm = rhytm;
        //readyToSpawn = true;
        //timer += Time.deltaTime;
        
    }

    bool ArrowSpawner(ArrowRecord arrow)
    {
        if (arrow.GetTicksFromLast() <= beatCounter.GetCurrentTick())
            
        {
            switch (arrow.GetDirection())
            {
                case 'A':
                    {
                        GameObject newArrow = Instantiate(arrowLeft);
                        newArrow.transform.parent = holder.transform;
                        newArrow.transform.position = spawnPosition[0];
                        break;
                    }
                case 'B':
                    {
                        GameObject newArrow = Instantiate(arrowUp);
                        newArrow.transform.parent = holder.transform;
                        newArrow.transform.position = spawnPosition[1];
                        break;
                    }
                case 'C':
                    {
                        GameObject newArrow = Instantiate(arrowDown);
                        newArrow.transform.parent = holder.transform;
                        newArrow.transform.position = spawnPosition[2];
                        break;
                    }
                case 'D':
                    {
                        GameObject newArrow = Instantiate(arrowRight);
                        newArrow.transform.parent = holder.transform;
                        newArrow.transform.position = spawnPosition[3];
                        break;
                    }
            }
            return true;

            //readyToSpawn = false;

        }
        return false;
    }

    private void OpenFile()
    {
        try
        {
            reader = null;
            string filePath = "Assets/Prefs/NotesPlacement1.txt";
            reader = new StreamReader(filePath);
            rawText = reader.ReadLine();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    private ArrowRecord[] ScanArrows(String rawText)
    {
        int recordLength = 4;
        notesCount = rawText.Length / recordLength;
        //int CurrentCount = 0;
        ArrowRecord[] res = new ArrowRecord[notesCount];

        for (int i = 0; i < notesCount; i++)
        {
            Char tempType;
            int tempCount = 0;
            tempType = rawText[i * recordLength];
            if (tempType != 'A' && tempType != 'B' && tempType != 'C' && tempType != 'D')
            {
                Debug.Log("incorrect note type: " + tempType);
                continue;
            }

            String tempString = rawText.Substring(i * recordLength + 1, recordLength - 1);
            int.TryParse(tempString, out tempCount);

            res[i] = new ArrowRecord(tempType, tempCount);

        }

        //for (int i = 0; i < notesCount; i++)
        //{

        //    Debug.Log(res[i].toString());
        //}
        return res;
    }

    public class ArrowRecord
    {
        public Char direction;
        public int ticksFromLast;

        public ArrowRecord(Char type, int ticksFromLast)
        {
            this.direction = type;
            this.ticksFromLast = ticksFromLast;
        }

        public Char GetDirection()
        {
            return direction;
        }

        public String toString()
        {
            return direction.ToString() + ticksFromLast.ToString();
        }

        public int GetTicksFromLast()
        {
            return ticksFromLast;
        }
    }
}