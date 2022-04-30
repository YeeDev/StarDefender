using System.Collections.Generic;
using System;
using UnityEngine;

public class StationHealth : MonoBehaviour
{
    public event Action OnTakeDamage;

    [SerializeField] Color defaultColor = Color.blue;
    [SerializeField] Color damageColor = Color.red;

    Queue<MeshRenderer> indicators = new Queue<MeshRenderer>();

    public int GetHealthRemaining { get => indicators.Count; }

    private void Awake() { CreateQueueAndSetColor(); }

    private void CreateQueueAndSetColor()
    {
        foreach (MeshRenderer childRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            if (childRenderer.CompareTag("HealthIndicator"))
            {
                childRenderer.material.color = defaultColor;
                indicators.Enqueue(childRenderer);
            }
        }
    }

    public void TakeDamage()
    {
        indicators.Dequeue().material.color = damageColor;

        if (OnTakeDamage != null) { OnTakeDamage(); }
    }
}
