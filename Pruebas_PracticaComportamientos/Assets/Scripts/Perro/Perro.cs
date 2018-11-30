using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Perro : Personaje {

    override protected void Start()
    {
		// Variables del padre
        base.Start();
        
        agent = GetComponent<NavMeshAgent>();
        ChangeLocation(Sala.Location.Entrada);

        mFSM = new FSM(this);
        mFSM.SetCurrentState(IdlePerro.GetInstance());
        mFSM.SetPreviousState(IdlePerro.GetInstance());
        mFSM.GetCurrentState().Enter(this);
		
		// Variables propias
		// ...
    }

    override public void UpdatePersonaje()
    {
		mFSM.Update();
    }

}
