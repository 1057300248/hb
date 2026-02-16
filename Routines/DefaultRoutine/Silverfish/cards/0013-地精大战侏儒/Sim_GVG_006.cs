using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 机械跃迁者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个中立随从卡牌，费用为2点，攻击力2，生命值3
    // 卡牌效果：你的机械的法力值消耗减少（1）点。
    class Sim_GVG_006 : SimTemplate //* 机械跃迁者 Mechwarper
    // Your Mechs cost (1) less.
    // 你的机械的法力值消耗减少（1）点。 
    {
        // 重写光环效果开始时的触发方法，当机械跃迁者进入战场时调用
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 检查机械跃迁者是否属于己方
            if (own.own)
            {
                // 如果是己方机械跃迁者，则增加己方机械跃迁者计数器
                // 这个计数器用于在计算手牌中机械卡牌的费用时应用费用减免效果
                p.anzOwnMechwarper++;
            }
            else
            {
                // 如果是敌方机械跃迁者，则增加敌方机械跃迁者计数器
                // 这个计数器用于在计算敌方手牌中机械卡牌的费用时应用费用减免效果
                p.anzEnemyMechwarper++;
            }
        }

        // 重写光环效果结束时的触发方法，当机械跃迁者离开战场时调用
        public override void onAuraEnds(Playfield p, Minion own)
        {
            // 检查机械跃迁者是否属于己方
            if (own.own)
            {
                // 如果是己方机械跃迁者离开战场，则减少己方机械跃迁者计数器
                // 这样会取消对己方机械卡牌的费用减免效果
                p.anzOwnMechwarper--;
            }
            else
            {
                // 如果是敌方机械跃迁者离开战场，则减少敌方机械跃迁者计数器
                // 这样会取消对敌方机械卡牌的费用减免效果
                p.anzEnemyMechwarper--;
            }
        }
    }
}