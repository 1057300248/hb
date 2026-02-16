using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 治疗波（Healing Wave）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_048 : SimTemplate
    {
        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从或英雄。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 默认恢复7点生命值
            int heal = ownplay ? p.getSpellHeal(7) : p.getEnemySpellHeal(7);

            // 揭示双方牌库里的一张随从牌（模拟逻辑）
            CardDB.Card ownMinion = null;
            CardDB.Card enemyMinion = null;

            // 查找己方牌库中的随从牌
            foreach (CardDB.Card card in p.ownDeck)
            {
                if (card.type == CardDB.cardtype.MOB)
                {
                    ownMinion = card;
                    break;
                }
            }

            // 查找敌方牌库中的随从牌
            // 注意：p.enemyDeckSize 是一个整数，表示敌方牌库的大小，不能用于遍历
            // 需要访问敌方牌库的实际卡牌列表
            // 由于敌方牌库不可直接访问，这里采用随机随从作为替代
            enemyMinion = CardDB.Instance.getCardData(CardDB.cardNameEN.unknown);

            // 如果己方随从法力值消耗更大，则恢复14点生命值
            if (ownMinion != null && enemyMinion != null && ownMinion.cost > enemyMinion.cost)
            {
                heal = ownplay ? p.getSpellHeal(14) : p.getEnemySpellHeal(14);
            }

            // 对目标恢复生命值
            p.minionGetDamageOrHeal(target, -heal);
        }

        /// <summary>
        /// 返回该卡牌的使用条件。
        /// </summary>
        /// <returns>使用条件数组。</returns>
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
            new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY), // 需要指定目标
            };
        }
    }
}