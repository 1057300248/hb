using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 德鲁伊 费用：3 攻击力：3 生命值：2
    //Splish-Splash Whelp
    //戏水雏龙
    //<b>Battlecry:</b> If you're holding a Dragon, gain an empty Mana Crystal.
    //<b>战吼：</b>如果你的手牌中有龙牌，获得一个空的法力水晶。
    class Sim_WW_819 : SimTemplate
    {
        public virtual void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
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

            // 如果手牌中有龙牌，则获得一个空的法力水晶
            if (手牌中是否有龙牌)
            {
                p.ownMaxMana = Math.Min(10, p.ownMaxMana + 1); // 增加一个法力水晶，上限为10
            }
        }
    }
}
