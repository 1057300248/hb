using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 圣骑士 费用：5 攻击力：3 生命值：8
    //Sanc'Azel
    //圣沙泽尔
    //<b>Rush</b>After this attacks, turninto a location.
    //<b>突袭</b>在本随从攻击后，变成地标。
    class Sim_VAC_923 : SimTemplate
    {
        // 实现攻击后变成地标的效果
        public override void onTurnEndsTrigger(Playfield p, Minion m, bool turnEndOfOwner)
        {
            // 检查是否是我们的卡牌，并且在本回合攻击过
            if (m.handcard.card.cardIDenum == CardDB.cardIDEnum.VAC_923 && !m.silenced && m.numAttacksThisTurn >= 1)
            {
                // 将随从变成地标
                p.minionTransform(m, CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.VAC_923t));
            }
        }
    }
}