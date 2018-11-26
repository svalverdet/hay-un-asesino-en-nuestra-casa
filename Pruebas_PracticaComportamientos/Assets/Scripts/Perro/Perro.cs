using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Perro : Personaje {

    override protected void Start()
    {
        base.Start();
        // Variables del padre
        mLastTimeUpdated = 0.0f;
        mIntervalToUpdate = 2.0f;
        agent = GetComponent<NavMeshAgent>();
        ChangeLocation(Sala.Location.Casa);

        // Variables propias

        mFSM = new FSM(this);
        mFSM.SetCurrentState(IdlePerro.GetInstance());
        mFSM.SetPreviousState(IdlePerro.GetInstance());
        //mFSM.SetGlobalState(AncianoGlobal.GetInstance());
        mFSM.GetCurrentState().Enter(this);
    }

    override public void UpdatePersonaje()
    {

        // No se actualiza de manera constante
        if (Sala.timer - mLastTimeUpdated > mIntervalToUpdate)
        {
            mLastTimeUpdated = Sala.timer;
            mFSM.Update();
        }

        // Se muestra el texto
        Vector3 labelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        mLabel.transform.position = labelPos;
    }

}
