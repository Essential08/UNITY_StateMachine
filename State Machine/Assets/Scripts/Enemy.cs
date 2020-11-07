using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Dit zijn de verschillende states die de enemy heeft. LOOKFOR zoekt de speler. GOTO zorgt ervoor dat de enemy naar de player doe gaat. En Attack state zorgt ervoor dat de enemy de speler aanvalt!
    /// </summary>
    public enum State
    {
        LOOKFOR,
        GOTO,
        ATTACK,
    }

    /// De state die een method kan hebben
    public State CurState;
    /// de snelheid van de enemy.
    public float Speed = .5f;
    /// De distance tussen de speler en de enemy die gehaald moet worden voordat de enemy verandert van state
    public float GoToDistance = 13;
    /// Dit is de distance die enemy met de speler moet zijn voor dat de enemy kan attacken
    public float AttackDistance = 4;
    /// De timer die ervoor zorgt wanneer de enemy kan attacken. 
    public float AttackTimer = 2;

    /// huldige tijd.
    private float CurTime;
    /// Hier wordt de speler script aangegeven zodat deze later aangevraagd kan worden.
    private Player PlayerScript;

    public Transform Target;

    /// Hier geven we aan wie de speler is. De tag zit in de Player in Unity.
    public string PlayerTag = "Player";


    /// Start is called before the first frame update
    /// IEnumerator zorgt ervoor dat het de heletijd blijft runnen. 
    IEnumerator Start()
    {
        //Start
        /// Hier zeggen we wie de target is van de enemy. Daar was de PlayerTag voor.
        Target = GameObject.FindGameObjectWithTag(PlayerTag).transform;
        CurTime = AttackTimer;

        /// Als er een target is dan zullen we de PlayerScript ophalen.
        if(Target != null)
        {
            PlayerScript = Target.GetComponent<Player>();
        }

        /// Dit is de loop die zorgt dat de verschillende states laat zien. Dit is de statemachine. Elke keer dat de state verandert zorgt de case ervoor dat je naar de bijbehordende method gaat.
        while (true)
        {
            //Update
            switch (CurState)
            {
                case State.LOOKFOR:
                    LookFor();
                    break;
                case State.GOTO:
                    GoTo();
                    break;
                case State.ATTACK:
                    Attack();
                    break;
            }

            yield return 0;
        }
    }

    /// <summary>
    /// Bij Lookfor() zorgen we ervoor dat de enemmy berekent hoever hij staat van de speler en of hij hem kan benaderen. Als hij hem kan benaderen zal de state veranderen naar GOTO
    /// Voor mijn eigen experiment heb ik er ook voor gekozen om te printen wat de enemy nu doet in de console.
    /// </summary>
    void LookFor()
    {
        print("Looking for player");
        /// We zullen naar onze target kijken. De functie zit in Unity.
        transform.LookAt(Target);
        if (Vector3.Distance(Target.position, transform.position) < GoToDistance)
        {
            CurState = State.GOTO;
        }
    }

    /// <summary>
    /// Als de speler in de radius komt van de enemy zal de enemy naar de speler zijn position komen. Maar als we al dichtbij genoeg zijn dat hoeven we niet meer naar de speler positie en gaan we direct naar de ATTACK state.
    /// </summary>
    void GoTo()
    {
        print("Going to player");
        /// We zullen naar onze target kijken. De functie zit in Unity.
        transform.LookAt(Target);
        if (Vector3.Distance(Target.position, transform.position) > AttackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
        else
        {
            CurState = State.ATTACK;
        }
    }

    /// <summary>
    /// Als we dichtbij genoeg zijn zullen we gaan beginnen met attacken. Dit gebeurt met een Timer. Als de timer onder de 0 komt zal de speler health verliezen. Dus hoelang de enemy bij de speler staat.
    /// Als de attackdistance te groot wordt zullen we weer veranderen van state en gaan we weer naar GOTO.
    /// </summary>
    void Attack()
    {
        print("Attacking player'");
        /// We zullen naar onze target kijken. De functie zit in Unity.
        transform.LookAt(Target);
        CurTime = CurTime - Time.deltaTime;

        if (CurTime < 0)
        {
            PlayerScript.health--;
            CurTime = AttackTimer;
        }

        if (Vector3.Distance(Target.position, transform.position) > AttackDistance)
        {
            CurState = State.GOTO;
        }
    }


}

