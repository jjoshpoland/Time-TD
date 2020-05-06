using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Managers
{
    [CreateAssetMenu(fileName = "Wave", menuName = "TD/Wave", order = 0)]
    public class Wave : ScriptableObject
    {
        public List<MobQuantity> Mobs;
    }

    [System.Serializable]
    public class MobQuantity
    {
        public GameObject Mob;
        public int Quantity;
    }
}

