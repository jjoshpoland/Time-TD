using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TD.Towers
{
    /// <summary>
    /// Base class for a tower. By default, a tower will instantiate its own turret on startup and contains logic for selling and upgrading its turret.
    /// </summary>
    public class Tower : MonoBehaviour
    {
        [SerializeField]
        Turret Turret;

        public UnityEvent OnUpgrade;
        public UnityEvent OnSell;

        // Start is called before the first frame update
        void Start()
        {
            Turret = Instantiate(Turret, transform);
            name = Turret.name;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Sell()
        {
            //get Time bank object, refund time based on Turret.Cost;
            OnSell.Invoke();
            Destroy(gameObject);
        }

        public void Upgrade(int index)
        {
            //get Time bank object, check if player can afford to upgrade

            //if they can afford it
            GameObject oldTurret = Turret.gameObject;
            Turret newTurret = Turret.GetUpgradePath(index);
            
            if(newTurret != null)
            {
                Turret = Instantiate(newTurret, transform);
                Destroy(oldTurret.gameObject);
                name = Turret.name;
                OnUpgrade.Invoke();
            }
        }

        
    }
}

