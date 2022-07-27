using UnityEngine;

[CreateAssetMenu(menuName = "LevelObject")]
public class LevelData : ScriptableObject
{
    //Items Properties
    public string sceneName;
    public Sprite sceneImage;
    public int sceneLoad;
    public bool isUnloked;
}