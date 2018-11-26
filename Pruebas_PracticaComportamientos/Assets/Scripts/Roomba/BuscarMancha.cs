using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscarMancha : GenericState {

    private List<GameObject> manchas;
    private GameObject manchaActual;

    #region SINGLETON
    private static BuscarMancha INSTANCE;

    public static BuscarMancha GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new BuscarMancha();
        }
        return INSTANCE;
    }

    #endregion
    override public void Enter(Personaje personaje)
    {
        personaje.mLabel.text = "UNA MANCHAAAAA";
        manchas = ((Roomba)personaje).GetManchas();
        manchaActual = manchas[0];
        personaje.GoTo(manchaActual.transform.position);
    }

    override public void Execute(Personaje personaje)
    {
        if (personaje.PathComplete())
        {
            ((Roomba)personaje).LimpiarMancha(manchaActual);
            manchas = ((Roomba)personaje).GetManchas();
            if (manchas.Count > 0)
            {
                manchaActual = manchas[0];
                personaje.GoTo(manchaActual.transform.position);
            }
            else personaje.GetFSM().ChangeState(IdleRoomba.GetInstance());
        }
    }
    override public void Exit(Personaje personaje)
    {
    }
}
