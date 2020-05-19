using System.Collections;
using System.Collections.Generic;
using TD.AI;
using TD.Debuffs;
using UnityEngine;

[CreateAssetMenu(fileName = "Slow", menuName = "TD/Debuffs/Slow", order = 0)]
public class Slow : Debuff
{
    [Range(0f, 1f)]
    public float factor;
    public override void Calculate(DebuffManager manager)
    {
        Mob mob = manager.GetComponent<Mob>();
        if(mob != null)
        {
            mob.Scale = factor;
        }

        
    }

    public override void Revert(DebuffManager manager)
    {
        Mob mob = manager.GetComponent<Mob>();
        if(mob != null)
        {
            mob.RevertSpeed();
        }
    }

    
}
