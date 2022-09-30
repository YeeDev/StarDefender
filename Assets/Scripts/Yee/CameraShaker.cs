using System.Collections;
using UnityEngine;

namespace Yee.VFX
{
	public class CameraShaker : MonoBehaviour
	{
		Vector3 originalPos;

		private void Awake() { originalPos = transform.position; }

		public IEnumerator Shake(float duration, float intensity)
		{
			float shakeTimer = duration;
			while (shakeTimer > 0)
			{
				shakeTimer -= Time.deltaTime;
				transform.position = originalPos + Random.insideUnitSphere * intensity;

				yield return new WaitForEndOfFrame();
			}

			transform.position = originalPos;
		}
	}
}