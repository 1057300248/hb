using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HREngine.Bots
{
    //随从 恶魔猎手 费用：5 攻击力：6 生命值：5
    //Window Shopper
    //橱窗看客
    //[x]<b>Miniaturize</b><b>Battlecry:</b> <b>Discover</b> aDemon. Set its stats andCost to this minion's.
    //<b>微缩</b><b>战吼：</b><b>发现</b>一张恶魔牌，将其属性值与法力值消耗变为与本随从相同。
    class Sim_TOY_652 : SimTemplate
    {

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {

            var card = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.ETC_409);

            // 设置该随从的属性值与当前随从相同
            card.Attack = own.Angr;
            card.Health = own.Hp;
            card.cost = own.handcard.card.cost;
            p.drawACard(card.cardIDenum, own.own, true);

            // 处理微缩效果，假设微缩效果是抽一张衍生物卡牌
            p.drawACard(CardDB.cardIDEnum.TOY_652t, own.own, true); // 替换为实际的衍生物卡牌 ID
        }

    }
}
