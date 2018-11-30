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
		Anciano a = (Anciano)personaje;
        if (a.TienePis())
		{
			a.EfectosDelWC();
			personaje.mLabel.text = "Me cagao encima";
			personaje.Mancha();
            //personaje.GetFSM().ChangeState(Excretar.GetInstance());
		}
    }
    override public void Exit(Personaje personaje)
    {
    }
}
