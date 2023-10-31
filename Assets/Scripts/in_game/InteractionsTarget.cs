using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionsTarget : ItemReceiver
{
    public GameObject target;
    private ICanDie target_CanDie;
    private IHaveStatusEffects target_HasStatusEffects;
    private ItemManager ourItemSolver;
    public UnityEvent onInteract;

    private void Start()
    {
        ourItemSolver = GameManager.Instance.ref_ItemSolver;
        if (target != null)
        {
            target_CanDie = target.GetComponent<ICanDie>();
            target_HasStatusEffects = target.GetComponent<IHaveStatusEffects>();
        }


    }

    public override void Receive(ItemDefiner _interactWith)
    {
        if (GameManager.Instance.ref_Stats.Stamina < _interactWith.StaminaConsumption)
        {
            print("NOT ENOUGH STAMINA");
            return;
        }
        print(name + " receives " + _interactWith.name);
        onInteract.Invoke();
        if (target_CanDie != null)
        {
            print("target implements candie");
            int _getDamage = _interactWith.DamageOutput;
            if (_getDamage > 0) target_CanDie.Hurt(_getDamage);
        }

        if (target_HasStatusEffects != null)
        {
            print("target implements status effects");
            Global.statusEffect statusEffect = ourItemSolver.return_statusEffect(_interactWith.name);

            if (statusEffect == Global.statusEffect.Burn) target_HasStatusEffects.Burn();
            else if (statusEffect == Global.statusEffect.Electrocute) target_HasStatusEffects.Electrocute();
            else if (statusEffect == Global.statusEffect.Stun) target_HasStatusEffects.Stun();
            else if (statusEffect == Global.statusEffect.Wet) target_HasStatusEffects.Wet();
        }
        GameManager.Instance.ref_Stats.Stamina_Modify(-_interactWith.StaminaConsumption);

    }

}
