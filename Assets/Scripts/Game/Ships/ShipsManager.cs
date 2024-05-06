using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipsManager : ASingleton<ShipsManager>
{
    [SerializeField] public AllyShip selectedAllyShip = null;
    [SerializeField] public EnemyShip selectedEnemyShip = null;

    [HideInInspector] public List<AllyShip> allyShips;
    [HideInInspector] public List<EnemyShip> enemyShips;
    void Awake()
    {
        base.Awake();
    }

    public void SelectShip(Ship ship)
    {
        if (ship is AllyShip)
        {
            selectedAllyShip = (AllyShip)ship;
        }
        else
        {
            selectedEnemyShip = (EnemyShip)ship;
            if (selectedAllyShip != null)
            {
                selectedAllyShip.Attack(selectedEnemyShip.gameObject);
            }
        }
    }

    public void MoveShip(Transform transform)
    {
        if (selectedAllyShip != null)
        {
            selectedAllyShip.MoveToNavPoint(transform);
        }
    }
    
    public void AllShipsAttack()
    {
        if (ShipsManager.Instance.selectedEnemyShip != null)
        {
            foreach (var ship in ShipsManager.Instance.allyShips)
            {
                ship.Attack(ShipsManager.Instance.selectedEnemyShip.gameObject);
            }
        }
    }
    
}
