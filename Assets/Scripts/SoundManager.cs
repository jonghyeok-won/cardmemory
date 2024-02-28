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

    [SerializeField] private AudioSource audioSourceBGM; // 배경음악을 재생할 오디오 소스
    [SerializeField] private Sound[] bgmSounds; // 배경음악 사운드 배열

    [SerializeField] private AudioSource[] audioSourceEffects; // 효과음을 재생할 오디오 소스 배열
    [SerializeField] private Sound[] effectSounds; // 효과음 사운드 배열

    private string[] playSoundName; // 현재 재생 중인 사운드의 이름을 저장하는 배열

    // Start is called before the first frame update
    void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
    }


    // 효과음 재생
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
                        return; // 사운드 재생 후 루프 종료
                    }
                }
                return;
            }
        }
        Debug.LogWarning("SoundManager: PlaySE called with unknown sound name " + _name);
    }

    // 배경음악 재생
    public void PlayBGM(string _name)
    {
        foreach (var bgm in bgmSounds)
        {
            if (_name == bgm.name)
            {
                audioSourceBGM.clip = bgm.clip;
                audioSourceBGM.Play();
                Debug.Log(bgm.name + " is playing as BGM.");
                return; // 사운드 재생 후 루프 종료
            }
        }
        Debug.LogWarning("SoundManager: PlayBGM called with unknown BGM name " + _name);
    }
}
