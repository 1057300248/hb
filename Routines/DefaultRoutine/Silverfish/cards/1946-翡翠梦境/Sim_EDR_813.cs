using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //法术 巫妖王 费用：1
    //Morbid Swarm
    //病变虫群
    //<b>Choose One -</b> Summon two 1/1 Ants; or Spend 2 <b>Corpses</b> to deal $4_damage to a minion.
    //<b>抉择：</b>召唤两只1/1的蚂蚁；或者消耗2份<b>残骸</b>，对一个随从造成$4点伤害。
    class Sim_EDR_813 : SimTemplate
    {
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EDR_813at);

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {

            if (choice == 1 || (p.ownFandralStaghelm > 0 && ownplay))
            {
                int place = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
                p.callKid(kid, place, ownplay, false);
                p.callKid(kid, place, ownplay);
                //p.evaluatePenality -= 3;
            }

            if (choice == 2 || (p.ownFandralStaghelm > 0 && ownplay))
            {
                // 获取当前可用的尸体数量
                int availableCorpses = p.getCorpseCount();
                int dmg = (ownplay) ? p.getSpellDamageDamage(4) : p.getEnemySpellDamageDamage(4);
                // 检查是否有足够的尸体
                if (availableCorpses >= 2)
                {
                    // 消耗 2 具尸体
                    p.本局消耗尸体数 += 2;
                    p.minionGetDamageOrHeal(target, dmg);
                    //p.evaluatePenality -= 60;
                }
            }
        }
    }
}
