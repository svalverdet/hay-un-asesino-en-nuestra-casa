using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paseo : GenericState {

    #region SINGLETON
    private static Paseo INSTANCE;

    public static Paseo GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new Paseo();
        }
        return INSTANCE;
    }

    #endregion
    override public void Enter(Personaje personaje)
    {
        personaje.println("A pasear");
        personaje.GoTo(Sala.GetRandomRoomPositionExcept(personaje.GetLocation()));
    }

    override public void Execute(Personaje personaje)
    {
        if (personaje.PathComplete())
        {
            personaje.GetFSM().ChangeState(IdlePerro.GetInstance());
        }
    }
    override public void Exit(Personaje personaje)
    {
    }
	
}
