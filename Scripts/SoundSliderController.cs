using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSliderController : MonoBehaviour {
    public Transform itemGroup;
    public Texture fillItemTexture;
    public Texture unfillItemTexture;
    public const int maxIndex = 10;
    public string VolumeCatergoryVariable;
    public AudioMixer audioMixer;


    int curIndex = maxIndex/2 - 1;
    Transform[] itemGroupList;
    float volumeRangePerNode = Constant.VOLUME_RANGE/maxIndex;

    void Start()
    {
        audioMixer.SetFloat(VolumeCatergoryVariable, Constant.DEFAUL_VOLUME);
        itemGroupList = new Transform[itemGroup.childCount];
        RawImage rawImage;
        for (int i = 0; i < itemGroup.childCount; i++)
        {
            itemGroupList[i] = itemGroup.GetChild(i);
            rawImage = itemGroupList[i].GetComponent<RawImage>();
            if (curIndex >= i)
            { 
                rawImage.texture = fillItemTexture;
            }
        }
    }

    void setAudioMusic() {
        
    }

    private void UpdateItemGroup(int index, bool isFilled)
    {
        RawImage rawImage = itemGroupList[index].GetComponent<RawImage>();
        rawImage.texture = isFilled?fillItemTexture:unfillItemTexture;
        float musicVol = -1;

        if (curIndex > 0)
        {
            audioMixer.GetFloat(VolumeCatergoryVariable, out musicVol);
            audioMixer.SetFloat(VolumeCatergoryVariable, musicVol + (isFilled ? volumeRangePerNode : -volumeRangePerNode));
        }
        else 
        {
            //mute sound case
            audioMixer.SetFloat(VolumeCatergoryVariable, isFilled ? Constant.MIN_VOLUME + volumeRangePerNode : Constant.MIN_VOLUME_SYSTEM);
        }
        audioMixer.GetFloat(VolumeCatergoryVariable, out musicVol);
    }

	public void OnIncrease()
    {
        if (curIndex < maxIndex - 1)
        {
            curIndex++;
            UpdateItemGroup(curIndex, true);
        }
    }

    public void OnDecrease()
    {
        if (curIndex >= 0)
        {
            UpdateItemGroup(curIndex, false);
            curIndex--;
        }
    }
}
