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
        personaje.println("Que le pasa al chucho ahora");
		
		Adulto a = (Adulto) personaje;
		Perro perro = (Perro) personaje.GetController().GetPersonajesByType<Perro>()[0];
		a.SetPerroAtencion(perro);
        personaje.GoTo(perro.transform.position);
    }

    override public void Execute(Personaje personaje)
    {
		Adulto a = (Adulto) personaje;
		Perro perro = a.GetPerroAtencion();
        if (a.GetVeoPerro())
        {
            if (perro.GetFSM().GetCurrentState() == Ladrar.GetInstance())
            {
                a.println("CALLATE");
                perro.GetFSM().ChangeState(Paseo.GetInstance());
                perro.SetAsesino(null);
            }
            personaje.GetFSM().ChangeState(EstarEnCasa.GetInstance());
        }
    }
	
    override public void Exit(Personaje personaje)
    {
    }
}
