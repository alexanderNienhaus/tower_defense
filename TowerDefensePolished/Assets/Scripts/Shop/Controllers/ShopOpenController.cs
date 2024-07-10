using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

/// <summary>
/// Controls opening the shop, holds a dictionary of all owned towers.
/// If a tower building spot is clicked, the class checks if a tower is present there or if the spot is empty and pushes
/// a shop open clicked event that either contains a tower if it was present at the spot or null if it was empty
/// </summary>
public class ShopOpenController : MonoBehaviour
{
    [SerializeField]
    private ShopOpenClickedEvent shopOpenClickedEvent; //Event for when the shop is opened
    [SerializeField]
    private GameObject towerBuildingSpots; //Gameobject that holds all possible building spots for towers

    private Dictionary<Vector3, TowerController> towersOwnedDictionary; //Dictionary of all towers owened, the index is the towers postition
    private TilemapCollider2D towerBuildingSpotsTilemapCollider2D; //Tilemap collider 2D of tower building spots
    private Grid grid; //Grid of tower building spots
    private float heightCorrection = 0.175f;
    
    public void OnTowerChanged(Vector3 pPosition, TowerController pTower, ShopAction pShopAction)
    {
        switch (pShopAction)
        {
            case ShopAction.Buy:
                towersOwnedDictionary.Add(pPosition, pTower);
                break;
            case ShopAction.Upgrade:
                Destroy(towersOwnedDictionary[pPosition].gameObject);
                towersOwnedDictionary.Remove(pPosition);
                towersOwnedDictionary.Add(pPosition, pTower);
                break;
            case ShopAction.Sell:
                Destroy(towersOwnedDictionary[pPosition].gameObject);
                towersOwnedDictionary.Remove(pPosition);
                break;
        }
    }

    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Gets components
    /// </summary>
    private void Initialize()
    {
        towersOwnedDictionary = new Dictionary<Vector3, TowerController>();

        grid = towerBuildingSpots.GetComponentInParent<Grid>();
        if (grid == null)
        {
            throw new System.Exception("There is no Grid component in parent.");
        }

        towerBuildingSpotsTilemapCollider2D = towerBuildingSpots.GetComponent<TilemapCollider2D>();
        if (towerBuildingSpotsTilemapCollider2D == null)
        {
            throw new System.Exception("There is no TilemapCollider2D component in towerBuildingSpots.");
        }
    }

    private void Update()
    {
        GatherMouseInput();
    }

    /// <summary>
    /// Checks when a tower building spot is clicked and raises the shop open event accordingly
    /// </summary>
    private void GatherMouseInput()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonUp(0) && towerBuildingSpotsTilemapCollider2D.OverlapPoint(mouseWorldPos)
            && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mouseGridPosVec3 = grid.GetCellCenterWorld(grid.WorldToCell(mouseWorldPos));
            mouseGridPosVec3.y += heightCorrection;
            if (towersOwnedDictionary.ContainsKey(mouseGridPosVec3))
            {
                shopOpenClickedEvent.Raise(mouseGridPosVec3, towersOwnedDictionary[mouseGridPosVec3]);
            }
            else
            {
                shopOpenClickedEvent.Raise(mouseGridPosVec3);
            }
        }
    }
}
