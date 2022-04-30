using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
	[SerializeField] [Range(1, 10)] float shakeDuration = 1f;
	[SerializeField] [Range(0.1f, 5f)] float shakeIntensity = 1f;

	Vector3 originalPos;
	StationHealth stationHealth;

    private void Awake()
    {
		originalPos = transform.position;
		stationHealth = FindObjectOfType<StationHealth>();	
	}

	private void OnEnable() { stationHealth.OnTakeDamage += StartShake; }
	private void OnDisable() { stationHealth.OnTakeDamage -= StartShake; }

    private void StartShake() { StartCoroutine(Shake()); }

	private IEnumerator Shake()
	{
		float shakeTimer = shakeDuration;
		while (shakeTimer > 0)
		{
			shakeTimer -= Time.deltaTime;
			transform.position = originalPos + Random.insideUnitSphere * shakeIntensity;

			yield return new WaitForEndOfFrame();
		}

		transform.position = originalPos;
	}
}
