using UnityEngine;
using Locator = Intellectika.BootstrapSpace.LocatorReference;
using cons = Intellectika.Consts.Constants;
using Intellectika.BootstrapSpace;

namespace Intellectika.Terrain
{
    public class CellTerrainGenerator : MonoBehaviour
    {
        [SerializeField] Vector2Int Size = new Vector2Int(5,5);
        [SerializeField]TerrainCell[] cell;
        Transform CellParent;
        TerrainCellStorage cellStorage;

        private void Awake()
        {
            CellParent = transform;
        }

        // Start is called before the first frame update
        void Start()
        {
            cellStorage = LocatorReference.Locator.Get("CellStorageInstance").GetComponent<TerrainCellStorage>();
            cellStorage.InitStorage(Size.x, Size.y);
            GenerateTerrain();
        }




        void GenerateTerrain()
        {
            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
                {
                    TerrainCell _cell = Instantiate(cell[Random.Range(0, cell.Length)], new Vector3(x * cons.CellSizeX * 1.5f + cons.CellSizeX * 0.75f * (y%2), 
                                                                                                    Random.Range(-0.600f, 0.600f),
                                                                                                    y * cons.CellSizeY / 2),
                                                                                                    Quaternion.identity, CellParent); 
                    cellStorage.Set(_cell, x, y);
                }
            }
        }
    }

}