using UnityEngine;

public class animacion : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite spriteDestruccion;
    public float velocidadAnimacion = 0.3f;

    private SpriteRenderer sr;
    private bool usandoSprite1 = true;
    private float temporizador;
    private bool destruido = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprite1;
    }

    void Update()
    {
        if (destruido) return; // si ya se destruye, no seguir animando

        temporizador += Time.deltaTime;
        if (temporizador >= velocidadAnimacion)
        {
            temporizador = 0;
            usandoSprite1 = !usandoSprite1;
            sr.sprite = usandoSprite1 ? sprite1 : sprite2;
        }
    }

    public void MostrarDestruccion()
    {
        destruido = true;
        if (sr == null) sr = GetComponent<SpriteRenderer>();

        sr.sprite = spriteDestruccion;
        sr.sortingOrder = 50;
        sr.color = Color.white;
        transform.localScale = new Vector3(2f, 2f, 1f);
    }
}
