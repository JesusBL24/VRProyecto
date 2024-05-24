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
            calculateTarget();
        }
        if (GameManager.Instance.State == GameManager.GameState.Play)
        {
            base.Update();
        }
    }

    protected void FixedUpdate()
    {
        if (GameManager.Instance.State == GameManager.GameState.Play)
        {
            base.FixedUpdate();
        }
    }

    private void calculateTarget()
    {
        float min = 1000;
        AllyShip shipToAttack = null;
        
        foreach (var ship in ShipsManager.Instance.allyShips)
        {
            if (ship != null)
            {
                var distance = Vector3.Distance(ship.transform.position, transform.position);
                if ( distance < min)
                {
                    shipToAttack = ship;
                    min = distance;
                }
            }
        }


        if (shipToAttack != null)
        {
            _attackTarget = shipToAttack.gameObject;
            if (min <= _attackRange)
            {
                Fire();
            }
        }
    }
    
    protected override void DestroyShip()
    {
        GameManager.OnGameStateChanged -= GrabActivation;
        ShipsManager.Instance.enemyShips.Remove(this);
        Destroy(this.gameObject);
    }
    
}
