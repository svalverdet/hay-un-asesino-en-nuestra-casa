using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpear : GenericState {

    #region SINGLETON
    private static Golpear INSTANCE;

    public static Golpear GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new Golpear();
        }
        return INSTANCE;
    }
    #endregion

    override public void Enter(Personaje personaje)
    {
        Adulto a = (Adulto)personaje;
        Personaje asesino = a.GetAsesino();
        personaje.println("Me cago en dios " + asesino.GetName() + " QUE FUI MILITAR");
        personaje.GoTo(asesino.transform.position);
        a.transform.LookAt(new Vector3(asesino.transform.position.x, a.transform.position.y, asesino.transform.position.z));
    }

    override public void Execute(Personaje personaje)
    {

        Adulto a = (Adulto)personaje;
        Asesino asesino = a.GetAsesino();
		if(asesino != null){
			a.GoTo(asesino.transform.position);

			a.transform.LookAt(new Vector3(asesino.transform.position.x, a.transform.position.y, asesino.transform.position.z));
			Vector3 distancia = asesino.transform.position - a.transform.position;

			if (a.GetHealth() < 10)
			{
				personaje.GetFSM().ChangeState(AdultoHuir.GetInstance());
			}else if (distancia.sqrMagnitude < 5)
			{
				if (asesino.GetHealth() <= 0)
				{
					asesino.GetFSM().ChangeState(Morir.GetInstance());
					a.SetAsesino(null);
					a.GetFSM().ChangeState(EstarEnCasa.GetInstance());
				}
				else
				{
					asesino.Damaged();
					asesino.SetVictima(a);
					asesino.TieneVictima(true);
				}
			}
			
			
		}else{
			a.GetFSM().ChangeState(EstarEnCasa.GetInstance());
		}
    }
    override public void Exit(Personaje personaje)
    {
    }
}
