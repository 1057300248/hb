using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 战斗机器人卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为1点，攻击力1，生命值3
    // 卡牌效果：受伤时具有+1攻击力。
    class Sim_GVG_051 : SimTemplate //* 战斗机器人 Warbot
    // Has +1 Attack while damaged.
    // 受伤时具有+1攻击力。 
    {
        // 重写激怒效果开始时的触发方法，当随从受伤进入激怒状态时调用
        public override void onEnrageStart(Playfield p, Minion m)
        {
            // 给战斗机器人增加+1攻击力
            p.minionGetBuffed(m, 1, 0);
        }

        // 重写激怒效果结束时的触发方法，当随从恢复满血退出激怒状态时调用
        public override void onEnrageStop(Playfield p, Minion m)
        {
            // 移除战斗机器人的+1攻击力增益
            p.minionGetBuffed(m, -1, 0);
        }
    }
}