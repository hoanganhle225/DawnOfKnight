using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public Slider mainVolumeSlider; // Kéo và thả Main Volume Slider vào đây trong Inspector của Unity.

    private void Start()
    {
        // Khởi tạo giá trị volume của AudioListener từ PlayerPrefs nếu tồn tại.
        if (PlayerPrefs.HasKey("AudioListenerVolume"))
        {
            float audioListenerVolume = PlayerPrefs.GetFloat("AudioListenerVolume");
            mainVolumeSlider.value = audioListenerVolume;
            UpdateAudioListenerVolume(mainVolumeSlider.value);
        }
        else
        {
            float audioListenerVolume = 1f;
            mainVolumeSlider.value = audioListenerVolume;
            UpdateAudioListenerVolume(audioListenerVolume);
        }
        mainVolumeSlider.onValueChanged.AddListener(OnMainVolumeChanged);
    }

    // Hàm này sẽ tự động thay đổi khi giá trị của Main Volume Slider thay đổi.
    public void OnMainVolumeChanged(float volume)
    {
        UpdateAudioListenerVolume(volume); // Áp dụng giá trị volume cho AudioListener.
    }

    // Hàm này để thay đổi volume của AudioListener dựa vào giá trị của Main Volume Slider.
    private void UpdateAudioListenerVolume(float volume)
    {
        AudioListener.volume = volume; // Áp dụng giá trị volume cho AudioListener.

        // Lưu giá trị volume của AudioListener vào PlayerPrefs để giữ lại giá trị này sau khi thoát game.
        PlayerPrefs.SetFloat("AudioListenerVolume", volume);
    }
}
