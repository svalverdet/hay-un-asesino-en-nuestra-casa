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
		if(personaje.GetSmartObjectInUse() != null){
			personaje.GoTo(personaje.GetSmartObjectInUse().transform.position);
		}else{
			personaje.GetFSM().ChangeState(Deambular.GetInstance());
		}
		
    }

    override public void Execute(Personaje personaje)
    {
		
		if (personaje.PathComplete())
        {
            personaje.println("Me siento");
			personaje.GetSmartObjectInUse().Use(personaje);
			
			// Si ha terminado de consumir el objeto, cambia de estado
			if(personaje.GetSmartObjectInUse().checkUseFinished(personaje)){
				personaje.GetSmartObjectInUse().inUse = false;
				personaje.SetSmartObjectInUse(null);
				personaje.GetFSM().ChangeState(NinoIdle.GetInstance());
			}
        }
    }
	
    override public void Exit(Personaje personaje)
    {
		if(personaje.GetSmartObjectInUse() != null){
			personaje.GetSmartObjectInUse().inUse = false;
			personaje.SetSmartObjectInUse(null);
		}
    }
	
}
