using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Anciano : Personaje {

    private int mVejiga;
    private int LIMITE_VEJIGA = 14;

    private GameObject mCollSeat1;
    private GameObject mCollSeat2;

    private ArrayList seats;

    void Start()
    {
        // Variables del padre
        mLastTimeUpdated = 0.0f;
        mIntervalToUpdate = 2.0f;
        agent = GetComponent<NavMeshAgent>();
        ChangeLocation(Sala.Location.Casa);

        // Variables propias
        mVejiga = 2;
        mCollSeat1 = GameObject.Find("Seat1");
        mCollSeat2 = GameObject.Find("Seat2");
        seats = new ArrayList();
        seats.Add(mCollSeat1);
        seats.Add(mCollSeat2);

        mFSM = new FSM(this);
        mFSM.SetCurrentState(Deambular.GetInstance());
        mFSM.ChangeState(Deambular.GetInstance());
        mFSM.SetGlobalState(GlobalState.GetInstance());
    }

    override public void UpdatePersonaje()
    {

        // No se actualiza de manera constante
        if (Sala.timer - mLastTimeUpdated > mIntervalToUpdate)
        {
            mLastTimeUpdated = Sala.timer;
            mFSM.Update();
            mVejiga += 2;
        }

        // Se muestra el texto
        Vector3 labelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        mLabel.transform.position = labelPos;
    }


    // Métodos
    
    public int GetVejiga() { return mVejiga; }
    
    public bool TienePis() { return mVejiga >= LIMITE_VEJIGA; }

    public ArrayList GetSeats() { return seats; }
}
