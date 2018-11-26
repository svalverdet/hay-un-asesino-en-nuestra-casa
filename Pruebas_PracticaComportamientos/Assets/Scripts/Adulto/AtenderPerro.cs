using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtenderPerro : GenericState {

    private Perro perro;

    #region SINGLETON
    private static AtenderPerro INSTANCE;

    public static AtenderPerro GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new AtenderPerro();
        }
        return INSTANCE;
    }
    #endregion

    override public void Enter(Personaje personaje)
    {
        personaje.mLabel.text = "Perritooo";
        perro = (Perro) personaje.GetController().GetPersonajesByType<Perro>()[0];
        personaje.GoTo(perro.transform.position);
    }

    override public void Execute(Personaje personaje)
    {
        if (perro.GetLocation() == personaje.GetLocation())
        {
            if(perro.GetFSM().GetCurrentState() == Ladrar.GetInstance())
                perro.GetFSM().ChangeState(Paseo.GetInstance());
            personaje.GetFSM().ChangeState(EstarEnCasa.GetInstance());
        }
    }
    override public void Exit(Personaje personaje)
    {
    }
}
