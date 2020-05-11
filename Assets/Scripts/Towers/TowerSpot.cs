using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace TD.Towers
{
    /// <summary>
    /// Attach this component to a Game Object to allow a tower to be placed there
    /// </summary>
    public class TowerSpot : MonoBehaviour
    {
        Tower occupant;

        public Tower Occupant
        {
            get
            {
                return occupant;
            }
            private set
            {
                occupant = value;
            }
        }

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
        
        /// <summary>
        /// Returns true if a tower was succesfully placed on this spot
        /// </summary>
        /// <param name="newOccupant"></param>
        /// <returns></returns>
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

