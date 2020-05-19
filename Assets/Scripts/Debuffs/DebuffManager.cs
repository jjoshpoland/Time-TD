using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TD.Debuffs
{
    public class DebuffManager : MonoBehaviour
    {

        List<Debuff> debuffs;
        Dictionary<Debuff, float> startTimes;
        Dictionary<Debuff, float> procTimes;

        // Start is called before the first frame update
        void Start()
        {
            startTimes = new Dictionary<Debuff, float>();
            procTimes = new Dictionary<Debuff, float>();
            debuffs = new List<Debuff>();
        }

        // Update is called once per frame
        void Update()
        {
            List<Debuff> expiredDebuffs = new List<Debuff>();
            for (int i = 0; i < debuffs.Count; i++)
            {
                if(startTimes[debuffs[i]] + debuffs[i].duration < Time.time)
                {
                    expiredDebuffs.Add(debuffs[i]);
                }
            }

            for (int i = 0; i < expiredDebuffs.Count; i++)
            {
                Expire(expiredDebuffs[i]);
            }

            for (int i = 0; i < debuffs.Count; i++)
            {
                if(Time.time >= procTimes[debuffs[i]] + debuffs[i].frequency)
                {
                    debuffs[i].Calculate(this);
                    procTimes[debuffs[i]] = Time.time;
                }
            }
        }

        public void AddDebuff(Debuff newDebuff)
        {
            if(debuffs.Contains(newDebuff))
            {
                startTimes[newDebuff] = Time.time;
                procTimes[newDebuff] = 0f;
            }
            else
            {
                debuffs.Add(newDebuff);
                startTimes[newDebuff] = Time.time;
                procTimes[newDebuff] = 0f;
            }
        }

        void Expire(Debuff expiredDebuff)
        {
            expiredDebuff.Revert(this);
            debuffs.Remove(expiredDebuff);
            startTimes.Remove(expiredDebuff);
        }
    }
}

