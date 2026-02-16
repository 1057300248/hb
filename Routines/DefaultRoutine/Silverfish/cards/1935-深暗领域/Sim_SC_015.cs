using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 无效的 费用：3
	//Nydus Worm
	//坑道虫
	//Draw two Zerg cards. They cost (1) less.
	//抽两张异虫牌，这些牌的法力值消耗减少（1）点。
	class Sim_SC_015 : SimTemplate
	{
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            if (ownplay)
            {
                int count = 2;
                foreach (KeyValuePair<CardDB.cardIDEnum, int> cid in p.prozis.turnDeck)
                {
                    CardDB.Card c = CardDB.Instance.getCardDataFromID(cid.Key);
                    if (c.zerg && count > 0)
                    {
                        int cardsToDraw = Math.Min(cid.Value, count);
                        for (int i = 0; i < cardsToDraw; i++)
                        {
                            // 记录抽牌前的手牌数量
                            int beforeDraw = p.owncards.Count;

                            // 执行抽牌动作
                            p.drawACard(cid.Key, true);

                            // 获取刚抽到的卡牌实例
                            if (p.owncards.Count > beforeDraw)
                            {
                                Handmanager.Handcard drawnCard = p.owncards[p.owncards.Count - 1];
                                // 调整手牌实例的费用（不影响原始数据）
                                drawnCard.manacost = Math.Max(0, c.cost - 1);
                            }

                            count--;
                        }
                        if (count <= 0) break;
                    }
                }
            }
        }
    }
}
