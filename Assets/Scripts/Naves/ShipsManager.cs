using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipsManager : ASingleton<ShipsManager>
{
    [SerializeField] private Ship SelectedAllyShip = null;
    [SerializeField] private Ship SelectedEnemyShip = null;
    [SerializeField] private TextMeshProUGUI allyText;
    [SerializeField] private TextMeshProUGUI enemyText;
    void Awake()
    {
        base.Awake();
    }

    public void SeleccionarNave(Ship ship)
    {
        if (ship.faction == 0)
        {
            SelectedAllyShip = ship;
            allyText.text = ship.GetShipType();
        }
        else
        {
            SelectedEnemyShip = ship;
            enemyText.text = ship.GetShipType();
            if (SelectedAllyShip != null)
            {
                SelectedAllyShip.Attack(SelectedEnemyShip.gameObject.GetNamedChild("Body"));
            }
        }
    }

    public void MoveShip(Vector3 newPosition)
    {
        if (SelectedAllyShip != null)
        {
            SelectedAllyShip.Move(newPosition);
        }
    }
    
}
