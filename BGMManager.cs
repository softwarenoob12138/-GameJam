using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager instance;
    
    public AudioSource backgroundMusic1; // ��һ�ױ������ֵ���ƵԴ
    public AudioSource backgroundMusic2; // �ڶ��ױ������ֵ���ƵԴ
    public Slider volumeSlider; // ���ڿ��������Ļ������
    private AudioSource currentMusic; // ��ǰ���ڲ��ŵ�����

    private void Awake()
    {
        instance = this;

        // ȷ���ڽű�����ʱ��ȡ����Ӧ���������
        if (backgroundMusic1 == null)
        {
            backgroundMusic1 = GetComponent<AudioSource>();
            if (backgroundMusic1 == null)
            {
                Debug.LogError("δ�ҵ���һ�ױ������ֵ���ƵԴ�������ȷ�������˸�������ֶ�ָ����");
            }
        }

        if (backgroundMusic2 == null)
        {
            // ����ʵ������ҵ��ڶ������ֵ���ƵԴ����������������ͬһ������
            backgroundMusic2 = GameObject.Find("BackgroundMusic2").GetComponent<AudioSource>();
            if (backgroundMusic2 == null)
            {
                Debug.LogError("δ�ҵ��ڶ��ױ������ֵ���ƵԴ�������ȷ�������˸�������ֶ�ָ����");
            }
        }

        if (volumeSlider == null)
        {
            // ������Ը���ʵ������޸Ĳ��ҷ�ʽ������ͨ����ǩ�����Ƶ��ҵ�Slider���
            volumeSlider = GameObject.FindObjectOfType<Slider>();
            if (volumeSlider == null)
            {
                Debug.LogError("δ�ҵ����ڿ��������Ļ����������ȷ���������и������");
            }
        }

        // ��ʼ���ŵ�һ������
        currentMusic = backgroundMusic1;
        currentMusic.volume = volumeSlider.value;
        currentMusic.Play();

        // ��ӻ���ֵ�ı�ļ��������Ա�ʵʱ��������
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
    }

    // ���ڵ��������ķ��������ݻ����ֵ�����õ�ǰ�������ֵ�����
    public void AdjustVolume(float volume)
    {
        currentMusic.volume = volume;
    }

    // �л�����һ�����ֵķ���
    public void SwitchToMusic1()
    {
        if (currentMusic != backgroundMusic1)
        {
            currentMusic.Stop();
            currentMusic = backgroundMusic1;
            currentMusic.volume = volumeSlider.value;
            currentMusic.Play();
        }
    }

    // �л����ڶ������ֵķ���
    public void SwitchToMusic2()
    {
        if (currentMusic != backgroundMusic2)
        {
            currentMusic.Stop();
            currentMusic = backgroundMusic2;
            currentMusic.volume = volumeSlider.value;
            currentMusic.Play();
        }
    }
}
