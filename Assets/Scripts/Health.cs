using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public int HP;
    int maxHP;
    
    public DamageType[] immunities;
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDie;

    public int MaxHP
    {
        get
        {
            return maxHP;
        }
        private set
        {
            maxHP = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHP = HP;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeDamage(int damage, DamageType type)
    {
        if(immunities.Contains(type))
        {
            return false;
        }
        else
        {
            int newHP = HP - damage;
            if(newHP <= 0)
            {
                Die();
            }
            else
            {
                HP = newHP;
            }
            OnTakeDamage.Invoke();
            return true;
        }
    }

    void Die()
    {
        OnDie.Invoke();
        Destroy(gameObject);
    }
}

public enum DamageType
{
    Kinetic, Thermal, Explosive
}
