using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    void Start()
    {
        faction = 1;
        ShipsManager.Instance.enemyShips.Add(this);
        base.Start();
    }

    protected void Update()
    {
        if (ShipsManager.Instance.allyShips.Count > 0)
        {
            _attackTarget = calculateTarget().gameObject;
        }
        base.Update();
    }

    private AllyShip calculateTarget()
    {
        float min = 1000;
        AllyShip shipToAttack = null;
        
        foreach (var ship in ShipsManager.Instance.allyShips)
        {
            var distance = Vector3.Distance(ship.transform.position, transform.position);
            if ( distance < min)
            {
                shipToAttack = ship;
                min = distance;
            }
        }
            
        

        return shipToAttack;
    }
    
    protected override void DestroyShip()
    {
        Destroy(this.gameObject);
        ShipsManager.Instance.enemyShips.Remove(this);
    }
    
}
