using UnityEngine;
using TMPro; 
using System.Collections;

public class ScoreBoard : MonoBehaviour
{
    [Header("Configuración UI")]
    public TextMeshProUGUI scoreText;
    public GameObject expedientePanel; 

    [Header("Ajustes")]
    public int puntuacionFinal = 1500; 
    public float duracionAnimacion = 2.0f; 

    void Start()
    {
        scoreText.text = "0000";
        StartCoroutine(AnimarPuntuacion());
    }
    IEnumerator AnimarPuntuacion()
    {
        yield return new WaitForSeconds(1.5f);

        float tiempoPasado = 0f;
        int valorInicial = 0;

        while (tiempoPasado < duracionAnimacion)
        {
            tiempoPasado += Time.deltaTime;
            float porcentaje = tiempoPasado / duracionAnimacion;
            int valorActual = (int)Mathf.Lerp(valorInicial, puntuacionFinal, porcentaje);
            
            scoreText.text = valorActual.ToString("D4"); 
            yield return null; 
        }
        scoreText.text = puntuacionFinal.ToString("D4");
    }
}