using System.Collections;
using System.Collections.Generic;
using TD.Managers;
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

        public float Cost
        {
            get
            {
                return Turret.Cost;
            }
            private set
            {
                return;
            }
        }

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

        public Turret GetUpgradePath(int index)
        {
            if(Turret.upgradePaths.Count > 0 && Turret.upgradePaths.Count > index)
            {
                return Turret.upgradePaths[index];
            }
            else
            {
                Debug.LogWarning("Cannot get an upgrade path for " + name + " or the index " + index + "is out of range");
                return null;
            }
        }

        public void InitializeTurret(float scale)
        {
            Turret.scale = scale;
            Turret.Initialized = true;
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
                InitializeTurret(oldTurret.GetComponent<Turret>().scale);
                Destroy(oldTurret.gameObject);
                name = Turret.name;
                OnUpgrade.Invoke();
            }
        }

        
    }
}

