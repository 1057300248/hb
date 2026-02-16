using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 纳克萨玛斯之影卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个中立法师职业随从卡牌，费用为3点，攻击力2，生命值2
    // 卡牌效果：<b><b>潜行</b>。</b>在你的回合开始时，获得+1/+1。
    class Sim_BG26_FP1_005 : SimTemplate //* 纳克萨玛斯之影 Shade of Naxxramas
    // <b><b>Stealth</b>.</b> At the start of your turn, gain +1/+1.
    // <b><b>潜行</b>。</b>在你的回合开始时，获得+1/+1。
    {
        // 重写回合开始触发方法，这是纳克萨玛斯之影卡牌效果的核心实现
        public override void onTurnStartTrigger(Playfield p, Minion triggerEffectMinion, bool turnStartOfOwner)
        {
            // 检查是否是纳克萨玛斯之影拥有者的回合开始
            if (turnStartOfOwner == triggerEffectMinion.own)
            {
                // 给纳克萨玛斯之影增加+1/+1
                // 参数说明：- 目标随从，- +1攻击力，- +1生命值
                p.minionGetBuffed(triggerEffectMinion, 1, 1);
            }
        }
    }
}