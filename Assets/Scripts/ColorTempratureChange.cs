using UnityEngine.PostProcessing;

public class ColorTempratureChange : UnityEngine.MonoBehaviour {

    public PostProcessingProfile ppProfile;
   
    float speedLowBound = 0;
    float speedUpBound = 1;

    float tempLowBound = -5;
    float tempUpBound = 5;

    float bloomLowBound = 0;
    float bloomUpBound = 0.5f;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update() {
        float _speed = ControllerInputHandler.instance.speed; // input 0 to 1 value
        float speedcoef = ((_speed - speedLowBound) / (speedUpBound - speedLowBound)); //speed coeficient
        //COLOR TEMP
        if (ppProfile.colorGrading.enabled == true) {
            ColorGradingModel.Settings colorSettings = ppProfile.colorGrading.basicsettings;
            colorSettings.basic.temperature = speedcoef 
                * (tempLowBound - (tempUpBound)) + tempUpBound; // converts 0 to 1 scale to -100 to 100 scale 
            ppProfile.colorGrading.basicsettings = colorSettings;//(in this case limited to -5 to 5)
            
        }
        //BLOOM
        if (ppProfile.bloom.enabled == true) {
            BloomModel.Settings bloomSettings = ppProfile.bloom.settings;
            bloomSettings.bloom.intensity = speedcoef * (bloomLowBound - (bloomUpBound)) + bloomUpBound; //0 to 0.5
            ppProfile.bloom.settings = bloomSettings;
        }
        //OTHER POST PROCCESS RELATED EFFECTS
    }

}
