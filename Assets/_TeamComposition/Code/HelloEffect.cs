using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.GameModes;
using UnityEngine;

public class HelloEffect : MonoBehaviour
{
    Player owner;
    public int damage = 30;
    void Start()
    {
        owner = GetComponentInParent<Player>();
        GameModeManager.AddHook(GameModeHooks.HookBattleStart, (_) => Trigger());
    }

    void OnDestroy()
    {
        GameModeManager.RemoveHook(GameModeHooks.HookBattleStart, (_) => Trigger());

    }


    public IEnumerator Trigger()
    {
        if (owner.data.view.IsMine)
        {
            foreach (Player player in PlayerManager.instance.players)
            {
                if(player != owner)
                {
                    player.data.healthHandler.CallTakeDamage(Vector2.down * damage, owner.transform.position, damagingPlayer: owner);
                }
            }
        }
        yield break;
    }

}
