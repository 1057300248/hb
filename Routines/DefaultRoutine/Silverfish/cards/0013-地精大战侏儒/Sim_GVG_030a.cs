using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 攻击模式卡牌的模拟实现类，继承自SimTemplate基类
    // 这是电镀机械熊仔的抉择选项之一，法师职业法术卡牌，费用为0点
    // 卡牌效果：+1攻击力。
    class Sim_GVG_030a : SimTemplate //* 攻击模式 Attack Mode
    // +1 Attack.
    // +1攻击力。 
    {
        // 重写战吼效果方法，这是攻击模式卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 给目标随从（通常是电镀机械熊仔）增加+1攻击力
            // 参数说明：- 目标随从，- +1攻击力，- 0生命值变化
            p.minionGetBuffed(own, 1, 0);
        }
    }
}