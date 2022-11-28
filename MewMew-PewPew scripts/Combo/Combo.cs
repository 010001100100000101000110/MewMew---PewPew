using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    public int comboCounter { get; private set; }

    [SerializeField] float timeTillComboDecay;
    float timeTillComboDecay2;

    [SerializeField] float timeBetweenComboDecreases;
    float timeBetweenComboDecreases2;
    public int ComboLimit { get; private set; }

    bool isFrozen;

    [SerializeField] int comboDecreasedOnHit = 25;

    [SerializeField] GameEvent combo100Event;
    PlayerHealth playerHealth;

    public delegate void FullCombo();
    public FullCombo fullCombo;

    void Start()
    {
        comboCounter = 1;
        ComboLimit = 100;
        timeBetweenComboDecreases2 = timeBetweenComboDecreases;
        timeTillComboDecay2 = timeTillComboDecay;
        
    }

    public bool isFullCombo()
    {
        return comboCounter >= ComboLimit;
    }

    private void OnEnable()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.healthChange += OnHitComboReduce;
    }

    private void OnDisable()
    {
        playerHealth.healthChange -= OnHitComboReduce;
    }

    void Update()
    {
        ComboDecreaseCooldownTimer();
        if (comboCounter < 1) comboCounter = 1;

        //if (Input.GetKeyDown(KeyCode.F)) FreezeCounter();
        //if (Input.GetKeyDown(KeyCode.C)) ContinueCounter();
        //if (Input.GetKeyDown(KeyCode.A)) 
        //{
        //    AddToCombo(2);
        //    ResetTimers();
        //}
        //if (Input.GetKeyDown(KeyCode.R)) ResetCounter();
        comboCounter = Mathf.Clamp(comboCounter, 1, ComboLimit);
        
    }
   
    public void ChangeComboCounter(int amount)
    {
        comboCounter = amount;
    }
    public void AddToCombo(int amount)
    {
        if (!isFrozen)
        {
            ComboMaxAchieved();

            if (comboCounter + amount > ComboLimit)
            {
                comboCounter = ComboLimit;
                return;
            }
            comboCounter += amount;
        }        
    }

    public void OnHitComboReduce()
    {
        if (!isFrozen)
        {
            if (comboCounter - comboDecreasedOnHit <= 0)
            {
                comboCounter = 1;
                return;
            }
            comboCounter -= comboDecreasedOnHit;
        }
    }

    public void SubtractFromCombo(int amount)
    {
        if (!isFrozen)
        {
            if (comboCounter - amount < 1)
            {
                comboCounter = 1;
                return;
            }
            comboCounter -= amount;
        }
    }

    void ComboDecreaseCooldownTimer()
    {
        if (!isFrozen)
        {
            timeTillComboDecay -= Time.deltaTime;

            if (timeTillComboDecay <= 0)
            {
                timeTillComboDecay = 0;
                timeBetweenComboDecreases += Time.deltaTime;
                if (timeBetweenComboDecreases >= timeBetweenComboDecreases2)
                {
                    SubtractFromCombo(1);
                    timeBetweenComboDecreases = 0;
                }
            }
        }
    }
    public void ResetTimers()
    {
        timeTillComboDecay = timeTillComboDecay2;
        timeBetweenComboDecreases = timeBetweenComboDecreases2;
    }

    public void FreezeCounter()
    {
        isFrozen = true;
        ResetTimers();
    }

    public void ContinueCounter()
    {
        isFrozen = false;
    }

    void ResetCounter()
    {
        if (!isFrozen)
        {
            comboCounter = 1;
            ResetTimers();
        }
            
    }
    public void ChangeComboCap(int max)
    {
        ComboLimit = max;
        if (comboCounter > ComboLimit) comboCounter = ComboLimit;
    }

    public void ComboMaxAchieved()
    {
        if (comboCounter >= ComboLimit)
        {
            fullCombo?.Invoke();
            comboCounter = ComboLimit;
        }
    }
}
