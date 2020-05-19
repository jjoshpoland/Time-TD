using System.Collections;
using System.Collections.Generic;
using TD.Managers;
using UnityEngine;

namespace TD.Debuffs
{
    public abstract class Debuff : ScriptableObject
    {
        public float duration;
        public float frequency;
        public abstract void Calculate(DebuffManager manager);

        public abstract void Revert(DebuffManager manager);
    }
}

