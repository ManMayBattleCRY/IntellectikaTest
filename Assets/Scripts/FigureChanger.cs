using UnityEngine;

namespace Intellectika
{
    public class FigureChanger : MonoBehaviour // меняет мод создаваемой фигуры
    {
        RayShapeCreator creator;

        private void Awake()
        {
            creator = GetComponent<RayShapeCreator>();
        }

        public void ChangeSphere()
        {
            creator.mode = 0;
        }

        public void ChangePrism()
        {
            creator.mode = 1;
        }

        public void ChangeParal()
        {
            creator.mode = 2;
        }

        public void ChangeCapsule()
        {
            creator.mode = 3;
        }

        public void ChangeMenuStateO()
        {
            creator.EditorMenuOpened = true;
        }

        public void ChangeMenuStateC()
        {
            creator.EditorMenuOpened = false;
        }

    }
}
