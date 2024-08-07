using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    public class TerrainCell : MonoBehaviour
    {
        [SerializeField] Color ActiveColor = Color.green;
        [SerializeField] Color NonActiveColor = Color.white;
        [SerializeField] public  Color OccupiedColor = Color.white;
        [SerializeField] GameObject ActivatedCell;
        public Material material;
        MeshObjects meshObjects;
        [SerializeField] MeshObjects[] obj;
        internal byte mode = 0;
        public bool occupied = false;
        private void Awake()
        {
            material = ActivatedCell.GetComponent<MeshRenderer>().material;
            material.color = NonActiveColor;
        }

        public void ChangeColor(bool Activated)
        {
            if (Activated) material.color = ActiveColor;
            else material.color = NonActiveColor;
            
        }

        internal void Spawn()
        {
            material.color = OccupiedColor;
            meshObjects = Instantiate(obj[mode], transform.position + new Vector3(0 , 5 , 0), Quaternion.identity);
            occupied = true;
        }

        internal void delete()
        {
            Destroy(meshObjects.gameObject);
            occupied = false;
        }
    }
}
