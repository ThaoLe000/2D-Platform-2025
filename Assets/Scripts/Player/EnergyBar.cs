using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Energy energyPlayer;
    [SerializeField] private Image totalEnergyBar;
    [SerializeField] private Image currentEnergyBar;

    private void Start()
    {
        totalEnergyBar.fillAmount = energyPlayer.energyTotal / 10;
    }
    private void Update()
    {
        currentEnergyBar.fillAmount = energyPlayer.energyCurrent / 10;
        EnergyTotalBarUpdate();
    }
    private void EnergyTotalBarUpdate()
    {
        totalEnergyBar.fillAmount = energyPlayer.energyTotal / 10;
    }
}
