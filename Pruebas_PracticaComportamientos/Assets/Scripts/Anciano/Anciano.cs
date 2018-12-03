using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Anciano : Personaje {

	private bool sit;
    private string[] frasesAsesino;

    override protected void Start()
    {
		// Variables del padre
        base.Start();
		
        agent = GetComponent<NavMeshAgent>();
        ChangeLocation(Sala.Location.Entrada);

		mVejiga = 0;
		ALERTA_VEJIGA = 50;
		ALERTA_ABURRIMIENTO = 50;
		
		mEstadoInicial = Deambular.GetInstance();
        mFSM = new FSM(this);
        mFSM.SetCurrentState(mEstadoInicial);
        mFSM.SetPreviousState(mEstadoInicial);
        mFSM.SetGlobalState(AncianoGlobal.GetInstance());
        mFSM.GetCurrentState().Enter(this);
        
		// Variables propias
		sit = false;
        List<Personaje> asesinos = GetController().GetPersonajesByType<Asesino>();
        mPersonajesDeInteres.AddRange(asesinos);
        frasesAsesino = new string[] { "Me he cagado, pero no ha sido por tu culpa",
                                        "Por fin alguien que se preocupa por mis sentimientos",
                                        "Llevo 20 años esperando este momento",
                                        "Disfruta mientras eres joven, tú que puedes",
                                        "Yo antes solía ser como tú, ¿sabes?",
                                        "Estoy calmado",
                                        "Gracias",
                                        "No sabes hacerlo mejor?",
                                        "Venga, no tengo todo el dia",
                                        "Hacía mucho que no tenía esta sensación",
										"Esto no me lo cubre la seguridad social",
										"Hasta mañana, si Dios quiere",
										"Dios me lleve pronto"
                                    };
    }

    override public void UpdatePersonaje()
    {
		mFSM.Update();
		IncrementarVejiga();
		IncrementarAburrimiento();
        
    }

    override public void UpdatePercepcion()
    {
        base.UpdatePercepcion();

        if (personajeVisto != null && personajeVisto.GetComponent<Asesino>() != null)
        {
            asesino = (Asesino)personajeVisto;
        }
    }

    // Métodos

    public void EfectosDelWC(){ this.mVejiga = 0;}
	public void IncrementarVejiga(){ mVejiga+=1;}
	public void IncrementarAburrimiento(){ mAburrimiento+=1;}

    public string GetFrase() { return frasesAsesino[Random.Range(0, frasesAsesino.Length - 1)]; }

    public bool IsSit(){ return sit;}
	public void SetSit(bool s){ sit = s;}
}
