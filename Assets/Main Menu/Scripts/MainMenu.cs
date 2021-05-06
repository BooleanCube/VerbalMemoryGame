using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
	public GameObject Menu;
	public GameObject About;
	//Method assigned to the Quit Button to close the application when clicked
	public void QuitGame() { Application.Quit(); }
	//Method assigned to the Start Button to start a game whenever this button is clicked
	public void StartGame() { SceneManager.LoadScene("Game"); }
	//Closes the Menu screen and opens the About screen when the About button is clicked
	public void OpenAboutPage() { Menu.SetActive(false); About.SetActive(true); }
	//Closes the About screen and opens the Menu screen when the Back button is clicked
	public void OpenMenuPage() { About.SetActive(false); Menu.SetActive(true); }
}
