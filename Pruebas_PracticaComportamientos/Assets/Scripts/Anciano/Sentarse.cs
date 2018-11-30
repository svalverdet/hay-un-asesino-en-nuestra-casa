using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentarse : GenericState {

    #region SINGLETON
    private static Sentarse INSTANCE;

    public static Sentarse GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new Sentarse();
        }
        return INSTANCE;
    }

    #endregion
	
    override public void Enter(Personaje personaje)
    {
        personaje.println("Voy a sentarme");
        personaje.GoTo(Sala.GetSeatLocation(personaje));
    }

    override public void Execute(Personaje personaje)
    {
		Anciano a = (Anciano) personaje;
        if (personaje.PathComplete() && !a.IsSit())
        {
            personaje.println("Me siento");
            a.SetSit(true);
        }
		else if (a.IsSit() && Random.value * 100 < 70)
        {
            personaje.GetFSM().ChangeState(Deambular.GetInstance());
        }
    }
	
    override public void Exit(Personaje personaje)
    {
    }
	
}
