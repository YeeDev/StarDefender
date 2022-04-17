using UnityEngine.UI;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] Text turretsText = null;

    EnergyBank energyBank;

    private void Awake()
    {
        energyBank = FindObjectOfType<EnergyBank>();
        turretsText.text = $"x {energyBank.GetMaxActiveTowers}";
    }
}
