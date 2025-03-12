using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float energyTotal = 6;
    public float energyCurrent { get; private set; }

    private void Awake()
    {
        energyCurrent = 0;    
    }
    private void Update()
    {
        energyCurrent = Mathf.Clamp(energyCurrent + Time.deltaTime, 0, energyTotal);
        
    }

    public void UseEnergy(float _energy)
    {
        energyCurrent = Mathf.Clamp(energyCurrent - _energy,0, energyTotal);
    }

    public void AddEnergy(float _energy)
    {
        energyCurrent = Mathf.Clamp(energyCurrent + _energy,0, energyTotal);
    }
    public void AddTotalEnergy(float _energy)
    {
        energyTotal += _energy;
    }
}
