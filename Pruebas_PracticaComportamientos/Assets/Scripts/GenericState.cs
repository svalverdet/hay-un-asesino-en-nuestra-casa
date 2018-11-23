using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericState{
	
	// Se ejecuta cuando se entra al estado
	public abstract void Enter(Personaje personaje);
	public abstract void Execute(Personaje personaje);
	public abstract void Exit(Personaje personaje);
	
}
