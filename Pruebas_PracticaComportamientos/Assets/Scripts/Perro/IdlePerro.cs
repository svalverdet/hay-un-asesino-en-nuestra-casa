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
        personaje.mLabel.text = "Soy un perro";
    }

    override public void Execute(Personaje personaje)
    {
        personaje.mLabel.text = "Me estoy lamiendo mis partes";
        if(personaje.GetLocation() == Sala.Location.Bar)
            personaje.GetFSM().ChangeState(Ladrar.GetInstance());
        else if (Random.value * 100 < 50)
            personaje.GetFSM().ChangeState(Paseo.GetInstance());
    }
    override public void Exit(Personaje personaje)
    {
    }
}
