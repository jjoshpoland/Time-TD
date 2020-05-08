using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace TD.Towers
{
    public class TowerSpot : MonoBehaviour
    {
        Tower occupant;

        public bool IsOccupied
        {
            get
            {
                if (occupant == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            private set
            {
                return;
            }
        }
        
        public bool AddOccupant(Tower newOccupant)
        {
            if(occupant == null)
            {
                occupant = newOccupant;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

