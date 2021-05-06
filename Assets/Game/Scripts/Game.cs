using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    //A bunch of public variables for me to easily access in the Unity Editor
    public Text scoreText;
    public GameObject gameScreen;
    public GameObject lostScreen;
    public Text livesText;
    public Text wordText;

    //A lot of private variables since I don't need them in the Unity Editor but come in handy for the actual game
    private string currentWord;
    private int lives = 3;
    private ArrayList visited = new ArrayList();
    private int score = 0;
    private string[] words = new string[]{};

    /*As soon as the game is launched when the player clicks "Start" we choose one of the 3 lists or random words
    This is done for just more randomization of the words and so its less confusing between games
    Then we also initialize the currentWord variable and set the wordText to display the currentWord in the game*/
    void Start() {
        System.Random r2 = new System.Random();
        int ri = r2.Next(3);
        if(ri==0) words = words1;
        if(ri==1) words = words2;
        if(ri==2) words = words3;
        System.Random r = new System.Random();
        int index = r.Next(words.Length);
        wordText.text = words[index];
        currentWord = words[index];
    }

    //This method was assigned to the seen button and is executed every time the user clicks the "Seen Button"
    //This method is being called in Unity UI when the user clicks the button, not in the code.
    public void markedSeen() {
        //If lives left equals 0 then we bring up the lost screen
        if(lives-1==0) { gameScreen.SetActive(false); lostScreen.SetActive(true); setLossScreen(); }
        //This changes the lives display to the number of lives the user currently has which changes depending on the user's input
        //This also calls the Contains method I created down below
        if(!Contains(visited, currentWord)) {
            lives--;
            livesText.text = "Lives: "+lives;
        } else {
            //Increases the score if the player is correct
            score++;
        }
        //Randomly generates the next word and puts the value in currentWord and then displays currentWord on wordText
        System.Random r = new System.Random();
        int ind = r.Next(words.Length);
        currentWord = words[ind];
        wordText.text = currentWord;
    }

    //This method was assigned to the new button and is executed every time the user clicks the "New Button"
    //This method is being called in Unity UI when the user clicks the button, not in the code.
    public void markedNew() {
        //If lives left equals 0 then we bring up the lost screen
        if(lives-1==0) { gameScreen.SetActive(false); lostScreen.SetActive(true); setLossScreen(); }
        //This changes the lives display to the number of lives the user currently has which changes depending on the user's input
        //This also calls the Contains method I created down below
        if(Contains(visited, currentWord)) {
            lives--;
            livesText.text = "Lives: "+lives;
        } else {
            //This changes the score and also adds the currentWord to the list of visited/seen words so far.
            score++;
            visited.Add(currentWord);
        }
        //Randomly generates the next word and puts the value in currentWord and then displays currentWord on wordText
        System.Random r = new System.Random();
        int ind = r.Next(words.Length);
        currentWord = words[ind];
        wordText.text = currentWord;
    }

    //This is my own Contains method that iterates through the entire list and searches for the word.
    private bool Contains(ArrayList visited, string currentWord) {
        //Loop through every element in the ArrayList given in the method as a paramter
        for(int i=0; i<visited.Count; i++) {
            //Get the element at index i
            string word = "";
            IEnumerator e = visited.GetEnumerator(i,1);
            e.MoveNext(); word = e.Current.ToString();
            //Check to see if the element in the ArrayList at index i has the same content as the currentWord and if so we return true or else it continues
            if(word==currentWord) return true;
        }
        //If it never returned true then it will complete iterating through the loop which means visited doesn't contain currentWord so we return false.
        return false;
    }

    private void setLossScreen() {
        //Gets the high score from the PlayerPrefs database which stores the data even if the game has been close or not running. If there is no data in the database then it returns the defaultValue which I have set to 0 in the method.
        int highScore = PlayerPrefs.GetInt("Score", 0);
        //Checks if score is greater than high score and if so then we change the high score to score and we change the high score in the database to score as well so we can remember it.
        if(score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("Score", highScore);
        }
        //Edits the scoreText variale to display the current score and the high score.
        scoreText.text = "Your score is: " + score + "\nYour High Score is: " + highScore;
    }

    //If the user clicks the Retry Button it will send him back to the Main Menu scene in which he will have to click the Start button again to start another game.
    //This method is being called by Unity UI when Retry button has been clicked.
    public void restartGame() {
        SceneManager.LoadScene("Main Menu");
    }

    //These are the lists of words that were randomly generated and put into a list over here.
    //https://www.thewordfinder.com/random-word-generator/?msclkid=338fbe3dad971610b758de014a01e69b
    //If there are any indecent words then I am truly sorry about that. I did check through each and every word to check if there were any such words manually so I might have missed a few.
    private string[] words1 = {"sounding", "whistle-blower", "preserve", "restoring", "agent", "lament", "mumble", "closing", "blanch", "intuitive", "age", "councilman", "ideological", "middle-income", "vet", "fill", "assumed", "chronicle", "axe", "wind", "amazed", "automotive", "melodic", "suffuse", "completion", "respectable", "reassemble", "collapsed", "pipeline", "parmesan", "gravitational", "odd", "lined", "correction", "change", "loaded", "blessed", "insufficient", "repeat", "strainer", "mourn", "unwelcome", "right-hand", "program", "deprive", "veracity", "sham", "foodstuff", "fuss", "rodent", "immersion", "versatile", "dimensional", "two-time", "enumerate", "hash", "alienation", "broken", "earmark", "thesis", "shout", "knapsack", "hydrogen", "controlling", "avatar", "drowning", "dwindle", "sit", "futuristic", "useful", "astonished", "intern", "spin", "dismount", "sculpted", "compassionate", "top", "affordable", "smarts", "haven", "asphalt", "jumping", "venom", "scent", "elliptical", "hound", "tally", "extol", "acknowledgement", "smoke", "waterway", "shiver", "threadbare", "unable", "benefit", "angelic", "assessor", "wetland", "commander"};
    private string[] words2 = {"inverse", "nickname", "decor", "commanding", "planting", "ascending", "shuffling", "taunt", "stock", "undermine", "busted", "petition", "consolidated", "gorge", "utilize", "valve", "confident", "dormitory", "core", "iconography", "afflict", "carry", "notch", "hospital", "persevere", "skewer", "psychologist", "uniform", "curious", "resultant", "racketeering", "priceless", "realtor", "rehab", "obedient", "bottom", "totality", "bored", "impeccable", "pathologist", "stationary", "everlasting", "punishment", "equalize", "executive", "god", "innocence", "neoliberal", "biosolids", "construct", "swatch", "diverge", "premier", "candlestick", "earphone", "impact", "libido", "lard", "tail", "charge", "knockout", "soothe", "profit", "bottom", "counselor", "collage", "aspen", "orphaned", "demo", "chowder", "motorcycle", "feel-good", "frown", "corner", "zealot", "tactical", "analyzer", "derive", "dopamine", "purification", "waste", "swell", "hash", "deck", "abolish", "icing", "longing", "irrigation", "garish", "eternity", "average", "term", "dashgin", "sloth", "dishes", "various", "parliamentary", "glitch", "broadcasting", "storm"};
    private string[] words3 = {"selfless", "adequate", "deodorant", "erosion", "thermometer", "one-hour", "functionality", "latex", "passing", "lemonade", "proclaim", "devoid", "pregame", "mantle", "bounty", "remedy", "separate", "range", "devaluation", "granola", "inferiority", "screen", "retire", "hazy", "plaintiff", "real-time", "grit", "crushing", "cordial", "shelve", "whistling", "reformer", "vendetta", "uncertainty", "centerpiece", "mitten", "lithe", "pistol", "inconsistent", "mortuary", "unregulated", "battlefield", "swan", "potato", "fussy", "shocking", "sought", "universe", "crashing", "schooling", "calm", "macroeconomic", "row", "group", "incredible", "fit", "gleam", "faraway", "clap", "wiretap", "add", "clinic", "watery", "abomination", "slip", "inspiration", "prompt", "muslim", "guard", "yell", "bloated", "midlife", "drastic", "funny", "overdo", "corporate", "hypnosis", "unpredictability", "headline", "horseshoe", "needle", "mention", "hull", "constituency", "messy", "manner", "hook", "receive", "caustic", "accolade", "interrogate", "pry", "standardize", "pre-eminent", "rumble", "drumming", "deceptive", "telecast", "finder", "messaging"};

}
