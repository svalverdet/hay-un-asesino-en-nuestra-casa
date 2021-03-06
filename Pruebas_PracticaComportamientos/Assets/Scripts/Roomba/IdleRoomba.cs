﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRoomba : GenericState {

    #region SINGLETON
    private static IdleRoomba INSTANCE;

    public static IdleRoomba GetInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new IdleRoomba();
        }
        return INSTANCE;
    }

    #endregion
    override public void Enter(Personaje personaje)
    {
		if(personaje.GetLocation() != Sala.Location.Cocina)
			personaje.GoTo(Sala.Location.Cocina);
        personaje.println("No hay manchas cerca");
    }

    override public void Execute(Personaje personaje)
    {

    }
    override public void Exit(Personaje personaje)
    {
    }
}
