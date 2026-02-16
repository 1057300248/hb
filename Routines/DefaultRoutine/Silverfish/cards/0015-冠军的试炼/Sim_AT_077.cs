using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{

        /// <summary>
        /// 白银之枪（Argent Lance）卡牌的模拟实现。
        /// </summary>
        class Sim_AT_077 : SimTemplate
        {
            // 获取武器卡牌数据
            private readonly CardDB.Card weapon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.AT_077);

            /// <summary>
            /// 当卡牌被使用时触发的事件处理方法。
            /// </summary>
            /// <param name="p">当前游戏局面。</param>
            /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
            /// <param name="target">目标随从（如果需要）。</param>
            /// <param name="choice">选择的选项（如果有）。</param>
            public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
            {
                // 装备白银之枪武器
                p.equipWeapon(weapon, ownplay);

                // 揭示双方牌库里的一张随从牌（模拟逻辑）
                CardDB.Card ownMinion = null;
                CardDB.Card enemyMinion = null;

                // 查找己方牌库中的第一张随从牌
                foreach (CardDB.Card card in p.ownDeck)
                {
                    if (card.type == CardDB.cardtype.MOB)
                    {
                        ownMinion = card;
                        break;
                    }
                }

                // 查找敌方牌库中的第一张随从牌
                foreach (CardDB.Card card in p.enemyDeck)
                {
                    if (card.type == CardDB.cardtype.MOB)
                    {
                        enemyMinion = card;
                        break;
                    }
                }

                // 如果己方随从法力值消耗更大，则增加武器耐久度
                if (ownMinion != null && enemyMinion != null && ownMinion.cost > enemyMinion.cost)
                {
                    if (ownplay)
                        p.ownWeapon.Durability++;
                    else
                        p.enemyWeapon.Durability++;
                }
            }
        }
    
}