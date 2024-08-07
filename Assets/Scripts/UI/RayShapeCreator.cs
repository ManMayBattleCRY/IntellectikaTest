using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    public class RayShapeCreator : MonoBehaviour
    {
        bool ActionMenuOpened = false;
        public bool EditorMenuOpened = false;
        bool subscribed = false;
        TerrainCell HitObject;
        [SerializeField] LayerMask layer;
        [SerializeField]Camera _camera;
        public GameObject CreatingPanel;
        public GameObject ChangePanel;
        Vector2 ClickPosition;
        [HideInInspector] public byte mode = 0;
        TerrainCell CurrentCell;
        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 250, layer) && !ActionMenuOpened && !EditorMenuOpened)
            {
                if (subscribed)
                {
                    if (hit.collider.gameObject.GetComponent<TerrainCell>() == HitObject)
                    {
                        HitObject.ChangeColor(true);
                        MouseInput();

                    }
                    else
                    {
                        if (HitObject.occupied) HitObject.material.color = HitObject.OccupiedColor;
                        else HitObject.ChangeColor(false);
                        HitObject = hit.collider.gameObject.GetComponent<TerrainCell>();
                        if (Vector2.Distance(Input.mousePosition, new Vector2(ClickPosition.x + 150, ClickPosition.y - 75)) > 250) UnActive();
                    }
                }
                else
                {
                    if(hit.collider.gameObject.GetComponent<TerrainCell>() != null)
                    subscribed = true;
                    HitObject = hit.collider.gameObject.GetComponent<TerrainCell>();
                }
            }
            if((ActionMenuOpened || EditorMenuOpened) && Vector2.Distance(Input.mousePosition, new Vector2(ClickPosition.x + 150, ClickPosition.y - 75)) > 250) UnActive();




        }

        void MouseInput()
        {
            if (Input.GetButtonDown("Fire1") && !ActionMenuOpened && !EditorMenuOpened)
            {
                if (!HitObject.occupied)
                {
                    CurrentCell = HitObject;
                    ClickPosition = Input.mousePosition;
                    ActionMenuOpened = true;
                    CreatingPanel.GetComponent<RectTransform>().position = new Vector2(Input.mousePosition.x + 150, Input.mousePosition.y - 75);
                    CreatingPanel.SetActive(true);
                }
                else
                {
                    CurrentCell = HitObject;
                    ClickPosition = Input.mousePosition;
                    ActionMenuOpened = true;
                    ChangePanel.GetComponent<RectTransform>().position = new Vector2(Input.mousePosition.x + 150, Input.mousePosition.y - 75);
                    ChangePanel.SetActive(true);
                }
            }
        }
        void UnActive()
        {
            ActionMenuOpened = false;
            CreatingPanel.SetActive(false);
            ChangePanel.SetActive(false);
        }

        public void Add()
        {
            CurrentCell.mode = mode;
             CurrentCell.Spawn();

                    
            
        }

        public void Change()
        {

        }

        public void Delete()
        {
            HitObject.delete();
        }
    }
}
