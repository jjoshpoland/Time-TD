using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TD.Towers;
using TD.Maps;
using UnityEngine.EventSystems;

namespace TD.Managers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        TowerMenu TowerMenuPrefab;
        [SerializeField]
        Canvas mainCanvas;
        Map mainMap;
        Cell currentCell;
        TowerSpot currentSpot;
        Tower attachedTower;
        Tower currentTower;

        TowerMenu currentMenu;

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
                Destroy(attachedTower.gameObject);
            }

            attachedTower = Instantiate(tower, null);
        }

        /// <summary>
        /// Places a tower on the Current Spot if the Current Spot is not occupied
        /// </summary>
        bool PlaceTower()
        {
            if(attachedTower != null && currentSpot != null)
            {
                if(currentSpot.AddOccupant(attachedTower))
                {
                    attachedTower.transform.position = currentCell.transform.position;
                    attachedTower.InitializeTurret(ClockManager.instance.TimeScale);
                    ClockManager.instance.RemoveTime(attachedTower.Cost);
                    attachedTower = null;
                    return true;
                }
                
            }
            return false;
        }

        void ActivateTowerMenu()
        {
            
            //if placing a tower, a menu should not be activated
            if(attachedTower != null)
            {
                return;
            }
            //if another menu is active, don't just make more
            if(currentMenu != null)
            {
                return;
            }
            
            //if there is something in that cell
            if (currentCell.occupant != null)
            {
                //and that something is a tower spot
                currentSpot = currentCell.occupant.GetComponent<TowerSpot>();
                if(currentSpot != null)
                {
                    //and the tower spot contains a tower
                    currentTower = currentSpot.Occupant;
                    if (currentTower != null)
                    {
                        //instantiate tower menu object on canvas at tower point
                        //hook tower up to menu buttons
                        //pause game

                        currentMenu = Instantiate(TowerMenuPrefab, mainCanvas.transform);
                        currentMenu.GetComponent<RectTransform>().anchoredPosition = currentCell.transform.position;
                        currentMenu.Init(currentTower);
                    }
                }
                
            }
        }

        void DeactivateTowerMenu()
        {
            //destroy tower menu object
            currentTower = null;
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
                    //if a tower is attached to cursor, place it
                    if(attachedTower != null)
                    {
                        attachedTower.transform.position = currentCell.transform.position;

                    }
                    if (currentCell.occupant != null)
                    {
                        currentSpot = currentCell.occupant.GetComponent<TowerSpot>();
                    }
                }
            }

            if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && currentMenu != null)
            {
                Destroy(currentMenu.gameObject);
                currentMenu = null;
            }

            //Place the currently attached tower if the user clicks on the spot
            if(Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if(!PlaceTower())
                {
                    ActivateTowerMenu();
                }
                
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

