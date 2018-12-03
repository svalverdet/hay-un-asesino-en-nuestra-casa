using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultoHuir : GenericState {


    #region SINGLETON
    private static AdultoHuir INSTANCE;

    public static AdultoHuir GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new AdultoHuir();
        }
        return INSTANCE;
    }
    #endregion

    override public void Enter(Personaje personaje)
    {
        Asesino asesino = ((Adulto)personaje).GetAsesino();
		if(asesino!=null){
			Vector3 dist = personaje.transform.position - asesino.transform.position;
			dist = dist.normalized;
			personaje.GoTo(personaje.transform.position + dist * 5);
			personaje.println("ERA MENTIRA NO FUI MILITAR");
		}else{
			personaje.GetFSM().ChangeState(EstarEnCasa.GetInstance());
		}
    }

    override public void Execute(Personaje personaje)
    {

        if (personaje.PathComplete())
        {
            Asesino asesino = ((Adulto)personaje).GetAsesino();
			if(asesino != null){
				Vector3 dist = personaje.transform.position - asesino.transform.position;
				dist = dist.normalized;
				personaje.GoTo(personaje.transform.position + dist * 5);
			}
			else{
				personaje.GetFSM().ChangeState(EstarEnCasa.GetInstance());
			}
        }
		
    }
    override public void Exit(Personaje personaje)
    {
    }
}
