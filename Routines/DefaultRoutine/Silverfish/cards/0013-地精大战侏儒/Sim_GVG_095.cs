using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 地精工兵卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力2，生命值4
    // 卡牌效果：如果你对手的手牌数量大于或等于6张，便具有+4攻击力。
    class Sim_GVG_095 : SimTemplate //* 地精工兵 Goblin Sapper
    // Has +4 Attack while your opponent has 6 or more cards in hand.
    // 如果你对手的手牌数量大于或等于6张，便具有+4攻击力。 
    {
        // 重写战吼效果方法，这是地精工兵卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 获取对手的手牌数量
            // 如果是己方地精工兵，则检查敌方手牌数；如果是敌方地精工兵，则检查己方手牌数
            int 对手手牌数量 = (own.own) ? p.enemyAnzCards : p.owncards.Count;

            // 如果对手手牌数量大于或等于6张
            if (对手手牌数量 >= 6)
            {
                // 给地精工兵增加+4攻击力
                // 参数说明：- 目标随从，- +4攻击力，- 0生命值变化
                p.minionGetBuffed(own, 4, 0);
            }
        }
    }
}