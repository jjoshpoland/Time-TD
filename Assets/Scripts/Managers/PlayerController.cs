using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TD.Towers;
using TD.Maps;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace TD.Managers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        TowerMenu TowerMenuPrefab;
        [SerializeField]
        Canvas mainCanvas;
        [SerializeField]
        GameObject PauseMenu;
        Map mainMap;
        Cell currentCell;
        TowerSpot currentSpot;
        Tower attachedTower;
        Tower currentTower;

        TowerMenu currentMenu;

        bool isPaused;

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

        public void ResumeGame()
        {
            
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            isPaused = false;
        }

        public void PauseGame()
        {
            
            Time.timeScale = 0;
            isPaused = true;
        }

        public void ActivatePauseMenu()
        {
            PauseGame();
            PauseMenu.SetActive(true);
        }

        public void Exit()
        {
            GameManager.instance.ExitToMenu();
        }

        public void Restart()
        {
            GameManager.instance.LoadLevel(SceneManager.GetActiveScene().name);
        }

        public void SetSpeed(float scale)
        {
            Time.timeScale = scale;
        }

        /// <summary>
        /// Places a tower on the Current Spot if the Current Spot is not occupied
        /// </summary>
        bool PlaceTower()
        {
            if(attachedTower != null && currentSpot != null)
            {
                if(currentSpot.AddOccupant(attachedTower) && attachedTower.Cost <= ClockManager.instance.RemainingTime)
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
            //pause game on space
            if(Input.GetKeyDown(KeyCode.Space))
            {
                
                if(isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }

            //bring up pause menu on escape
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                if (attachedTower != null)
                {
                    Destroy(attachedTower.gameObject);
                }
                else if(currentMenu.gameObject.activeSelf)
                {
                    currentMenu.gameObject.SetActive(false);
                }
                else if (PauseMenu.activeSelf)
                {
                    ResumeGame();
                    PauseMenu.SetActive(false);
                }
                else
                {
                    ActivatePauseMenu();
                }
                
            }

            //dont allow other inputs if pause menu is up
            if(PauseMenu != null && PauseMenu.activeSelf)
            {
                return;
            }

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

            //right click cancels tower placement
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

