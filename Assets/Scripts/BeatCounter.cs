using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public float bpm; // значение BPM
    [SerializeField]private float ticksPerSecond; // число тиков в секунду
    [SerializeField] private float currentSec; // текущее количество тиков
    [SerializeField] private int ticksSpent;

    void Start()
    {
        UpdateTicksPerSecond();
    }

    void Update()
    {
        currentSec+=Time.deltaTime;
        if (currentSec >= ticksPerSecond)
        {
            currentSec = 0;
            OnTick();
        }
    }

    public void SetBPM(float bpm)
    {
        this.bpm = bpm;
        UpdateTicksPerSecond();
    }

    private void UpdateTicksPerSecond()
    {
        ticksPerSecond = 60f / (bpm * 4f);
    }

    public float GetTicksPerSecond()
    {
        return ticksPerSecond;
    }

    public int GetCurrentTick() 
    { 
        return ticksSpent; 
    }

    public void ResetTicks()
    {
        ticksSpent = 0;
    }

    protected virtual void OnTick()
    {
        ticksSpent++;
    }
}
