using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 瑞文戴尔男爵卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个中立法师职业随从卡牌，费用为4点，攻击力1，生命值7
    // 卡牌效果：你的随从的<b>亡语</b>将触发两次。
    class Sim_BG_FP1_031 : SimTemplate //* 瑞文戴尔男爵 Baron Rivendare
    // Your minions trigger their <b>Deathrattles</b> twice.
    // 你的随从的<b>亡语</b>将触发两次。
    {
        // 重写光环开始方法，当瑞文戴尔男爵进入战场时调用
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 根据瑞文戴尔男爵的归属增加相应的亡语触发次数
            if (own.own)
            {
                // 增加己方亡语触发次数
                p.我们有瑞文戴尔男爵++;
            }
            else
            {
                // 增加敌方亡语触发次数
                p.敌方有瑞文戴尔男爵++;
            }
        }

        // 重写光环结束方法，当瑞文戴尔男爵离开战场时调用
        public override void onAuraEnds(Playfield p, Minion m)
        {
            // 根据瑞文戴尔男爵的归属减少相应的亡语触发次数
            if (m.own)
            {
                // 减少己方亡语触发次数
                p.我们有瑞文戴尔男爵--;
            }
            else
            {
                // 减少敌方亡语触发次数
                p.敌方有瑞文戴尔男爵--;
            }
        }
    }
}