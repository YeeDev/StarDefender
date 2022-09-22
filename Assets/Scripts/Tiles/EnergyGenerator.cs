using System.Collections.Generic;
using UnityEngine;

namespace StarDef.Tiles
{
    public class EnergyGenerator : MonoBehaviour
    {
        [SerializeField] MeshRenderer indicator = null;
        [SerializeField] Color damagedColor = Color.red;

        List<MeshRenderer> paths = new List<MeshRenderer>();

        Transform usedBy = null;

        public void AddPath(List<MeshRenderer> path) { paths.AddRange(path); }

        public bool IsOn { get => indicator.material.color != damagedColor; }
        public bool CanBeUsed(Transform checkTransform)
        {
            return usedBy == null || usedBy == checkTransform;
        }
        public Transform UsedBy { set => usedBy = value; }

        public void DamageGenerator()
        {
            indicator.material.color = damagedColor;

            foreach (var indicator in paths)
            {
                indicator.material.color = Color.red;
            }
        }
    }
}