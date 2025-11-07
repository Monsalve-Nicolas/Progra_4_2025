using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    [Header("Configuración de la torreta")]
    public float radioApuntar = 10f;
    public float radioAtaque = 9f;
    public float tiempoEntreDisparos = 1f;
    public float powerBullet = 7f;

    [Header("Referencias")]
    public Transform spawnBullet;
    public GameObject prefabBullet;

    [Header("Detección de enemigos")]
    public LayerMask detectionLayer;

    private Transform objetivoActual;
    private Vector2 direccion;
    private float tiempoUltimoDisparo;

    void Update()
    {
        DetectarObjetivo();
        ApuntarAlObjetivo();

        if (objetivoActual == null) return;

        if (direccion.magnitude <= radioAtaque && Time.time >= tiempoUltimoDisparo + tiempoEntreDisparos)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    void DetectarObjetivo()
    {
        Collider2D[] objetivos = Physics2D.OverlapCircleAll(transform.position, radioApuntar, detectionLayer);

        if (objetivos.Length > 0)
        {
            Debug.Log($"{objetivos.Length} enemigo(s) detectado(s) dentro del radio de apuntar.");
        }

        Transform objetivoMasCercano = null;
        float distanciaMasCercana = Mathf.Infinity;

        foreach (Collider2D col in objetivos)
        {
            float distancia = Vector2.Distance(transform.position, col.transform.position);
            if (distancia < distanciaMasCercana)
            {
                distanciaMasCercana = distancia;
                objetivoMasCercano = col.transform;
            }
        }

        if (objetivoMasCercano != objetivoActual)
        {
            if (objetivoMasCercano != null)
                Debug.Log($"Nuevo objetivo detectado: {objetivoMasCercano.name}");
            else
                Debug.Log("Sin objetivo en el radio de apuntar.");
        }

        objetivoActual = objetivoMasCercano;
    }

    void ApuntarAlObjetivo()
    {
        if (objetivoActual == null) return;

        direccion = objetivoActual.position - transform.position;

        if (direccion.magnitude <= radioApuntar)
        {
            transform.up = direccion;
        }
    }

    void Disparar()
    {
        if (prefabBullet == null || spawnBullet == null) return;

        Debug.Log($" Disparando hacia: {objetivoActual.name}");

        GameObject proyectil = Instantiate(prefabBullet, spawnBullet.position, transform.rotation);
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(direccion.normalized * powerBullet, ForceMode2D.Impulse);
        }

        Destroy(proyectil, 5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioApuntar);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioAtaque);
    }
}
