using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // Vitesse de d�placement et force de saut du joueur
    public float moveSpeed;
    public float jumpForce;

    // Bool�ens pour le saut, l'�tat du sol, l'escalade.
    private bool isJumping = false;
    private bool isGrounded;
    public bool isClimbing;
    public bool isReverseMovement = false; //Inversion des mouvement

    // Point de v�rification au sol et rayon pour d�tecter le sol
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    // Composants du joueur
    public Rigidbody2D rb; // Physic
    public Animator animator; // Animation
    public SpriteRenderer spriteRenderer; // Visuel du joueur
    public CapsuleCollider2D playerCollider; // Collision

    private Vector3 velocity = Vector3.zero; // Vecteur de velocit�
    private float horizontalMovement; // Mouvement horizonral
    private float verticalMovement; // Mouvement vertical

    // Instance statique de PlayerMovement pour le singleton
    public static PlayerMovement instance;

    // M�thode appel�e lors de l'initialisation
    private void Awake()
    {
        // V�rifie s'il y a d�j� une instance de PlayerMovement
        if (instance != null)
        {
            // Avertissement s'il y a plus d'une instance de PlayerMovement dans la sc�ne
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la sc�ne");
            return;
        }

        // Initialise l'instance unique
        instance = this;
    }

    // M�thode appel�e � chaque frame
    void Update()
    {
        // V�rifie si le mouvement est invers�
        if (isReverseMovement)
        {
            // Lance les coroutines pour inverser le mouvement et g�rer le d�lai d'inversion
            StartCoroutine(ReverseMovement());
            StartCoroutine(handleReverseDelay());
        }
        else if (!isReverseMovement)
        {
            // G�re le mouvement horizontal et vertical normal du joueur
            horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
            verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime;
        }

        // V�rifie si le joueur appuie sur le bouton de saut et s'il est au sol et non en train de grimper
        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            isJumping = true;
        }
        // Effectue une rotation du joueur en fonction de sa v�locit� horizontale
        Flip(rb.velocity.x);

        // Met � jour l'animation du joueur en fonction de sa v�locit�
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    // M�thode appel�e � chaque frame en fonction du temps
    void FixedUpdate()
    {
        // V�rifie si le joueur est au sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        // D�place le joueur
        MovePlayer(horizontalMovement, verticalMovement);
    }

    // M�thode pour d�placer le joueur
    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        // V�rifie si le joueur ne grimpe pas
        if (!isClimbing)
        {
            // D�place le joueur horizontalement et verticalement de mani�re fluide
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

            // Verifie si le joueur est en train de sauter
            if (isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = false;
            }
        }
        else // D�placement Verticale
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        }
    }

    // M�thode pour retourner le sprite du joueur en fonction de sa v�locit�
    void Flip(float _velocity)
    {
        if(_velocity > 0.1f) // A droite
        {
            spriteRenderer.flipX = false;
        }else if(_velocity < -0.1f)// A gauche
        {
            spriteRenderer.flipX = true;
        }
    }

    // Direction invers�e quand pi�ge touch�
    public IEnumerator ReverseMovement()
    {
        while (isReverseMovement)
        {
            horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime * (-1); // Direction Horizontal invers�e
            verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime * (-1);     // Direction Vertical invers�e
            yield return new WaitForSeconds(0f);
        }

    }

    // Dur�e de l'inversion de direction
    public IEnumerator handleReverseDelay()
    {
        yield return new WaitForSeconds(15f);
        isReverseMovement = false;
    }

    // Afficher le groundCheck dans le mode scene
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Affiche groundCheck en rouge
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); // Affiche le groundCheck en forme de shp�re
    }


}