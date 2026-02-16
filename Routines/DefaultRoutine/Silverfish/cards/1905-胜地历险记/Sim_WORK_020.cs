using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 猎人 费用：4
	//Monkey Business
	//业务支猿
	//Add 8 Bananas to your hand. Any that can't fit are randomly fed to friendly minions in play.
	//将8根香蕉置入你的手牌。放不下的香蕉会随机喂给场上的友方随从。
	class Sim_WORK_020 : SimTemplate
	{

        CardDB.Card banana = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_014t); // 香蕉的卡牌ID为EX1_014t

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int bananasToAdd = 8; // 添加8根香蕉
            int handLimit = 10;   // 手牌上限为10张
            int cardsInHand = ownplay ? p.owncards.Count : p.enemyAnzCards; // 获取当前手牌数量
            int availableSlots = handLimit - cardsInHand; // 计算可容纳的手牌数量

            // 计算需要直接添加到手牌中的香蕉数量
            int bananasInHand = Math.Min(availableSlots, bananasToAdd);
            // 计算需要喂给随从的香蕉数量
            int bananasToFeed = bananasToAdd - bananasInHand;

            // 将香蕉加入手牌
            for (int i = 0; i < bananasInHand; i++)
            {
                p.drawACard(CardDB.cardIDEnum.EX1_014t, ownplay, true); // 添加香蕉到手牌
            }

            // 如果有多余的香蕉，随机喂给场上的友方随从
            if (bananasToFeed > 0)
            {
                List<Minion> friendlyMinions = ownplay ? p.ownMinions : p.enemyMinions; // 获取友方随从列表

                for (int i = 0; i < bananasToFeed; i++)
                {
                    if (friendlyMinions.Count > 0)
                    {
                        Minion randomMinion = friendlyMinions[p.getRandomNumber(0, friendlyMinions.Count - 1)]; // 随机选择一个友方随从
                        p.minionGetBuffed(randomMinion, 1, 1); // 给该随从+1/+1效果（香蕉效果）
                    }
                }
            }
        }
    }
}
