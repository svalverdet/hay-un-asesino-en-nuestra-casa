using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Anciano : Personaje {

    private int mCaca;
    private int LIMITE_CACA = 2;

    private GameObject mCollSeat1;
    private GameObject mCollSeat2;

    public GameObject manchaCaca;

    private ArrayList seats;

    override protected void Start()
    {
		
        base.Start();
		
        // Variables del padre
        mLastTimeUpdated = 0.0f;
        mIntervalToUpdate = 2.0f;
        agent = GetComponent<NavMeshAgent>();
        ChangeLocation(Sala.Location.Casa);

        // Variables propias
        mCaca = 0;
        mCollSeat1 = GameObject.Find("Seat1");
        mCollSeat2 = GameObject.Find("Seat2");
        seats = new ArrayList();
        seats.Add(mCollSeat1);
        seats.Add(mCollSeat2);

        mFSM = new FSM(this);
        mFSM.SetCurrentState(Deambular.GetInstance());
        mFSM.SetPreviousState(Deambular.GetInstance());
        mFSM.SetGlobalState(AncianoGlobal.GetInstance());
        mFSM.GetCurrentState().Enter(this);
    }

    override public void UpdatePersonaje()
    {

        // No se actualiza de manera constante
        if (Sala.timer - mLastTimeUpdated > mIntervalToUpdate)
        {
            mLastTimeUpdated = Sala.timer;
            mFSM.Update();
            mCaca += 1;
        }

        // Se muestra el texto
        Vector3 labelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        mLabel.transform.position = labelPos;
    }


    // Métodos
    
    public int GetCaca() { return mCaca; }
    
    public bool TieneCaca() { return mCaca >= LIMITE_CACA; }

    public void Mancha()
    {
        GameObject mancha = Instantiate(manchaCaca, new Vector3(transform.position.x, transform.position.y - GetComponent<Renderer>().bounds.size.y / 2, transform.position.z), Quaternion.identity);
        mCaca = 0;
        Roomba roomba = (Roomba) controller.GetPersonajesByType<Roomba>()[0];
        roomba.AddMancha(mancha);
        if (roomba.GetFSM().GetCurrentState() == IdleRoomba.GetInstance())
            roomba.GetFSM().ChangeState(BuscarMancha.GetInstance());
    }

    public ArrayList GetSeats() { return seats; }
}
