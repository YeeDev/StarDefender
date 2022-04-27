using UnityEngine.UI;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] Text turretsText = null;
    [SerializeField] float healthBarXSize = 64f;
    [SerializeField] RectTransform healthBar = null;

    StationHealth stationHealth;
    EnergyBank energyBank;

    private void Awake()
    {
        energyBank = FindObjectOfType<EnergyBank>();
        stationHealth = FindObjectOfType<StationHealth>();
        turretsText.text = $"x {energyBank.GetMaxActiveTowers}";
    }

    private void OnEnable() { stationHealth.OnTakeDamage += UpdateHealth; }
    private void OnDisable() { stationHealth.OnTakeDamage -= UpdateHealth; }

    private void UpdateHealth()
    {
        Vector2 newSize = healthBar.sizeDelta;
        newSize.x = Mathf.Clamp(stationHealth.GetHealthPoints * healthBarXSize, 0, 5 * healthBarXSize);
        healthBar.sizeDelta = newSize;
    }
}
