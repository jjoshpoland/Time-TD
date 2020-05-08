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
            //Debug.Log("current cell = " + currentCell + ", and current tower spot = " + currentSpot);
        }

        public void AttachTowerToMouse(Tower tower)
        {
            if(attachedTower != null)
            {
                Destroy(attachedTower);
            }

            attachedTower = Instantiate(tower, null);
        }

        void PlaceTower()
        {
            if(attachedTower != null && currentSpot != null)
            {
                attachedTower.transform.position = currentCell.transform.position;
                attachedTower.InitializeTurret();
                attachedTower = null;
            }
        }

        void HandleInput()
        {
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

            if(Input.GetMouseButton(0))
            {
                PlaceTower();
            }
        }
    }
}

