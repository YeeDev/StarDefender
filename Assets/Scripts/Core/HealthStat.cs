using UnityEngine;

namespace StarDef.Core
{
    public class HealthStat : MonoBehaviour
    {
        int totalHealth;
        Animator animator;

        private void Awake() { animator = GetComponent<Animator>(); }

        public bool NoHealth { get => totalHealth <= 0; }

        public void AddHealth(int amountToAdd) { totalHealth += amountToAdd;Debug.Log(totalHealth); }

        public void TakeDamage(bool lowerHealth)
        {
            if (lowerHealth) { totalHealth--; }
            animator.SetTrigger("Hit");
            animator.SetInteger("Health", totalHealth);
        }
    }
}