using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huir : GenericState {
    #region SINGLETON
    private static Huir INSTANCE;

    public static Huir GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new Huir();
        }
        return INSTANCE;
    }
    #endregion

    override public void Enter(Personaje personaje)
    {
        Asesino asesino = ((Nino)personaje).GetAsesino();
        Vector3 dist = personaje.transform.position - asesino.transform.position;
        dist = dist.normalized;
        personaje.GoTo(personaje.transform.position + dist * 5);
        personaje.println("NO ME COGERAS CON VIDAAA");
    }
    override public void Execute(Personaje personaje)
    {
        if (personaje.PathComplete()) {
            Asesino asesino = ((Nino)personaje).GetAsesino();
            Vector3 dist = personaje.transform.position - asesino.transform.position;
            dist = dist.normalized;
            personaje.GoTo(personaje.transform.position + dist * 5);
        }
    }
    override public void Exit(Personaje personaje)
    {
        personaje.println("weno po me tranquilizo");
    }
}
