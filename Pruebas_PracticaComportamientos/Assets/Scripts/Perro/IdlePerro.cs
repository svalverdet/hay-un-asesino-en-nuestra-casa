using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePerro : GenericState {

    #region SINGLETON
    private static IdlePerro INSTANCE;

    public static IdlePerro GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new IdlePerro();
        }
        return INSTANCE;
    }

    #endregion
    override public void Enter(Personaje personaje)
    {
    }

    override public void Execute(Personaje personaje)
    {
        personaje.println("Me estoy lamiendo mis partes");
        if (Random.value * 100 < 10)
            personaje.GetFSM().ChangeState(Paseo.GetInstance());
    }
    override public void Exit(Personaje personaje)
    {
    }
}
