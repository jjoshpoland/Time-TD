using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TD.Towers;
using TD.Maps;

namespace TD.Managers
{
    public class PlayerController : MonoBehaviour
    {
        Map mainMap;
        Cell currentCell;
        TowerSpot currentSpot;
        Tower attachedTower;

        private void Awake()
        {
            mainMap = FindObjectOfType<Map>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }

        /// <summary>
        /// Instantiates the given tower at the position
        /// </summary>
        /// <param name="tower"></param>
        public void AttachTowerToMouse(Tower tower)
        {
            if(attachedTower != null)
            {
                Destroy(attachedTower);
            }

            attachedTower = Instantiate(tower, null);
        }

        /// <summary>
        /// Places a tower on the Current Spot if the Current Spot is not occupied
        /// </summary>
        void PlaceTower()
        {
            if(attachedTower != null && currentSpot != null)
            {
                if(currentSpot.AddOccupant(attachedTower))
                {
                    attachedTower.transform.position = currentCell.transform.position;
                    attachedTower.InitializeTurret();
                    attachedTower = null;
                }
                
            }
        }

        /// <summary>
        /// Generic function for handling responses to input events
        /// </summary>
        void HandleInput()
        {
            //Check for the current cell and force the currently attached tower to that cell
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit))
            {
                //Debug.Log( mainMap.GetCellAtPosition(hit.point).occupant);
                currentCell = mainMap.GetCellAtPosition(hit.point);
                if(currentCell != null)
                {
                    if(attachedTower != null)
                    {
                        attachedTower.transform.position = currentCell.transform.position;
                    }
                    if(currentCell.occupant != null)
                    {
                        currentSpot = currentCell.occupant.GetComponent<TowerSpot>();
                    }
                }
            }

            //Place the currently attached tower if the user clicks on the spot
            if(Input.GetMouseButton(0))
            {
                PlaceTower();
            }

            if(Input.GetMouseButton(1))
            {
                if(attachedTower != null)
                {
                    Destroy(attachedTower.gameObject);
                }
            }
        }
    }
}

