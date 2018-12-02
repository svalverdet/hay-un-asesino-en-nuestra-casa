using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladrar : GenericState {

    private List<Personaje> adultos;

    #region SINGLETON
    private static Ladrar INSTANCE;

    public static Ladrar GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new Ladrar();
        }
        return INSTANCE;
    }

    #endregion
    override public void Enter(Personaje personaje)
    {
        personaje.println("Wof wof ven adulto");
        adultos = personaje.GetController().GetPersonajesByType<Adulto>();
        personaje.Stop();
        BroadCast();
    }

    override public void Execute(Personaje personaje)
    {
        personaje.println("Wof wof ven adulto");
        BroadCast();
    }
	
    override public void Exit(Personaje personaje)
    {
    }
	
	
    private void BroadCast()
    {
        if (adultos.Count > 0)
        {
            foreach (Adulto a in adultos)
            {
                if (a.GetFSM().GetCurrentState() != EstarEnElWC.GetInstance())
                    a.GetFSM().ChangeState(AtenderPerro.GetInstance());
            }
        }
    }
}
