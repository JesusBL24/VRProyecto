using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Manger del juego, patron State para organizar estados del juego
public class GameManager : ASingleton<GameManager>
{
    //Stado del juegp
    public GameState State;

    //Llama a las acciones al cambiar estado
    public static event Action<GameState> OnGameStateChanged;
    
    
    private void Awake()
    {
        base.Awake();
        ChangeState(GameState.SetUnits);
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        //switch de estados
        switch (newState) { 
            case GameState.SetUnits:
                break;
            case GameState.Play:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState,null);      
        }
        //Si ha cambiado el estado llama al nuevo estado
        OnGameStateChanged?.Invoke(newState);
    }

    //Enumeracion de estados
    public enum GameState
    {
        SetUnits = 0,
        Play = 1
    }
}
