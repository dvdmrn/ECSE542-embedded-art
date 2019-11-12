using UnityEngine;
using UnityEngine.UI;

public class GuiDepthForce : MonoBehaviour
{

    public int guiDepth;
    public bool continuous;

    void OnGUI(){
        GUI.depth = guiDepth;

    }

    void Update(){
        if(continuous){
            GUI.depth = guiDepth;
        }
    }
}
