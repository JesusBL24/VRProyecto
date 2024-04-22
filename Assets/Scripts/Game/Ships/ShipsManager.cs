using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipsManager : ASingleton<ShipsManager>
{
    [SerializeField] public AllyShip SelectedAllyShip = null;
    [SerializeField] public EnemyShip SelectedEnemyShip = null;
    [SerializeField] private TextMeshProUGUI allyText;
    [SerializeField] private TextMeshProUGUI enemyText;

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
            SelectedAllyShip = (AllyShip)ship;
            allyText.text = ship.GetShipType();
        }
        else
        {
            SelectedEnemyShip = (EnemyShip)ship;
            enemyText.text = ship.GetShipType() + ship.GetHeatlh();
            if (SelectedAllyShip != null)
            {
                SelectedAllyShip.Attack(SelectedEnemyShip.gameObject);
            }
        }
    }
    
    
}
