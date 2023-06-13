using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    GameObject panelOpções;
    public UnityEngine.UI.Slider volumeSlider, volumeSliderSFX;
    public AudioSource audioSourceOst1;
    public AudioSource audioSourceOst2;
    public AudioSource audioSourceSFX;
    public AudioSource audioSourceCaminhao;
    public AudioSource audioSourceCaminhaoFut;
    public AudioClip[] audioClips;

    public Texture[] novaTextura = new Texture[8];

    public RawImage rawImageT;
    public RawImage rawImageM;

    public RawImage buttonSFX;
    public RawImage buttonMusica;

    public Texture[] mutadoDesmutado = new Texture[4];

    public GameObject objetoComSalvarCarregar;
    void Start()
    {
        volumeSlider.onValueChanged.AddListener(AtualizarVolume);
        volumeSliderSFX.onValueChanged.AddListener(AtualizarVolumeSFX);
    }

    public void AbrirPause()
    {
        panelOpções.SetActive(true);
        audioSourceSFX.PlayOneShot(audioClips[0]);
    }
    public void FecharPause()
    {
        panelOpções.SetActive(false);
        audioSourceSFX.PlayOneShot(audioClips[0]);
    }
    public void FecharJogo()
    {
        objetoComSalvarCarregar.GetComponent<Upgrades>().Salvar();

        audioSourceSFX.PlayOneShot(audioClips[0]);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    private void AtualizarVolume(float volume)
    {
        audioSourceOst1.volume = volume;
        audioSourceOst2.volume = volume;
    }
    private void AtualizarVolumeSFX(float volumeSFX)
    {
        audioSourceSFX.volume = volumeSFX;
        audioSourceCaminhao.volume = volumeSFX / 10;
        audioSourceCaminhaoFut.volume = volumeSFX / 10;
    }

    public void MutarSFX()
    {
        volumeSliderSFX.value = 0;
        audioSourceSFX.PlayOneShot(audioClips[0]);
    }
    public void Mutar()
    {
        volumeSlider.value = 0;
        audioSourceSFX.PlayOneShot(audioClips[0]);
    }

    public void Update()
    {
        buttonSFX.texture = (volumeSliderSFX.value > 0) ? mutadoDesmutado[0] : mutadoDesmutado[1];
        buttonMusica.texture = (volumeSlider.value > 0) ? mutadoDesmutado[2] : mutadoDesmutado[3];

    }

    private Dictionary<int, int[]> trocasTextura = new Dictionary<int, int[]>()
{
    { 1, new int[] { 0, 1 } },
    { 2, new int[] { 2, 3 } },
    { 3, new int[] { 4, 5 } },
    { 4, new int[] { 6, 7 } },
    { 5, new int[] { 8, 9 } },
    { 6, new int[] { 10, 11 } },
    { 7, new int[] { 12, 13 } },
    { 8, new int[] { 14, 15 } }
};

    public void Trocar(int indice)
    {
        audioSourceSFX.PlayOneShot(audioClips[0]);
        if (trocasTextura.ContainsKey(indice))
        {
            int[] indicesTextura = trocasTextura[indice];
            rawImageM.texture = novaTextura[indicesTextura[0]];
            rawImageT.texture = novaTextura[indicesTextura[1]];
        }
    }
}
