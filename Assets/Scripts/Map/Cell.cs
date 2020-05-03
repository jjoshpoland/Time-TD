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

        public Vector2Int Coords { get => coords; set => coords = value; }

        // Start is called before the first frame update
        void Start()
        {

        }

        
    }

}

