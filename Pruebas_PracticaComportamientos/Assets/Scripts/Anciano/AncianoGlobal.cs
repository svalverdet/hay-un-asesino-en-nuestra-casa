using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncianoGlobal : GenericState {

    #region SINGLETON
    private static AncianoGlobal INSTANCE;

    public static AncianoGlobal GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new AncianoGlobal();
        }
        return INSTANCE;
    }

    #endregion
    override public void Enter(Personaje personaje)
    {
    }

    override public void Execute(Personaje personaje)
    {
        if (((Anciano)personaje).TieneCaca())
            personaje.GetFSM().ChangeState(Excretar.GetInstance());
    }
    override public void Exit(Personaje personaje)
    {
    }
}
