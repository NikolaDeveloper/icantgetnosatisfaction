using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColourPickerController : MonoBehaviour {

	public Image[] colourSprites;

	[Range(0,2)]
	public int spriteToChange = 0;

	public Image colorViewImage;
	public Slider redSlider;
	public Slider greenSlider;
	public Slider blueSlider;

	void Start(){
		
	}

	void OnEnable(){
		colorViewImage.color = colourSprites [spriteToChange].color;
	}

	void Update(){
		colorViewImage.color = new Color (redSlider.value, greenSlider.value, blueSlider.value);

		colourSprites [spriteToChange].color = colorViewImage.color;

        if (spriteToChange == 0)
        {
            SettingsMainMenu.Instance.SetTrainMainColor(colorViewImage.color);
        }
        else if (spriteToChange == 1)
        {
            SettingsMainMenu.Instance.SetTrainStripeColor(colorViewImage.color);
        }
        else if (spriteToChange == 2)
        {
            SettingsMainMenu.Instance.SetTrainWindowsColor(colorViewImage.color);
        }
    }

	public void changeSelectedSprite(int number){
		spriteToChange = number;
	}


}
