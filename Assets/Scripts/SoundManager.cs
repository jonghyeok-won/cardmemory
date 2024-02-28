using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip; 
}

public class SoundManager : MonoBehaviour
{
    #region singleton
    static public SoundManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion singleton

    [SerializeField] private AudioSource audioSourceBGM; // ��������� ����� ����� �ҽ�
    [SerializeField] private Sound[] bgmSounds; // ������� ���� �迭

    [SerializeField] private AudioSource[] audioSourceEffects; // ȿ������ ����� ����� �ҽ� �迭
    [SerializeField] private Sound[] effectSounds; // ȿ���� ���� �迭

    private string[] playSoundName; // ���� ��� ���� ������ �̸��� �����ϴ� �迭

    // Start is called before the first frame update
    void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
    }


    // ȿ���� ���
    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        playSoundName[j] = effectSounds[i].name;
                        return; // ���� ��� �� ���� ����
                    }
                }
                return;
            }
        }
        Debug.LogWarning("SoundManager: PlaySE called with unknown sound name " + _name);
    }

    // ������� ���
    public void PlayBGM(string _name)
    {
        foreach (var bgm in bgmSounds)
        {
            if (_name == bgm.name)
            {
                audioSourceBGM.clip = bgm.clip;
                audioSourceBGM.Play();
                Debug.Log(bgm.name + " is playing as BGM.");
                return; // ���� ��� �� ���� ����
            }
        }
        Debug.LogWarning("SoundManager: PlayBGM called with unknown BGM name " + _name);
    }
}
