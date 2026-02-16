using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_WW_336 : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 对敌方所有随从造成3点伤害
            int dmg = (ownplay) ? p.getSpellDamageDamage(3) : p.getEnemySpellDamageDamage(3);
            p.allCharsOfASideGetDamage(!ownplay, dmg);
        }

        public override int GetManaCost(Playfield p, int manaCost)
        {
            // 基础费用为8，每有一个敌方随从减少1点，最低为0
            int enemyMinionCount = p.enemyMinions.Count;
            int adjustedCost = manaCost - enemyMinionCount;
            return adjustedCost > 0 ? adjustedCost : 0;
        }
    }
}
