using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TD.Map
{
    public class Cell : MonoBehaviour
    {
        [SerializeField]
        Sprite Sprite;
        Vector2Int coords;
        
        public GameObject occupant;

        public bool IsOccupied
        {
            get
            {
                return occupant != null;
            }
            private set
            {
                IsOccupied = value;
            }
        }

        public Vector2Int Coords { get => coords; set => coords = value; }

        // Start is called before the first frame update
        void Start()
        {

        }

        
    }

}

