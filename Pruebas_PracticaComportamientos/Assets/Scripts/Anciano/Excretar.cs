using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excretar : GenericState
{

    #region SINGLETON
    private static Excretar INSTANCE;

    public static Excretar GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new Excretar();
        }
        return INSTANCE;
    }

    #endregion
    override public void Enter(Personaje personaje)
    {
        personaje.mLabel.text = "Ya no hacen pañales como los de antes";
        personaje.Stop();
    }

    override public void Execute(Personaje personaje)
    {
        ((Anciano)personaje).Mancha();
        personaje.GetFSM().RevertToPreviousState();
    }
    override public void Exit(Personaje personaje)
    {
    }
}
