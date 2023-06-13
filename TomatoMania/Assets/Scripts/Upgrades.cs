using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    [SerializeField]
    Text dinheirosTxt;
    [SerializeField]
    Text[] textoUpgrades = new Text[10];
    [SerializeField]
    Button[] botoes = new Button[11];
    [SerializeField]
    Text texto2x;

    public float dinheiro;
    [SerializeField]
    bool[] loop = new bool[10];
    [SerializeField]
    float[] preco = new float[10];
    
    public float[] nivelDoUpgrade = new float[10];

    private Animator anim;
    public Animator animCamin;
    public Animator animCaminFut;
    bool clickUpgrade = false;

    public AudioClip[] audioClips;
    public AudioSource audioSource, audioSourceOst1, audioSourceOst2;

   
    GameObject[] objetosCenario = new GameObject[10];
    GameObject[] agricultorCenario = new GameObject[10];
    GameObject[] agricultorFutCenario = new GameObject[10];
    GameObject[] terrenosCenario = new GameObject[5];


    [SerializeField]
    GameObject[] objetosCenariosFuturista = new GameObject[10];

    public Texture texturaNova;
    public RawImage buttonAgricultor;

    public GameObject sol;

    bool musicaInicial;

    void Start()
    {
        Iniciar();

        Carregar();


        audioSource.PlayOneShot(audioClips[2]);
        
        anim = botoes[11].GetComponent<Animator>();
        animCamin = objetosCenario[6].GetComponent<Animator>();
        //definição dos preços iniciais
        preco[0] = 10; 
        preco[1] = 50; 
        preco[2] = 100; 
        preco[3] = 250; 
        preco[4] = 500;
        preco[5] = 1000;
        preco[6] = 5000;
        preco[7] = 10000;
        preco[8] = 50000;
        preco[9] = 100000;     
    }
    void FixedUpdate()
    {
        //colocando o dinheiro no texto de forma arredondada
        int dinheiroArredondado = (int)Mathf.FloorToInt(dinheiro);
        dinheirosTxt.text = dinheiroArredondado.ToString();
        //ativação dos upgrades
        if (loop[0] == true)
        {
            dinheiro = dinheiro + (nivelDoUpgrade[0] * Time.deltaTime) * (1 + 10 / 100);
        }
        if (loop[1] == true)
        {
            dinheiro = dinheiro + (nivelDoUpgrade[1] * Time.deltaTime) * (2 + 10 / 100);
        }
        if (loop[2] == true)
        {
            dinheiro = dinheiro + (nivelDoUpgrade[2] * Time.deltaTime) * (4 + 10 / 100);
        }
        if (loop[3] == true)
        {
            dinheiro = dinheiro + (nivelDoUpgrade[3] * Time.deltaTime) * (8 + 10 / 100);
        }
        if (loop[4] == true)
        {
            dinheiro = dinheiro + (nivelDoUpgrade[4] * Time.deltaTime) * (14 + 10 / 100);
        }
        if (loop[5] == true)
        {
            dinheiro = dinheiro + nivelDoUpgrade[5] * Time.deltaTime * (20 + 10 / 100);
        }
        if (loop[6] == true)
        {
            dinheiro = dinheiro + nivelDoUpgrade[6] * Time.deltaTime * (28 + 10 / 100);
        }
        if (loop[7] == true)
        {
            dinheiro = dinheiro + nivelDoUpgrade[7] * Time.deltaTime * (35 + 10 / 100);
        }
        if (loop[8] == true)
        {
            dinheiro = dinheiro + (nivelDoUpgrade[8] * Time.deltaTime) * (43 + 10 / 100);
        }
        if (loop[9] == true)
        {
            dinheiro = dinheiro + (nivelDoUpgrade[9] * Time.deltaTime) * (70 + 10 / 100);
        }

        //colocando upgrade e nivel no texto dos botoes
        textoUpgrades[0].text = preco[0].ToString() + "                  " + nivelDoUpgrade[0].ToString();
        textoUpgrades[1].text = preco[1].ToString() + "                  " + nivelDoUpgrade[1].ToString();
        textoUpgrades[2].text = preco[2].ToString() + "                  " + nivelDoUpgrade[2].ToString();
        textoUpgrades[3].text = preco[3].ToString() + "                  " + nivelDoUpgrade[3].ToString();
        textoUpgrades[4].text = preco[4].ToString() + "                  " + nivelDoUpgrade[4].ToString();
        textoUpgrades[5].text = preco[5].ToString() + "                  " + nivelDoUpgrade[5].ToString();
        textoUpgrades[6].text = preco[6].ToString() + "                  " + nivelDoUpgrade[6].ToString();
        textoUpgrades[7].text = preco[7].ToString() + "                  " + nivelDoUpgrade[7].ToString();
        textoUpgrades[8].text = preco[8].ToString() + "                  " + nivelDoUpgrade[8].ToString();
        textoUpgrades[9].text = preco[9].ToString() + "                  " + nivelDoUpgrade[9].ToString();

        //verificação do dinheiro
        for (int i = 0; i < preco.Length; i++)
        {
            if (dinheiro >= preco[i])
            {
                botoes[i].interactable = true;
                textoUpgrades[i].color = new Color(255f, 255f, 255f);
            }
            else
            {
                textoUpgrades[i].color = Color.gray;
                botoes[i].interactable = false;
            }
        }

        if (dinheiro >= 30000 && clickUpgrade == false)
        {
            botoes[10].interactable = true;
            textoUpgrades[10].color = new Color(255f, 255f, 255f);
        }
        else if (dinheiro < 30000 || !clickUpgrade)
        {
            textoUpgrades[10].color = Color.gray;
            botoes[10].interactable = false;
        }
    }
    public void Click()
    {
        if (clickUpgrade == true)
        {
            dinheiro = dinheiro + 2;
        }
        else
        {
            dinheiro++;
        }

        audioSource.PlayOneShot(audioClips[0]);
        anim.SetBool("Click", true);
        Invoke("DesativarBotao", 0.1f);
    }
    void DesativarBotao()
    {
        anim.SetBool("Click", false);
    }

    //botoes de mehloria
    public void PlantacaoUpgrade()
    {
        if (dinheiro >= preco[0] )
        {
            loop[0] = true;
            nivelDoUpgrade[0]++;
            dinheiro = dinheiro - preco[0];
            preco[0] = preco[0] + nivelDoUpgrade[0] * 2;
            audioSource.PlayOneShot(audioClips[1]);


            for (int i = 0; i < terrenosCenario.Length; i++)
            {
                int nivelRequerido = (i + 1) * 10;
                if (nivelDoUpgrade[0] >= nivelRequerido)
                {
                    terrenosCenario[i].SetActive(true);
                }
                else
                {
                    terrenosCenario[i].SetActive(false);
                }
            }
        }

    }
    public void AgricultorUpgrade()
    {
        if (dinheiro >= preco[1])
        {
            loop[1] = true;
            nivelDoUpgrade[1]++;
            dinheiro = dinheiro - preco[1];
            preco[1] = preco[1] + nivelDoUpgrade[1] * 5;
            audioSource.PlayOneShot(audioClips[1]);
            for (int i = 0; i < agricultorCenario.Length; i++)
            {
                
                int nivelRequerido = (i + 1) * 10;
                if (nivelDoUpgrade[1] >= nivelRequerido)
                {
                    agricultorCenario[i].SetActive(true);
                }
                else
                {
                    agricultorCenario[i].SetActive(false);
                }
            }

        }
    }
    public void EstufaUpgrade()
    {
        if (dinheiro >= preco[2])
        {
            loop[2] = true;
            nivelDoUpgrade[2]++;
            dinheiro = dinheiro - preco[2];
            preco[2] = preco[2] + nivelDoUpgrade[2] * 10;
            audioSource.PlayOneShot(audioClips[1]);

            if (nivelDoUpgrade[2] == 1)
            {
                objetosCenario[2].SetActive(true);

            }
        }
    }
    public void FazendaUpgrade()
    {
        if (dinheiro >= preco[3])
        {
            loop[3] = true;
            nivelDoUpgrade[3]++;
            dinheiro = dinheiro - preco[3];
            preco[3] = preco[3] + nivelDoUpgrade[3] * 25;
            audioSource.PlayOneShot(audioClips[1]);

            if (nivelDoUpgrade[3] == 1)
            {
                objetosCenario[3].SetActive(true);
            }
        }
    }
    public void IndustriaUpgrade()
    {
        if (dinheiro >= preco[4])
        {
            loop[4] = true;
            nivelDoUpgrade[4]++;
            dinheiro = dinheiro - preco[4];
            preco[4] = preco[4] + nivelDoUpgrade[4] * 50;
            audioSource.PlayOneShot(audioClips[1]);

            if (nivelDoUpgrade[4] == 1)
            {
                objetosCenario[4].SetActive(true);
            }
        }
    }
    public void LaboratorioUpgrade()
    {
        if (dinheiro >= preco[5])
        {
           
            loop[5] = true;
            nivelDoUpgrade[5]++;
            dinheiro = dinheiro - preco[5];
            preco[5] = preco[5] + nivelDoUpgrade[5] * 100;
            audioSource.PlayOneShot(audioClips[1]);

            if (nivelDoUpgrade[5] == 1)
            {
                objetosCenario[5].SetActive(true);
            }
        }
    }
    public void DistribuidoraUpgrade()
    {
        if (dinheiro >= preco[6])
        {
            loop[6] = true;
            nivelDoUpgrade[6]++;
            dinheiro = dinheiro - preco[6];
            preco[6] = preco[6] + nivelDoUpgrade[6] * 500;
            audioSource.PlayOneShot(audioClips[1]);
            
            if (nivelDoUpgrade[6] == 1)
            {

                objetosCenario[6].SetActive(true);
                animCamin.SetBool("StartAnim", true);

            }
        }
    }
    public void AeroportoUpgrade()
    {
        if (dinheiro >= preco[7])
        {
           
            loop[7] = true;
            nivelDoUpgrade[7]++;
            dinheiro = dinheiro - preco[7];
            preco[7] = preco[7] + nivelDoUpgrade[7] * 1000;
            audioSource.PlayOneShot(audioClips[1]);
            if (nivelDoUpgrade[7] == 1)
            {
        
                objetosCenario[7].SetActive(true);

            }
        }
    }
    public void FogueteUpgrade()
    {
        if (dinheiro >= preco[8])
        {
            loop[8] = true;
            nivelDoUpgrade[8]++;
            dinheiro = dinheiro - preco[8];
            preco[8] = preco[8] + nivelDoUpgrade[8] * 1500;
            audioSource.PlayOneShot(audioClips[1]);

            if (nivelDoUpgrade[8] == 1)
            {
                objetosCenario[8].SetActive(true);

            }
        }
    }
    public void FuturoUpgrade()
    {
        if (dinheiro >= preco[9])
        {
            audioSourceOst1.Stop();
            audioSourceOst2.Play();

            loop[9] = true;
            nivelDoUpgrade[9]++;
            dinheiro = dinheiro - preco[9];
            preco[9] = preco[9] + nivelDoUpgrade[9] * 2000;
            audioSource.PlayOneShot(audioClips[1]);

            buttonAgricultor.texture = texturaNova;
           
            sol.transform.rotation = Quaternion.Euler(190f, -30, 0f);
            
           
            if (nivelDoUpgrade[9] >= 1)
            {
                for (int i = 1; i <= 9; i++)
                {
                    if (nivelDoUpgrade[1] >= i * 10)
                    {
                        agricultorCenario[i - 1].SetActive(false);
                        agricultorFutCenario[i - 1].SetActive(true);
                    }
                }

                for (int i = 2; i <= 8; i++)
                {
                    if (nivelDoUpgrade[i] >= 1)
                    {
                        objetosCenario[i].SetActive(false);
                        objetosCenariosFuturista[i].SetActive(true);

                        if (i == 6) // Se for o objeto "Caminhao"
                        {
                            animCamin.SetBool("StartAnimFut", true);
                        }
                    }
                }
            }
        }
    }

    //botão de compra da melhoria de clicks

    public void ClickUpgrade()
    {
        if (clickUpgrade == false)
        {
            clickUpgrade = true;
            dinheiro = dinheiro - 30000;
            audioSource.PlayOneShot(audioClips[1]);
            botoes[10].interactable = false;
            texto2x.text = "Comprado";
        }
    }

    private void Iniciar()
    {
        for (int i = 0; i < agricultorCenario.Length; i++)
        {
            string nomeObjeto = "Agricultor" + (i + 1);
            agricultorCenario[i] = GameObject.Find(nomeObjeto);
        }

        for (int i = 0; i < agricultorFutCenario.Length; i++)
        {
            string nomeObjeto = "AgricultorFut" + (i + 1);
            agricultorFutCenario[i] = GameObject.Find(nomeObjeto);
        }

        for (int i = 0; i < terrenosCenario.Length; i++)
        {
            string nomeObjeto = "Terreno";
            if (i > 0)
            {
                nomeObjeto += (i + 1);
            }
            terrenosCenario[i] = GameObject.Find(nomeObjeto);
        }

        string[] nomesObjetos = { "Estufa", "Fazenda", "Fabrica", "Laboratorio", "Caminhao", "Aeroporto", "Foguete" };

        for (int i = 2; i < objetosCenario.Length; i++)
        {
            if (i < nomesObjetos.Length + 2)
            {
                objetosCenario[i] = GameObject.Find(nomesObjetos[i - 2]);
            }
        }

        string[] nomesObjetosFuturista = { "EstufaFut", "FazendaFut", "FabricaFut", "LaboratorioFut", "CaminhaoFut", "AeroportoFut", "FogueteFut" };

        for (int i = 2; i < objetosCenariosFuturista.Length; i++)
        {
            if (i < nomesObjetosFuturista.Length + 2)
            {
                objetosCenariosFuturista[i] = GameObject.Find(nomesObjetosFuturista[i - 2]);
            }
        }

        GameObject[] objetosDesativar = {
    agricultorCenario[0], agricultorCenario[1], agricultorCenario[2], agricultorCenario[3], agricultorCenario[4],
    agricultorCenario[5], agricultorCenario[6], agricultorCenario[7], agricultorCenario[8], agricultorCenario[9],
    agricultorFutCenario[0], agricultorFutCenario[1], agricultorFutCenario[2], agricultorFutCenario[3], agricultorFutCenario[4],
    agricultorFutCenario[5], agricultorFutCenario[6], agricultorFutCenario[7], agricultorFutCenario[8], agricultorFutCenario[9],
    terrenosCenario[0], terrenosCenario[1], terrenosCenario[2], terrenosCenario[3], terrenosCenario[4],
    objetosCenario[2], objetosCenario[3], objetosCenario[4], objetosCenario[5], objetosCenario[6], objetosCenario[7], objetosCenario[8],
    objetosCenariosFuturista[2], objetosCenariosFuturista[3], objetosCenariosFuturista[4], objetosCenariosFuturista[5], objetosCenariosFuturista[6],
    objetosCenariosFuturista[7], objetosCenariosFuturista[8]
};

        for (int i = 0; i < objetosDesativar.Length; i++)
        {
            if (objetosDesativar[i] != null)
            {
                objetosDesativar[i].SetActive(false);
            }
        }
    }

 

    // Método para carregar os valores da array nivelDoUpgrade usando PlayerPrefs
    public void Salvar()
    {
        SalvarArrayFloat(nivelDoUpgrade, "NivelUpgrade_");
        SalvarArrayFloat(preco, "Preco_");
        SalvarArrayBool(loop, "Loop_");
        SalvarEstadoDosObjetos();
        PlayerPrefs.SetFloat("Dinheiro", dinheiro);
        PlayerPrefs.Save(); // Salva as alterações no PlayerPrefs imediatamente
    }

    public void Carregar()
    {
        CarregarArrayFloat(nivelDoUpgrade, "NivelUpgrade_");
        CarregarArrayFloat(preco, "Preco_");
        CarregarArrayBool(loop, "Loop_");
        dinheiro = PlayerPrefs.GetFloat("Dinheiro", 0f);
        CarregarEstadoDosObjetos();
    }

    private void SalvarArrayFloat(float[] array, string chavePrefixo)
    {
        for (int i = 0; i < array.Length; i++)
        {
            string chave = chavePrefixo + i.ToString();
            PlayerPrefs.SetFloat(chave, array[i]);
        }
    }

    private void CarregarArrayFloat(float[] array, string chavePrefixo)
    {
        for (int i = 0; i < array.Length; i++)
        {
            string chave = chavePrefixo + i.ToString();
            array[i] = PlayerPrefs.GetFloat(chave, 0f);
        }
    }

    private void SalvarArrayBool(bool[] array, string chavePrefixo)
    {
        for (int i = 0; i < array.Length; i++)
        {
            string chave = chavePrefixo + i.ToString();
            PlayerPrefs.SetInt(chave, array[i] ? 1 : 0);
        }
    }

    private void CarregarArrayBool(bool[] array, string chavePrefixo)
    {
        for (int i = 0; i < array.Length; i++)
        {
            string chave = chavePrefixo + i.ToString();
            array[i] = PlayerPrefs.GetInt(chave, 0) == 1;
        }
    }



    public void SalvarEstadoDosObjetos()
    {
        SalvarEstadoDosGameObjects(objetosCenario, "ObjetosCenario_");
        SalvarEstadoDosGameObjects(agricultorCenario, "AgricultorCenario_");
        SalvarEstadoDosGameObjects(agricultorFutCenario, "AgricultorFutCenario_");
        SalvarEstadoDosGameObjects(terrenosCenario, "TerrenosCenario_");
        SalvarEstadoDosGameObjects(objetosCenariosFuturista, "ObjetosCenariosFuturista_");
        PlayerPrefs.Save(); // Salva as alterações no PlayerPrefs imediatamente
    }

    public void CarregarEstadoDosObjetos()
    {
        CarregarEstadoDosGameObjects(objetosCenario, "ObjetosCenario_");
        CarregarEstadoDosGameObjects(agricultorCenario, "AgricultorCenario_");
        CarregarEstadoDosGameObjects(agricultorFutCenario, "AgricultorFutCenario_");
        CarregarEstadoDosGameObjects(terrenosCenario, "TerrenosCenario_");
        CarregarEstadoDosGameObjects(objetosCenariosFuturista, "ObjetosCenariosFuturista_");
    }

    private void SalvarEstadoDosGameObjects(GameObject[] array, string chavePrefixo)
    {
        for (int i = 0; i < array.Length; i++)
        {
            string chave = chavePrefixo + i.ToString();
            bool estadoAtivo = (array[i] != null && array[i].activeSelf);
            PlayerPrefs.SetInt(chave, estadoAtivo ? 1 : 0);
        }
    }

    private void CarregarEstadoDosGameObjects(GameObject[] array, string chavePrefixo)
    {
        for (int i = 0; i < array.Length; i++)
        {
            string chave = chavePrefixo + i.ToString();
            int estadoAtivo = PlayerPrefs.GetInt(chave, 0);
            if (array[i] != null)
            {
                array[i].SetActive(estadoAtivo == 1);
            }
        }
    }
    public void DeletarSaves()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Saves deletados com sucesso.");
    }
}
