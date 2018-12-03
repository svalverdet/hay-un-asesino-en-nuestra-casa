using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morir : GenericState
{

    #region SINGLETON
    private static Morir INSTANCE;

    public static Morir GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new Morir();
        }
        return INSTANCE;
    }

    #endregion
    override public void Enter(Personaje personaje)
    {
		personaje.Mancha(1);
        personaje.RemoveLabel();
        personaje.Stop();
        personaje.GetController().PersonajeMuerto(personaje);
    }

    override public void Execute(Personaje personaje)
    {
    }
	
    override public void Exit(Personaje personaje)
    {
    }
}
