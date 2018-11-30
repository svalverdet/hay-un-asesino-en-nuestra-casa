using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtenderPerro : GenericState {

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
        personaje.println("Perritooo");
		
		Adulto a = (Adulto) personaje;
		Perro perro = (Perro) personaje.GetController().GetPersonajesByType<Perro>()[0];
		a.SetPerroAtencion(perro);
        personaje.GoTo(perro.transform.position);
    }

    override public void Execute(Personaje personaje)
    {
		Adulto a = (Adulto) personaje;
		Perro perro = a.GetPerroAtencion();
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
