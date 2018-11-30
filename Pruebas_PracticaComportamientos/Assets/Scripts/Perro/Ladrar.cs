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
		personaje.mLabel.text = "Wof Wof";
        adultos = personaje.GetController().GetPersonajesByType<Adulto>();
        BroadCast();
    }

    override public void Execute(Personaje personaje)
    {
        if(personaje.mLabel.text.Equals("Ladro")) personaje.mLabel.text = "Guau guau";
        else personaje.mLabel.text = "Ladro";
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
