using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class InputFieldScript : MonoBehaviour
{
    public InputField inputField;
    public Text nome;
    
    [SerializeField]
    private VideoPlayer player;
    [SerializeField]
    private GameObject video;

    [SerializeField]
    private VideoPlayer playerLorax;
    [SerializeField]
    private GameObject videoLorax;

    [SerializeField]
    private VideoPlayer playerSaulGoodman;
    [SerializeField]
    private GameObject videoSaulGoodman;

    public GameObject objetoComSalvarCarregar;

    void Start()
    {
        inputField.ActivateInputField();
    }
    private void Update()
    {
        if (nome.text == "Terreno de Macaco")
        {
            video.SetActive(true);
            player.Play();
        }
        if (nome.text == "Terreno de Lorax")
        {
            videoLorax.SetActive(true);
            playerLorax.Play();
        }
        if (nome.text == "Terreno de SaulGoodman")
        {
            videoSaulGoodman.SetActive(true);
            playerSaulGoodman.Play();
        }
        if (nome.text == "Terreno de SaveReset")
        {
            objetoComSalvarCarregar.GetComponent<Upgrades>().DeletarSaves();
        }
    }

    public void ColetarTexto()
    {
        nome.text = "Terreno de " + inputField.text;
        inputField.DeactivateInputField();
        Destroy(inputField.gameObject);
    }

}
