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

    [HideInInspector] public List<AllyShip> _allyShips;
    [HideInInspector] public List<EnemyShip> _enemyShips;
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
    
    
}
