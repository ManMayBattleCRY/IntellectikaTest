using Intellectika.BootstrapSpace;
using Intellectika.Interfaces;
using UnityEngine;

namespace Intellectika.Terrain
{
    public class TerrainCellStorage : MonoBehaviour, ILocatable // хранит в себе массив €чеек
    {

        TerrainCell[,] cells;
        private string _name = "CellStorageInstance";
        public string Name => _name;

        bool StorageInited = false;


        public static TerrainCellStorage CellStorageInstance { get; private set; }



        private void Awake()
        {
            if (CellStorageInstance == null)
            {
                CellStorageInstance = this;
                LocatorReference.Locator.Add(Name, this);
                
                DontDestroyOnLoad(this);
            }
            else Destroy(gameObject);
        }

        public void InitStorage(int x, int y)
        {
            if (!StorageInited) 
            {
                cells = new TerrainCell[x, y];
                StorageInited = true;
            }

        }

        public void Set(TerrainCell _Cell, int x, int y)
        {
            if (StorageInited)
            {
                cells[x, y] = _Cell;
            }
        }

        public GameObject Return()
        {
            return gameObject;
        }

        public void OnDestroy()
        {
            LocatorReference.Locator.Remove(Name);
        }
    }
}
