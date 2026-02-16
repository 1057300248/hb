using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 猎人 费用：2
	//Patchwork Pals
	//拼布好朋友
	//Get all 3 Animal Companions. Theycost (1) less.
	//获取全部3种动物伙伴，其法力值消耗减少（1）点。
	class Sim_TOY_353 : SimTemplate
	{
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            addCardToHand(p, CardDB.cardIDEnum.NEW1_034, ownplay); // 霍弗
            addCardToHand(p, CardDB.cardIDEnum.NEW1_033, ownplay); // 米莎
            addCardToHand(p, CardDB.cardIDEnum.NEW1_032, ownplay); // 雷欧克
        }

        private void addCardToHand(Playfield p, CardDB.cardIDEnum cardID, bool ownplay)
        {
            // 获取原始卡牌数据（不修改原始数据）
            CardDB.Card originalCard = CardDB.Instance.getCardDataFromID(cardID);

            // 创建手牌实例并调整费用
            Handmanager.Handcard newHandCard = new Handmanager.Handcard(originalCard);
            newHandCard.manacost = Math.Max(0, originalCard.cost - 1); // 单独设置实例费用

            // 添加到对应玩家的手牌
            if (ownplay) p.owncards.Add(newHandCard);
            else p.enemyAnzCards++;

            p.triggerCardsChanged(ownplay);
        }
    }
}
