using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 索瑞森大帝卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业随从卡牌，费用为5点，攻击力5，生命值5
    // 卡牌效果：在你的回合结束时，你所有手牌的法力值消耗减少（1）点。
    class Sim_BRM_028 : SimTemplate //* 索瑞森大帝 Emperor Thaurissan
    // At the end of your turn, reduce the Cost of cards in your hand by (1).
    // 在你的回合结束时，你所有手牌的法力值消耗减少（1）点。 
    {
        // 重写回合结束触发方法，这是索瑞森大帝卡牌效果的核心实现
        public override void onTurnEndsTrigger(Playfield p, Minion m, bool turnEndOfOwner)
        {
            // 检查触发回合是否属于索瑞森大帝的拥有者
            if (m.own == turnEndOfOwner)
            {
                // 遍历所有己方手牌，减少每张卡牌的法力值消耗
                foreach (Handmanager.Handcard hc in p.owncards)
                {
                    // 确保法力值消耗不会低于0
                    if (hc.manacost >= 1)
                    {
                        hc.manacost--; // 减少1点法力值消耗
                    }
                }
            }
        }
    }
}