using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 暮光雏龙卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业随从卡牌，费用为1点，攻击力2，生命值1
    // 卡牌效果：<b>战吼：</b>如果你的手牌中有龙牌，便获得+2生命值。
    class Sim_BRM_004 : SimTemplate //* 暮光雏龙 Twilight Whelp
    // <b>Battlecry:</b> If you're holding a Dragon, gain +2 Health.
    // <b>战吼：</b>如果你的手牌中有龙牌，便获得+2生命值。 
    {
        // 重写战吼效果方法，这是暮光雏龙卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 检查暮光雏龙的归属
            if (m.own)
            {
                // 检查己方手牌中是否有龙牌
                bool dragonInHand = false;
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    // 检查卡牌种族是否为龙
                    if ((TAG_RACE)hc.card.race == TAG_RACE.DRAGON)
                    {
                        dragonInHand = true;
                        break;
                    }
                }

                // 如果手牌中有龙牌，则给暮光雏龙增加+2生命值
                if (dragonInHand)
                {
                    p.minionGetBuffed(m, 0, 2); // 参数：- 攻击力变化0，- 生命值变化+2
                }
            }
            else
            {
                // 对于敌方暮光雏龙，由于无法确切知道敌方手牌，采用估算方式
                // 如果敌方手牌数量>=2，假设有龙牌的概率较高
                if (p.enemyAnzCards >= 2)
                {
                    p.minionGetBuffed(m, 0, 2); // 参数：- 攻击力变化0，- 生命值变化+2
                }
            }
        }
    }
}