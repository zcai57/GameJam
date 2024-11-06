using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : Singleton<AudioManager>
{
    #region ����
    private AudioSource bkMusicSource;
    private float bkMusicVolume;
    private float effectMusicVolume;
    private List<AudioSource> effectMusicList;
    #endregion
    public AudioManager()
    {
        CommonMono.Instance.AddUpdate(MyUpdate);
        bkMusicSource = null;
        bkMusicVolume = 1;
        effectMusicVolume = 1;
        effectMusicList = new List<AudioSource>();
    }
    private void MyUpdate()
    {
        for(int i=effectMusicList.Count-1;i>=0;--i)
        {
            if (!effectMusicList[i].isPlaying)
            {
                ObjectPool.Instance.SendBack(effectMusicList[i].gameObject.name, effectMusicList[i].gameObject);
                effectMusicList.RemoveAt(i);
            }
        }
    }

    //���ű�������(Audio/BkMusic/"name")
    public void PlayBGM(string name)
    {
        if (bkMusicSource == null)
        {
            GameObject obj = new GameObject("BackGroundMusic");
            bkMusicSource = obj.AddComponent<AudioSource>();
        }
        ResourcesManager.Instance.ResourceLoadAsync<AudioClip>("Audio/Music/"+name, (clip) =>
        {
            bkMusicSource.clip = clip;
            bkMusicSource.volume = bkMusicVolume;
            bkMusicSource.loop = true;
            bkMusicSource.Play();
        });
    }
    //��ͣ��������
    public void PauseBGM()
    {
        if (bkMusicSource == null)
            return;
        bkMusicSource.Pause();
    }
    //ֹͣ��������
    public void StopBGM()
    {
        if(bkMusicSource == null)
            return;
        bkMusicSource.Stop();
    }
    //�ı䱳����������
    public void ChangeBGMVol(float volume)
    {
        bkMusicVolume = volume;
        if(bkMusicSource == null)
            return;
        bkMusicSource.volume = bkMusicVolume;
    }

    //������Ч(Audio/EffectMusic/"name")
    public void PlayEffectMusic(string name,bool isLoop,UnityAction<AudioSource> func=null)
    {
        ResourcesManager.Instance.ResourceLoadAsync<AudioClip>("Audio/SFX/"+name, (clip) =>
        { 
            ObjectPool.Instance.GetObject("Controls/AudioSource", (obj) =>
            {
                AudioSource source = obj.GetComponent<AudioSource>();
                source.clip = clip;
                source.volume = effectMusicVolume;
                source.loop = isLoop;
                effectMusicList.Add(source);
                source.Play();
                if (func != null)
                    func(source);
            });        
        });
    }
    //ֹͣ��Ч
    public void StopEffectMusic(AudioSource source)
    {
        if(effectMusicList.Contains(source))
        {
            source.Stop();
            ObjectPool.Instance.SendBack(source.gameObject.name, source.gameObject);
            effectMusicList.Remove(source);
        }
    }
    //�ı���Ч����
    public void ChangeEffectVolume(float volume)
    {
        effectMusicVolume = volume;
        for (int i = 0; i < effectMusicList.Count; ++i)
            effectMusicList[i].volume = volume;
    }
}
