using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject selectLevelMenu;
    public GameObject optionsMenu;
    public GameData gameData;

    public Selectitem selectItem;
    // Start is called before the first frame update
    void Awake()
    {
        if(gameData.gameStarted == false)
        {
            gameData.level1 = true;
            gameData.gameStarted = true;
            gameData.saveGame();
        }
        else
        {
            gameData.loadGame();
        }
    }

    private void Start()
    {
        selectLevelMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void SelectLevel()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        selectLevelMenu.SetActive(true);
        selectItem.LoadObjects();
    }
    public void Options()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        optionsMenu.SetActive(true);
    }

    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        optionsMenu.SetActive(false);
    }

    public void BackSelectItem()
    {
        selectItem.ResetList();
        selectLevelMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
