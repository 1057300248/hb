using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{

        /// <summary>
        /// 亡者归来（Anyfin Can Happen）卡牌的模拟实现。
        /// </summary>
        class Sim_LOE_026 : SimTemplate
        {
            /// <summary>
            /// 当卡牌被使用时触发的事件处理方法。
            /// </summary>
            /// <param name="p">当前游戏局面。</param>
            /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
            /// <param name="target">目标随从或英雄（如果需要）。</param>
            /// <param name="choice">选择的选项（如果有）。</param>
            public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
            {
                // 获取召唤位置
                int position = ownplay ? p.ownMinions.Count : p.enemyMinions.Count;

                // 获取墓地中的鱼人
                var graveyard = ownplay ? Probabilitymaker.Instance.ownGraveyard : Probabilitymaker.Instance.enemyGraveyard;

                // 限制最多召唤7个随从
                if (position > 6) return;

                CardDB.Card card;
                foreach (var entry in graveyard)
                {
                    card = CardDB.Instance.getCardDataFromID(entry.Key);
                    // 只召唤鱼人或融合怪
                    if (card.race == CardDB.Race.MURLOC || card.race == CardDB.Race.ALL)
                    {
                        // 召唤鱼人或融合怪
                        p.callKid(card, position, ownplay, false);
                        position++;

                        // 如果已召唤7个随从，则停止
                        if (position > 6) break;

                        // 如果该随从死亡多次，则继续召唤
                        if (entry.Value > 1)
                        {
                            p.callKid(card, position, ownplay, false);
                            position++;

                            if (position > 6) break;
                        }
                    }
                }
            }
        }
}