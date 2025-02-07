using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager instance;
    
    public AudioSource backgroundMusic1; // 第一首背景音乐的音频源
    public AudioSource backgroundMusic2; // 第二首背景音乐的音频源
    public Slider volumeSlider; // 用于控制音量的滑块组件
    private AudioSource currentMusic; // 当前正在播放的音乐

    private void Awake()
    {
        instance = this;

        // 确保在脚本挂载时获取到对应的组件引用
        if (backgroundMusic1 == null)
        {
            backgroundMusic1 = GetComponent<AudioSource>();
            if (backgroundMusic1 == null)
            {
                Debug.LogError("未找到第一首背景音乐的音频源组件，请确保挂载了该组件或手动指定！");
            }
        }

        if (backgroundMusic2 == null)
        {
            // 根据实际情况找到第二首音乐的音频源组件，这里假设它在同一场景中
            backgroundMusic2 = GameObject.Find("BackgroundMusic2").GetComponent<AudioSource>();
            if (backgroundMusic2 == null)
            {
                Debug.LogError("未找到第二首背景音乐的音频源组件，请确保挂载了该组件或手动指定！");
            }
        }

        if (volumeSlider == null)
        {
            // 这里可以根据实际情况修改查找方式，比如通过标签、名称等找到Slider组件
            volumeSlider = GameObject.FindObjectOfType<Slider>();
            if (volumeSlider == null)
            {
                Debug.LogError("未找到用于控制音量的滑块组件，请确保场景中有该组件！");
            }
        }

        // 初始播放第一首音乐
        currentMusic = backgroundMusic1;
        currentMusic.volume = volumeSlider.value;
        currentMusic.Play();

        // 添加滑块值改变的监听器，以便实时调整音量
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
    }

    // 用于调整音量的方法，根据滑块的值来设置当前播放音乐的音量
    public void AdjustVolume(float volume)
    {
        currentMusic.volume = volume;
    }

    // 切换到第一首音乐的方法
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

    // 切换到第二首音乐的方法
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
