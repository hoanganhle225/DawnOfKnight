using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Key để lưu giá trị vào PlayerPrefs
    private const string VolumeKey = "Volume";
    private const string GenderKey = "Gender";

    // Giá trị mặc định nếu không tìm thấy giá trị trong PlayerPrefs
    private const float DefaultVolume = 0.5f;
    private const string DefaultGender = "Male";
    public Button MaleButton;
    public Button FemaleButton;
    private void Update()
    {
        if(PlayerPrefs.GetString(GenderKey, DefaultGender) == "Male")
        {
            MaleButton.interactable = false;
            FemaleButton.interactable = true;
        }
        else
        {
            MaleButton.interactable = true;
            FemaleButton.interactable = false;
        }
    }
    // Lưu giá trị Volume vào PlayerPrefs
    public  void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }

    // Đọc giá trị Volume từ PlayerPrefs
    public  float LoadVolume()
    {
      
            return PlayerPrefs.GetFloat(VolumeKey,DefaultVolume);
        
    }

    // Lưu giá trị Gender vào PlayerPrefs
    public  void SaveGender(string gender)
    {
        PlayerPrefs.SetString(GenderKey, gender);
        PlayerPrefs.Save();
    }

    // Đọc giá trị Gender từ PlayerPrefs
    public  string LoadGender()
    {

        return PlayerPrefs.GetString(GenderKey, DefaultGender);
        
    }
}
