using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HuntTurnView : TurnView
{
    MonsterGroup[] monsterGroups;

    public GameObject cancelButton;
    public GameObject attackButton;

    public int winSanityLoss = 5;
    public int lossSanityLoss = 15;
    MonsterGroup selectedGroup;
    int[] totalReward;
    public override void startTurnView()
    {
        //monster anim in
        loadTutorials();
        nextButton.SetActive(false);
        uiPanel.SetActive(true);

        updateDescriptionText();

        view.SetActive(true);
        totalReward = new int[] { };
        hideRelatedCharacters();
        showMonsterGroups();
        cancelButton.SetActive(true);
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            var character = relatedCharacters[i];
            if (character.isDead)
            {
                continue;
            }
            character.temporaryLeave = false;
            character.GetComponent<Collider2D>().isTrigger = false;
        }

        MusicManager.Instance.playBattle();
    }


    protected override void loadTutorials()
    {

        TutorialManager.Instance.unlockAction((int)CharacterAction.rest);
        TutorialManager.Instance.showTutorialPanel(TutorialManager.tutorialTurnIntro_Hunt);
    }

    public void onClickCancelButton()
    {
        Popup.Instance.Init("Do you want to cancel the hunt?", () =>
        {
            gameOver(3);
        });
        //Debug.Log("cancel");
        //stopTurnView();
    }

    public void onAttackButton()
    {
        ControlManager.Instance.cleanPopups();
        attackButton.SetActive(false);
        StartCoroutine(playerAttack());
    }

    int checkGameOver() // 1 = all player die 2 = all enemy die
    {
        bool playerAllDead = true;
        bool enemyAllDead = true;
        var monsters = selectedGroup.monsters;
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            var character = relatedCharacters[i];
            if (character.isDead)
            {
                continue;
            }
            if (character.temporaryLeave)
            {
                continue;
            }
            playerAllDead = false;
            break;
        }
        for (int i = 0; i < monsters.Count; i++)
        {
            var character = monsters[i];
            if (character.isDead)
            {
                continue;
            }
            enemyAllDead = false;
            break;
        }
        if (playerAllDead == true)
        {
            return 1;
        }
        if (enemyAllDead)
        {
            return 2;
        }
        return 0;
    }
    IEnumerator playerAttack()
    {
        var monsters = selectedGroup.monsters;


        for (int i = 0; i < monsters.Count; i++)
        {

            var monster = monsters[i].GetComponent<Monster>();
            if (monster.isDead)
            {
                continue;
            }
            //monster.transform.DOShakePosition(0.7f);
            var characterID = Utils.findClosestIndex(monster.transform, relatedCharacters);
            var targetCharacter = relatedCharacters[characterID];

            //check if monster can avoid
            var monsterAvoid = targetCharacter.avoidRate;
            var characterHit = monster.hitRate;
            var avoidRate = monsterAvoid * characterHit;
            monster.animator.SetTrigger("attack");
            float rand = Random.Range(0f, 1f);
            if (rand < avoidRate)
            {
                //hit

                targetCharacter.doDamage(monster.attack);
                //ControlManager.Instance.createPopupUI("-" + monster.attack, targetCharacter.transform.position,Color.red);
                targetCharacter.temporaryLeave = true;

            }
            else
            {

                ControlManager.Instance.createPopupUI("Miss", targetCharacter.transform.position,Color.yellow);
            }

            // targetCharacter.transform.DOShakePosition(0.9f,1f,5);
            //targetCharacter.transform.DOPunchScale(Vector3.one, 0.9f, 10, 0.5f);
            yield return new WaitForSeconds(1);

            if (rand < avoidRate)
            {

                //character go away

                yield return new WaitForSeconds(0.5f);
                targetCharacter.transform.DOMove(targetCharacter.transform.position + new Vector3(20, Random.Range(-10, 10), 0), 1);

                SFXManager.Instance.playSFXRandom(SFXManager.Instance.loseBattle);
                yield return new WaitForSeconds(1);
            }

            int gameover = checkGameOver();
            if (gameover > 0)
            {
                gameOver(gameover);
                yield break;
            }
        }
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            var character = relatedCharacters[i];
            if (character.isDead)
            {
                continue;
            }
            if (character.temporaryLeave)
            {
                continue;
            }
            //character.transform.DOShakePosition(0.7f);
            SFXManager.Instance.playSFXRandom(SFXManager.Instance.attackShake);
            var monsterID = Utils.findClosestIndex(character.transform, monsters);
            var targetMonster = monsters[monsterID];

            //check if monster can avoid
            var monsterAvoid = targetMonster.avoidRate;
            var characterHit = character.hitRate;
            var avoidRate = monsterAvoid * characterHit;

            float rand = Random.Range(0f, 1f);
            if (rand < avoidRate)
            {
                //hit

                if (Inventory.Instance.getItemAmount(InventoryItem.weapon) >= 1)
                {

                    character.animator.SetTrigger("attackS");
                }
                else
                {

                    character.animator.SetTrigger("attack");
                }

                targetMonster.doDamage(character.attack);
                ControlManager.Instance.createPopupUI("-" + character.attack, targetMonster.transform.position, Color.red);
            }
            else
            {

                ControlManager.Instance.createPopupUI("Miss", targetMonster.transform.position, Color.yellow);
            }


            //targetMonster.transform.DOPunchScale(Vector3.one, 0.9f, 10, 0.5f);//( DOShakePosition(0.9f);
            if (targetMonster.isDead)
            {
                CityManager.Instance.killedMonster( targetMonster.info.id);
                addReward(targetMonster);
            }
            int gameover = checkGameOver();
            if (gameover > 0)
            {
                gameOver(gameover);
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
        finishAttack();
    }

    void finishAttack()
    {
        attackButton.SetActive(true);
        cancelButton.SetActive(true);
    }

    void gameOver(int res)
    {

        
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            relatedCharacters[i].showStatus(CharacterStatus.sanity);
            if(res == 2)
            {

                relatedCharacters[i].decreaseStatus(CharacterStatus.sanity, winSanityLoss);
                var affectAbility =  Random.Range(0, 4);
                relatedCharacters[i].increaseAbility((CharacterAbility) affectAbility, 2);
            }
            else
            {

                relatedCharacters[i].decreaseStatus(CharacterStatus.sanity, lossSanityLoss);
            }
        }


        CSDialogueManager.Instance. addDialogue(1);
        if (res == 1)
        {
            //all character die
            descriptionText.text = "You lose the battle, you feel bad about it. ";
        }
        else if (res == 2)
        {
            // all enemy die
            descriptionText.text = "You won! You feel bad for the animals but you need to survive.";
        }
        else if (res == 3)
        {
            //cancel
            descriptionText.text = "Cancelled the hunt. you feel bad because you might get hungry because of not geting enough food. ";
        }
        string rewardString = Inventory.Instance.inventoryItemsToString(totalReward);
        if (rewardString.Length > 0)
        {
            descriptionText.text += "You got " + rewardString + " in this battle!";

        }

        attackButton.SetActive(false);
        cancelButton.SetActive(false);
        //show reward
        nextButton.SetActive(true);

        foreach (var chara in relatedCharacters)
        {
            chara.stopBattle();
        }
    }

    public override void stopTurnView()
    {
        base.stopTurnView();
        if (selectedGroup)
        {

            selectedGroup.Clear();
        }
        foreach (var g in monsterGroups)
        {
            g.gameObject.SetActive(false);
        }

        foreach (var character in relatedCharacters)
        {
            character.GetComponent<Collider2D>().isTrigger = true;
        }

        MusicManager.Instance.playNormal();
    }


    public void onClickNextButton()
    {
        //show pop up
        Debug.Log("");
    }
    protected override void updateDescriptionText()
    {
        descriptionText.text = "select a gruop of monsters to fight.";
    }

    void showMonsterGroups()
    {
        foreach (var g in monsterGroups)
        {
            g.gameObject.SetActive(true);
        }
        //create monster group
        int gi = 0;
        var monstersInfo = CityManager.Instance.currentCityInfo().monsters;
        var maxMonstersInfo = CityManager.Instance.currentCityInfo().maxMonsterNumber;
        //int[] monsterGroups = new int[monstersInfo.Length];
        for (int i = 0; i < monstersInfo.Length; i++)
        {
            if(i>= monstersInfo.Length)
            {
                i = i;
            }
            if (i<monstersInfo.Length&& monstersInfo[i] > 0)
            {
                int rand = Random.Range(1, Mathf.Min(maxMonstersInfo[i], monstersInfo[i]) + 1);
                //monsterGroups[i] = rand;
                monsterGroups[gi].gameObject.SetActive(true);
                monsterGroups[gi].Init(i, rand, this);
                gi++;
            }
        }
        foreach (var chara in relatedCharacters)
        {
            chara.startBattle();
        }
    }

    public void selectMonsterGroup(MonsterGroup group)
    {
        descriptionText.text = "drag characters close to the enemy to attack.";
        foreach (var g in monsterGroups)
        {
            if (g != group)
            {
                g.Clear();
            }
            else
            {
                g.transform.DOMove(new Vector3(0, 0, 0), 1);
                g.transform.DOScale(new Vector3(2, 2, 2), 1);
                //g.transform.localScale = new Vector3(2, 2, 2);
                //g.transform.position = new Vector3(0, 0, 0);

                setCharactersPosition();
                StartCoroutine(moveCharacters());
                showRelatedCharacters();
            }
        }

        selectedGroup = group;


    }

    protected override void afterMoveCharacter()
    {
        //base.afterMoveCharacter();
        cancelButton.SetActive(true);
        attackButton.SetActive(true);
        for (int i = 0; i < relatedCharacters.Count; i++)
        {
            relatedCharacters[i].showStatus(CharacterStatus.health);
        }
    }

    void addReward(Monster mon)
    {
        var reward = mon.info.reward;
        string inventoryString = Inventory.Instance.inventoryItemsToString(reward);

        descriptionText.text = "Killed monster " + mon.name + " get " + inventoryString;
        Inventory.Instance.addItems(reward);
        totalReward = Utils.arrayAggregasion(totalReward, reward);
    }

    void generateCharactersAround()
    {
        foreach (var chara in relatedCharacters)
        {

        }
    }

    private void Awake()
    {
        monsterGroups = GetComponentsInChildren<MonsterGroup>();
        foreach (var g in monsterGroups)
        {
            g.gameObject.SetActive(false);
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
