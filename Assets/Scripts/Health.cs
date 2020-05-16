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
    [SerializeField]
    TextMesh floatingTextPrefab;
    public Vector3 randomTextIntensity = new Vector3(0,0,0);
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDie;
    public UnityEvent OnDelete;


    public bool isDead
    {
        get
        {
            return HP <= 0;
        }
        private set
        {
            return;
        }
    }

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
            ShowFloatingText(0, type);
            return false;
        }
        else
        {
            int newHP = HP - damage;
            ShowFloatingText(damage, type);
            if(newHP <= 0)
            {
                HP = 0;
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

    void ShowFloatingText(int damage, DamageType type)
    {
        Color textColor = Color.white;
        switch(type)
        {
            case DamageType.Thermal:
                textColor = Color.red;
                break;
            case DamageType.Explosive:
                textColor = Color.yellow;
                break;
        }

        if (floatingTextPrefab != null)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-randomTextIntensity.x, randomTextIntensity.x), 
                Random.Range(-randomTextIntensity.y, randomTextIntensity.y), 
                Random.Range(-randomTextIntensity.z, randomTextIntensity.z));
            
            TextMesh text = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            text.transform.localPosition += new Vector3(0, 1.5f, 0);
            text.transform.localPosition += randomOffset; //random offset not working because Unity thinks it should be animating global transforms since the prefab is not a child transform
            text.text = damage.ToString();
            text.color = textColor;
        }
    }

    void Die()
    {
        OnDie.Invoke();
        //Destroy(gameObject);
    }

}

public enum DamageType
{
    Kinetic, Thermal, Explosive
}
