using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 死灵骑士卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为6点，攻击力4，生命值6
    // 卡牌效果：<b>亡语：</b>消灭与该随从相邻的随从。
    class Sim_NAXM_001 : SimTemplate //* 死灵骑士 Necroknight
    // <b>Deathrattle:</b> Destroy the minions next to this one as well.
    // <b>亡语：</b>消灭与该随从相邻的随从。 
    {
        // 重写亡语效果方法，这是死灵骑士卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 根据死灵骑士的归属确定要处理的随从列表
            List<Minion> minions = (m.own) ? p.ownMinions : p.enemyMinions;

            // 遍历所有同方随从，查找与死灵骑士相邻的随从
            foreach (Minion adjacentMinion in minions)
            {
                // 检查是否为相邻位置（左边或右边）
                if (adjacentMinion.zonepos == m.zonepos + 1 || adjacentMinion.zonepos == m.zonepos - 1)
                {
                    // 消灭相邻的随从
                    p.minionGetDestroyed(adjacentMinion);
                }
            }
        }
    }
}