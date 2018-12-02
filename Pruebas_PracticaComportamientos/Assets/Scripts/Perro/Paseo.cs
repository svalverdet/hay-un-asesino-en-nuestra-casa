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
        personaje.GoTo(Sala.GetRandomRoomPositionExcept(new Sala.Location[] { personaje.GetLocation(), Sala.Location.Bar }));
    }

    override public void Execute(Personaje personaje)
    {
        personaje.println("A pasear");
        if (personaje.PathComplete())
        {
            personaje.GetFSM().ChangeState(IdlePerro.GetInstance());
        }
    }
    override public void Exit(Personaje personaje)
    {
    }
	
}
