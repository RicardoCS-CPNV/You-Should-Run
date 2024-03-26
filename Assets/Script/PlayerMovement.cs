using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // Vitesse de déplacement et force de saut du joueur
    public float moveSpeed;
    public float jumpForce;

    // Booléens pour le saut, l'état du sol, l'escalade.
    private bool isJumping = false;
    private bool isGrounded;
    public bool isClimbing;
    public bool isReverseMovement = false; //Inversion des mouvement

    // Point de vérification au sol et rayon pour détecter le sol
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    // Composants du joueur
    public Rigidbody2D rb; // Physic
    public Animator animator; // Animation
    public SpriteRenderer spriteRenderer; // Visuel du joueur
    public CapsuleCollider2D playerCollider; // Collision

    private Vector3 velocity = Vector3.zero; // Vecteur de velocité
    private float horizontalMovement; // Mouvement horizonral
    private float verticalMovement; // Mouvement vertical

    // Instance statique de PlayerMovement pour le singleton
    public static PlayerMovement instance;

    // Méthode appelée lors de l'initialisation
    private void Awake()
    {
        // Vérifie s'il y a déjà une instance de PlayerMovement
        if (instance != null)
        {
            // Avertissement s'il y a plus d'une instance de PlayerMovement dans la scène
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scène");
            return;
        }

        // Initialise l'instance unique
        instance = this;
    }

    // Méthode appelée à chaque frame
    void Update()
    {
        // Vérifie si le mouvement est inversé
        if (isReverseMovement)
        {
            // Lance les coroutines pour inverser le mouvement et gérer le délai d'inversion
            StartCoroutine(ReverseMovement());
            StartCoroutine(handleReverseDelay());
        }
        else if (!isReverseMovement)
        {
            // Gère le mouvement horizontal et vertical normal du joueur
            horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
            verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime;
        }

        // Vérifie si le joueur appuie sur le bouton de saut et s'il est au sol et non en train de grimper
        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            isJumping = true;
        }
        // Effectue une rotation du joueur en fonction de sa vélocité horizontale
        Flip(rb.velocity.x);

        // Met à jour l'animation du joueur en fonction de sa vélocité
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    // Méthode appelée à chaque frame en fonction du temps
    void FixedUpdate()
    {
        // Vérifie si le joueur est au sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        // Déplace le joueur
        MovePlayer(horizontalMovement, verticalMovement);
    }

    // Méthode pour déplacer le joueur
    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        // Vérifie si le joueur ne grimpe pas
        if (!isClimbing)
        {
            // Déplace le joueur horizontalement et verticalement de manière fluide
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

            // Verifie si le joueur est en train de sauter
            if (isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = false;
            }
        }
        else // Déplacement Verticale
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        }
    }

    // Méthode pour retourner le sprite du joueur en fonction de sa vélocité
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

    // Direction inversée quand piège touché
    public IEnumerator ReverseMovement()
    {
        while (isReverseMovement)
        {
            horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime * (-1); // Direction Horizontal inversée
            verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime * (-1);     // Direction Vertical inversée
            yield return new WaitForSeconds(0f);
        }

    }

    // Durée de l'inversion de direction
    public IEnumerator handleReverseDelay()
    {
        yield return new WaitForSeconds(15f);
        isReverseMovement = false;
    }

    // Afficher le groundCheck dans le mode scene
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Affiche groundCheck en rouge
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); // Affiche le groundCheck en forme de shpère
    }


}