using UnityEngine;
using System.Collections;

public class ControladorPersonaje1 : MonoBehaviour {

	public float fuerzaSalto = 100f;

	private bool enSuelo = true;
	public Transform comprobadorSuelo;
	private float comprobadorRadio = 0.07f;
	public LayerMask mascaraSuelo;

	private bool dobleSalto = false;

	private Animator animator;

	private bool corriendo = false;
	public float velocidad = 1f;
    private Rigidbody2D rb;
    void Awake(){
		animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate(){
		if(corriendo){
			rb.velocity = new Vector2(velocidad, rb.velocity.y);
		}
		animator.SetFloat("VelX", rb.velocity.x);
		enSuelo = Physics2D.OverlapCircle(comprobadorSuelo.position, comprobadorRadio, mascaraSuelo);
		animator.SetBool("isGrounded", enSuelo);
		if(enSuelo){
			dobleSalto = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if(corriendo){
				// Hacemos que salte si puede saltar
				if(enSuelo || !dobleSalto){
					//audio.Play();
					rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
					//rigidbody2D.AddForce(new Vector2(0, fuerzaSalto));
					if(!dobleSalto && !enSuelo){
						dobleSalto = true;
					}
				}
			}else{
				corriendo = true;
				NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeEmpiezaACorrer");
			}
		}
	}
}
