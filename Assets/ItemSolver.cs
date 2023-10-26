using UnityEngine;

public class ItemSolver : MonoBehaviour
{
    public int return_DamageOutput(string toTest)
    {
        int toReturn = 0;

        switch (toTest)
        {
            case"Axe":
                toReturn = 2;
                break;
            case "Pistol":
                toReturn = 2;
                break;
            default:
                toReturn = 0;
                break;
        }
        return toReturn;
    }
    
    public Global.statusEffect return_statusEffect(string toTest)
    {
        Global.statusEffect toReturn = Global.statusEffect.Nothing;

        switch (toTest)
        {
            case "Torch":
                toReturn = Global.statusEffect.Burn;
                break;
            case "Rock":
                toReturn = Global.statusEffect.Stun;
                break;
            default:
                break;
        }
        return toReturn;
    }
}
