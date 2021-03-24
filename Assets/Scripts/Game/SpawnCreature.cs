using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreature : MonoBehaviour
{
    [System.Serializable]
    public struct Planet
    {
        public string name;
        public GameObject[] Creatures;
        public Color[] ColorForClickParticleSystems;
        public Color[] ColorBackgrounds;
    }

    public SpawnBackground SpawnBackground;

    private float width;
    private float positionXForCreature;

    private string busyClips = "";

    public Transform parent;
    public GameObject Bank;
    public Planet[] planets;

    public AllUsedSounds UsedSound;
    public SpawnAudioSourceForClickOnCreature SpawnAudio;

    private GameObject[] _creature;
    private Color[] colorForClickParticleSystems;
    private Color[] colorBackgrounds;
    private Camera cameraMain;

    private AllParametrs allParametrs;
    private void Awake()
    {
        allParametrs = JsonUtility.FromJson<AllParametrs>(PlayerPrefs.GetString(AllParametrs.KEY));
        cameraMain = Camera.main;
        width = cameraMain.ScreenToWorldPoint(new Vector3(cameraMain.pixelWidth, cameraMain.pixelHeight, 0)).x;
        positionXForCreature = width * 2;
        _creature = planets[allParametrs.selectedPlanet].Creatures;
        colorForClickParticleSystems = planets[allParametrs.selectedPlanet].ColorForClickParticleSystems;
        colorBackgrounds = planets[allParametrs.selectedPlanet].ColorBackgrounds;
        Spawn();
    }
    public void Spawn()
    {
        SpawnAudio.SpawnAudioSource();
        for (int i = 0; i < _creature.Length; i++)
        {
            GameObject creature = Instantiate(_creature[i], parent.transform);

            creature.AddComponent<BoxCollider2D>().size = new Vector2(14, 28);
            creature.AddComponent<Creature>();
            Creature creatureScript = creature.GetComponent<Creature>();
            creatureScript.offsetX = positionXForCreature;
            creatureScript.colorForClickParticleSystem = colorForClickParticleSystems[i];
            creatureScript.ColorBackground = colorBackgrounds[i];
            creature.transform.position = new Vector3(positionXForCreature * i, creature.transform.position.y, 0);

            GameObject bank = Instantiate(Bank, creature.transform.position, Quaternion.identity);
            creatureScript._BankaForCreature = bank.GetComponent<BankaForCreature>();
            creatureScript.StartClickClip += SpawnAudio.StartClickClip;

            SpawnBackground.Spawn(i, creatureScript.ColorBackground, creature.transform);

            int randomIndexForClip = Random.Range(0, UsedSound.audioClips.Length);
            while (busyClips.Contains(randomIndexForClip.ToString()))
            {
                randomIndexForClip = Random.Range(0, UsedSound.audioClips.Length);
            }
            busyClips += randomIndexForClip.ToString() + "_";
            creatureScript.audioClipForClick = UsedSound.audioClips[randomIndexForClip];
        }
    }
}
