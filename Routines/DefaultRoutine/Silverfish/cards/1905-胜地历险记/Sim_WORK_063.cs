using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HREngine.Bots
{
	//随从 法师 费用：5 攻击力：4 生命值：5
	//Portalmancer Skyla
	//传送门操控师斯奇拉
	//[x]<b>Rogue Tourist</b><b>Battlecry:</b> Swap the Costs ofthe lowest and highest Costspells in your hand.
	//<b>潜行者游客</b><b>战吼：</b>使你手牌中法力值消耗最低和最高的法术牌的法力值消耗互换。
	class Sim_WORK_063 : SimTemplate
	{
        public override void getBattlecryEffect(Playfield p, Minion ownMinion, Minion target, int choice)
        {
            // 找到手牌中所有的法术牌
            var spellsInHand = p.owncards.Where(hc => hc.card.type == CardDB.cardtype.SPELL).ToList();

            // 如果手牌中没有法术牌或者只有一张法术牌，不做任何操作
            if (spellsInHand.Count < 2)
            {
                return;
            }

            // 按法力值排序法术牌，获取最低和最高法力值的法术牌
            var lowestCostSpell = spellsInHand.OrderBy(hc => hc.manacost).First();
            var highestCostSpell = spellsInHand.OrderByDescending(hc => hc.manacost).First();

            // 交换它们的法力值
            int tempCost = lowestCostSpell.manacost;
            lowestCostSpell.manacost = highestCostSpell.manacost;
            highestCostSpell.manacost = tempCost;
        }
    }
}
