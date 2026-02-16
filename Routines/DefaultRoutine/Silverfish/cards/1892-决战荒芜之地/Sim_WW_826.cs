using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 德鲁伊 费用：4 攻击力：3 生命值：4
    //Desert Nestmatron
    //沙漠巢母
    //<b>Taunt</b>. <b>Battlecry:</b> If you're holding a Dragon, refresh 4 Mana Crystals.
    //<b>嘲讽</b>。<b>战吼：</b>如果你的手牌中有龙牌，复原四个法力水晶。
    class Sim_WW_826 : SimTemplate
    {
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 检查手牌中是否有龙牌
            bool 手牌中是否有龙牌 = false;
            foreach (Handmanager.Handcard card in p.owncards)
            {
                if (card.card.race == CardDB.Race.DRAGON)
                {
                    手牌中是否有龙牌 = true;
                    break;
                }
            }

            // 如果手牌中有龙牌，复原4个法力水晶
            if (手牌中是否有龙牌)
            {
                p.mana = Math.Min(10, p.mana + 4); // 恢复4个法力水晶，但不超过10个
            }
        }
    }
}
