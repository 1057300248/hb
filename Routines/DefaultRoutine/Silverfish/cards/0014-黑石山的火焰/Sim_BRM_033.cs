using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 黑翼技师卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张中立职业随从卡牌，费用为3点，攻击力2，生命值4
    // 卡牌效果：<b>战吼：</b>如果你的手牌中有龙牌，便获得+1/+1。
    class Sim_BRM_033 : SimTemplate //* 黑翼技师 Blackwing Technician
    // <b>Battlecry:</b> If you're holding a Dragon, gain +1/+1.
    // <b>战吼：</b>如果你的手牌中有龙牌，便获得+1/+1。 
    {
        // 重写战吼效果方法，这是黑翼技师卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 检查黑翼技师的归属
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

                // 如果手牌中有龙牌，则给黑翼技师增加+1/+1
                if (dragonInHand)
                {
                    p.minionGetBuffed(m, 1, 1); // 参数：- 攻击力变化+1，- 生命值变化+1
                }
            }
            else
            {
                // 对于敌方黑翼技师，由于无法确切知道敌方手牌，采用估算方式
                // 如果敌方手牌数量>=2，假设有龙牌的概率较高
                if (p.enemyAnzCards >= 2)
                {
                    p.minionGetBuffed(m, 1, 1); // 参数：- 攻击力变化+1，- 生命值变化+1
                }
            }
        }
    }
}