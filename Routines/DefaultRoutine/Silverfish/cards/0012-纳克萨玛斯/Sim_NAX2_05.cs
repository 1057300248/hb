using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 膜拜者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业随从卡牌，费用为1点，攻击力1，生命值1
    // 卡牌效果：你的英雄在你的回合获得+1攻击力。
    class Sim_NAX2_05 : SimTemplate //* 膜拜者 Worshipper
    // Your hero has +1 Attack on your turn.
    // 你的英雄在你的回合获得+1攻击力。 
    {
        // 重写光环开始方法，当膜拜者进入战场时调用
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 给膜拜者拥有者的英雄增加+1攻击力
            // 参数说明：- 目标英雄（根据膜拜者归属选择己方或敌方英雄），- +1攻击力，- 0生命值变化
            p.minionGetBuffed(own.own ? p.ownHero : p.enemyHero, 1, 0);
        }

        // 重写光环结束方法，当膜拜者离开战场时调用
        public override void onAuraEnds(Playfield p, Minion own)
        {
            // 移除膜拜者拥有者英雄的+1攻击力
            // 参数说明：- 目标英雄（根据膜拜者归属选择己方或敌方英雄），- -1攻击力，- 0生命值变化
            p.minionGetBuffed(own.own ? p.ownHero : p.enemyHero, -1, 0);
        }
    }
}