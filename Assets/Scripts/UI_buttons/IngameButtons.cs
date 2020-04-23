using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameButtons : MonoBehaviour
{
	public void ToSettingsMenu()
	{
		SceneManagerExt.LoadScene_u(Macro.IDX_SETTINGSMENU, LoadSceneMode.Additive);
		GameLoop.State = GameState.settingsmenuState;
	}
}
