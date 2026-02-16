using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HREngine.Bots
{
    //法术 圣骑士 费用：2
    //Order in the Court
    //法庭秩序
    //从你的牌库中抽一张法力值消耗最高的牌。
    class Sim_MAW_016 : SimTemplate
	{
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            if (ownplay)
            {
                var deck = p.prozis.turnDeck;
                if (deck.Count == 0) return;

                // 获取有序牌库列表（按卡牌ID稳定排序）
                var orderedDeck = deck.Keys
                    .OrderBy(id => id.ToString())
                    .Select(id => CardDB.Instance.getCardDataFromID(id));

                // 确定最高费用
                int maxCost = orderedDeck.Max(c => c.cost);

                // 选择首个最高费用卡牌
                CardDB.Card targetCard = orderedDeck
                    .FirstOrDefault(c => c.cost == maxCost);

                if (targetCard != null)
                {
                    // 直接抽卡但不修改牌库
                    p.drawACard(targetCard.cardIDenum, true);
                }
            }
        }
    }
}
