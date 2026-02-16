using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HREngine.Bots
{
	//随从 法师 费用：4 攻击力：4 生命值：4
	//Watercolor Artist
	//水彩美术家
	//<b>Battlecry:</b> Draw aFrost spell. At the startof your turns, reduceits Cost by (1).
	//<b>战吼：</b>抽一张冰霜法术牌，在你的回合开始时，其法力值消耗减少（1）点。
	class Sim_TOY_376 : SimTemplate
	{

        public override void getBattlecryEffect(Playfield p, Minion ownMinion, Minion target, int choice)
        {
            // 遍历牌库中的卡牌，查找是否有冰霜法术
            bool hasFrostSpell = false;
            foreach (CardDB.Card card in p.ownDeck)
            {
                if (card.type == CardDB.cardtype.SPELL && card.SpellSchool == CardDB.SpellSchool.FROST)
                {
                    hasFrostSpell = true;
                    break; // 找到后立即停止搜索
                }
            }

            // 如果找到冰霜法术牌，进行抽取
            if (hasFrostSpell)
            {
                p.drawACard(CardDB.cardIDEnum.None, ownMinion.own);
                p.watercolorArtistHand.Add(p.owncards.LastOrDefault());
            }
        }

        public override void onTurnStartTrigger(Playfield p, Minion m, bool ownTurn)
        {
            // 只有在随从控制者的回合开始时触发
            if (m.own == ownTurn && p.watercolorArtistHand.Count > 0)
            {
                foreach (var item in p.watercolorArtistHand)
                {
                    // 减少冰霜法术牌的费用，最小值为0
                    item.manacost = Math.Max(0, item.manacost - 1);
                }
                p.watercolorArtistHand.Clear();
            }
        }
    }
}
