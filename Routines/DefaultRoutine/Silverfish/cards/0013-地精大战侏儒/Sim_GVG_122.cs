using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 小个子扰咒师卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力2，生命值5
    // 卡牌效果：相邻的随从拥有<b>扰魔</b>。
    class Sim_GVG_122 : SimTemplate //* 小个子扰咒师 Wee Spellstopper
    // Adjacent minions have <b>Elusive</b>.
    // 相邻的随从拥有<b>扰魔</b>。
    {
        // 重写光环开始方法，当小个子扰咒师进入战场时调用
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 根据小个子扰咒师的归属确定要处理的随从列表
            List<Minion> minions = own.own ? p.ownMinions : p.enemyMinions;

            // 遍历所有随从，找到小个子扰咒师的位置
            for (int i = 0; i < minions.Count; i++)
            {
                if (minions[i].entitiyID == own.entitiyID)
                {
                    // 处理左侧相邻随从
                    if (i > 0)
                    {
                        Minion leftNeighbor = minions[i - 1];
                        leftNeighbor.Elusive = true;
                    }

                    // 处理右侧相邻随从
                    if (i < minions.Count - 1)
                    {
                        Minion rightNeighbor = minions[i + 1];
                        rightNeighbor.Elusive = true;
                    }
                    break;
                }
            }
        }

        // 重写光环结束方法，当小个子扰咒师离开战场时调用
        public override void onAuraEnds(Playfield p, Minion m)
        {
            // 根据小个子扰咒师的归属确定要处理的随从列表
            List<Minion> minions = m.own ? p.ownMinions : p.enemyMinions;

            // 遍历所有随从，找到小个子扰咒师的位置
            for (int i = 0; i < minions.Count; i++)
            {
                if (minions[i].entitiyID == m.entitiyID)
                {
                    // 移除左侧相邻随从的扰魔效果
                    if (i > 0)
                    {
                        Minion leftNeighbor = minions[i - 1];
                        leftNeighbor.Elusive = false;
                    }

                    // 移除右侧相邻随从的扰魔效果
                    if (i < minions.Count - 1)
                    {
                        Minion rightNeighbor = minions[i + 1];
                        rightNeighbor.Elusive = false;
                    }
                    break;
                }
            }
        }
    }
}