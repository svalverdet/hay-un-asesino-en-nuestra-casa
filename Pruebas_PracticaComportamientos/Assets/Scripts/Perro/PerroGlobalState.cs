using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerroGlobalState : GenericState {


    #region SINGLETON
    private static PerroGlobalState INSTANCE;

    public static PerroGlobalState GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new PerroGlobalState();
        }
        return INSTANCE;
    }

    #endregion
    override public void Enter(Personaje personaje)
    {
    }

    override public void Execute(Personaje personaje)
    {
        if (personaje.GetAsesino() != null 
            && personaje.GetFSM().GetCurrentState() != Ladrar.GetInstance()) personaje.GetFSM().ChangeState(Ladrar.GetInstance());
    }
    override public void Exit(Personaje personaje)
    {
    }
}
