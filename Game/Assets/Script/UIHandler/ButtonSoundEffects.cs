using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundEffects : MonoBehaviour
{
    public AudioClip hoverSound;   // Âm thanh khi di chuột qua nút
    public AudioClip clickSound;   // Âm thanh khi nhấp chuột vào nút

    public AudioSource audioSource;

    void Start()
    {
        // Lấy thành phần AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    public void OnHoverEnter()
    {
        // Phát âm thanh hover khi di chuột qua
        audioSource.PlayOneShot(hoverSound);
    }

    public void OnButtonClick()
    {
        // Phát âm thanh click khi nhấp chuột vào
        audioSource.PlayOneShot(clickSound);
    }
}