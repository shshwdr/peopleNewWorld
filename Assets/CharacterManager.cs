using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{

    string[] boyNames = new string[] {
    "Liam",
"Noah",
"Oliver",
"Elijah",
"William",
"James",
"Benjamin",
"Lucas",
"Henry",
"Alexander",
"Mason",
"Michael",
"Ethan",
"Daniel",
"Jacob",
"Logan",
"Jackson",
"Levi",
"Sebastian",
"Mateo",
"Jack",
"Owen",
"Theodore",
"Aiden",
"Samuel",
"Joseph",
"John",
"David",
"Wyatt",
"Matthew",
"Luke",
"Asher",
"Carter",
"Julian",
"Grayson",
"Leo",
"Jayden",
"Gabriel",
"Isaac",
"Lincoln",
"Anthony",
"Hudson",
"Dylan",
"Ezra",
"Thomas",
"Charles",
"Christopher",
"Jaxon",
"Maverick",
"Josiah",
"Isaiah",
"Andrew",
"Elias",
"Joshua",
"Nathan",
"Caleb",
"Ryan",
"Adrian",
"Miles",
"Eli",
"Nolan",
"Christian",
"Aaron",
"Cameron",
"Ezekiel",
"Colton",
"Luca",
"Landon",
"Hunter",
"Jonathan",
"Santiago",
"Axel",
"Easton",
"Cooper",
"Jeremiah",
"Angel",
"Roman",
"Connor",
"Jameson",
"Robert",
"Greyson",
"Jordan",
"Ian",
"Carson",
"Jaxson",
"Leonardo",
"Nicholas",
"Dominic",
"Austin",
"Everett",
"Brooks",
"Xavier",
"Kai",
"Jose",
"Parker",
"Adam",
"Jace",
"Wesley",
"Kayden",
"Silas",
"Bennett",
"Declan",
"Waylon",
"Weston",
"Evan",
"Emmett",
"Micah",
"Ryder",
"Beau",
"Damian",
"Brayden",
"Gael",
"Rowan",
"Harrison",
"Bryson",
"Sawyer",
"Amir",
"Kingston",
"Jason",
"Giovanni",
"Vincent",
"Ayden",
"Chase",
"Myles",
"Diego",
"Nathaniel",
"Legend",
"Jonah",
"River",
"Tyler",
"Cole",
"Braxton",
"George",
"Milo",
"Zachary",
"Ashton",
"Luis",
"Jasper",
"Kaiden",
"Adriel",
"Gavin",
"Bentley",
"Calvin",
"Zion",
"Juan",
"Maxwell",
"Max",
"Ryker",
"Carlos",
"Emmanuel",
"Jayce",
"Lorenzo",
"Ivan",
"Jude",
"August",
"Kevin",
"Malachi",
"Elliott",
"Rhett",
"Archer",
"Karter",
"Arthur",
"Luka",
"Elliot",
"Thiago",
"Brandon",
"Camden",
"Justin",
"Jesus",
"Maddox",
"King",
"Theo",
"Enzo",
"Matteo",
"Emiliano",
"Dean",
"Hayden",
"Finn",
"Brody",
"Antonio",
"Abel",
"Alex",
"Tristan",
"Graham",
"Zayden",
"Judah",
"Xander",
"Miguel",
"Atlas",
"Messiah",
"Barrett",
"Tucker",
"Timothy",
"Alan",
"Edward",
"Leon",
"Dawson",
"Eric",
"Ace",
"Victor",
"Abraham",
"Nicolas",
"Jesse",
"Charlie",
"Patrick",
"Walker",
"Joel",
"Richard",
"Beckett",
"Blake",
"Alejandro",
"Avery",
"Grant",
"Peter",
"Oscar",
"Matias",
"Amari",
"Lukas",
"Andres",
"Arlo",
"Colt",
"Adonis",
"Kyrie",
"Steven",
"Felix",
"Preston",
"Marcus",
"Holden",
"Emilio",
"Remington",
"Jeremy",
"Kaleb",
"Brantley",
"Bryce",
"Mark",
"Knox",
"Israel",
"Phoenix",
"Kobe",
"Nash",
"Griffin",
"Caden",
"Kenneth",
"Kyler",
"Hayes",
"Jax",
"Rafael",
"Beckham",
"Javier",
"Maximus",
"Simon",
"Paul",
"Omar",
"Kaden",
"Kash",
"Lane",
"Bryan",
"Riley",
"Zane",
"Louis",
"Aidan",
"Paxton",
"Maximiliano",
"Karson",
"Cash",
"Cayden",
"Emerson",
"Tobias"};

    public GameObject characterPrefab;
    public List<Character> characterList = new List<Character>();
    public GameObject characterParent;
    Dictionary<string, bool> boyNameUsed = new Dictionary<string, bool>();

    void Awake()
    {
        characterParent = new GameObject("characterParent");
    }
    public void addCharacter()
    {
        //Transform position = characterPositionParent.GetChild(characterList.Count);
        GameObject go = Instantiate(characterPrefab, characterParent.transform);
        string name = "";
        while (true)
        {
            int rand = Random.Range(0, boyNames.Length);
            name = boyNames[rand];
            if (!boyNameUsed.ContainsKey(name))
            {
                boyNameUsed[name] = true;
                break;
            }
        }

        go.GetComponent<Character>().Init(characterList.Count,name);
        characterList.Add(go.GetComponent< Character>());
    }

    List<Character> getCharacterWithAction(CharacterAction action)
    {
        List<Character> res = new List<Character>();
        foreach(Character go in characterList)
        {
            if(go.currentAction == action)
            {
                res.Add(go);
            }
        }
        return res;
    }

    List<Character> getCharacterWithoutScouting()
    {
        List<Character> res = new List<Character>();
        foreach (Character go in characterList)
        {
            if (!go.isScouting)
            {
                res.Add(go);
            }
        }
        return res;
    }

    
    public List<Character> getCharacters()
    {
        switch (GameTurnManager.Instance.currentTurn)
        {
            case GameTurn.player:

                return getCharacterWithoutScouting();
            default:
                return getCharacterWithAction((CharacterAction)((int)GameTurnManager.Instance.currentTurn-1));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
