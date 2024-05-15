using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [Header("Variáveis de Painel")]
    public GameObject painelInicio;
    public GameObject painelJogo;

    [Header("Objetos do Jogo")]
    public TMP_Text textoTitulo;
    public TMP_Text textoPergunta;
    public Image imagemQuiz;
    public TMP_Text[] textoResposta;

    [Header("Conteúdo das Perguntas")]
    public string[] titulos;
    public Sprite[] imagens;
    public string[] perguntas;
    public string[] opcao1;
    public string[] opcao2;
    public string[] opcao3;
    public string[] opcao4;
    public int[] opcaoCorreta;
    public int perguntaAtual;

    [Header("Objeto de Música")]
    public AudioSource caixaDeMusica;
    public AudioSource caixaDeEfeitos;

    [Header("Arquivos de Música")]
    public AudioClip musicaMenu;
    public AudioClip musicaJogo;

    [Header("Aquivos de Efeitos Sonoros")]
    public AudioClip efeitoAcerto;
    public AudioClip efeitoErro;

    // Start is called before the first frame update
    void Start()
    {
        painelInicio.SetActive(true);
        painelJogo.SetActive(false);
        caixaDeMusica.clip = musicaMenu;
        caixaDeMusica.Play();
    }

    // Método para iniciar o jogo
    public void IniciarJogo()
    {
        painelInicio.SetActive(false);
        painelJogo.SetActive(true);
        ProximaPergunta(perguntaAtual);
        caixaDeMusica.clip = musicaJogo;
        caixaDeMusica.Play();
    }

    //Método para fazer as perguntas
    public void ProximaPergunta(int numero)
    {
     
        textoTitulo.text = titulos[numero];
        imagemQuiz.sprite = imagens[numero];
        textoPergunta.text = perguntas[numero];
        textoResposta[0].text = opcao1[numero];
        textoResposta[1].text = opcao2[numero];
        textoResposta[2].text = opcao3[numero];
        textoResposta[3].text = opcao4[numero];
    }

    //Método para checar as perguntas
    public void ChecarResposta(int numero)
    {
        StartCoroutine(ValidarResposta(numero));
    }

    //Co-Rotina para mostrar se mostrar se acertou ou errou
    IEnumerator ValidarResposta(int numero) 
    {
        if (numero == opcaoCorreta[perguntaAtual])
        {
            imagemQuiz.color = Color.green;
            caixaDeEfeitos.PlayOneShot(efeitoAcerto);

            textoResposta[0].GetComponentInParent<Button>().interactable = false;
            textoResposta[1].GetComponentInParent<Button>().interactable = false;
            textoResposta[2].GetComponentInParent<Button>().interactable = false;
            textoResposta[3].GetComponentInParent<Button>().interactable = false;

            yield return new WaitForSeconds(2);

            textoResposta[0].GetComponentInParent<Button>().interactable = true;
            textoResposta[1].GetComponentInParent<Button>().interactable = true;
            textoResposta[2].GetComponentInParent<Button>().interactable = true;
            textoResposta[3].GetComponentInParent<Button>().interactable = true;

            imagemQuiz.color = Color.white;

            perguntaAtual++;

            if (perguntaAtual >= titulos.Length)
            {
                painelInicio.SetActive(true);
                painelJogo.SetActive(false);
                perguntaAtual = 0;
                caixaDeMusica.clip = musicaMenu;
                caixaDeMusica.Play();
            }
            else
            {
                ProximaPergunta(perguntaAtual);
            }
        }
        else
        {
            imagemQuiz.color = Color.red;
            caixaDeEfeitos.PlayOneShot(efeitoErro);
            yield return new WaitForSeconds(2);

            painelInicio.SetActive(true);
            painelJogo.SetActive(false);
            perguntaAtual = 0;
            caixaDeMusica.clip = musicaMenu;
            caixaDeMusica.Play();
        }

    


       
    }

}
