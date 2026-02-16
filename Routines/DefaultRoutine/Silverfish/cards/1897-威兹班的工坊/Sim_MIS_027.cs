using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 术士 费用：3
	//Domino Effect
	//多米诺效应
	//Deal $2 damage to a minion. Repeat to the left or right, dealing 1 more damage each time.
	//对一个随从造成$2点伤害。向左侧或右侧重复此效果，每次伤害增加1点。
	class Sim_MIS_027 : SimTemplate
	{

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int damage = 2;
            List<Minion> minions = ownplay ? p.ownMinions : p.enemyMinions;

            // 获取目标随从在列表中的位置
            int index = minions.IndexOf(target);
            if (index == -1) return; // 如果未找到目标，直接返回

            // 根据选择向左或向右递增伤害
            int direction = choice == 1 ? -1 : 1; // choice 1 表示左侧，2 表示右侧
            while (index >= 0 && index < minions.Count)
            {
                Minion currentMinion = minions[index];
                p.minionGetDamageOrHeal(currentMinion, damage);
                damage += 1;
                index += direction;
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),   // 目标必须是随从
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),    // 目标必须是敌方
            };
        }
    }
}
