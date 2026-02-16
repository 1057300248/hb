using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 假死卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为2点
    // 卡牌效果：触发所有友方随从的<b>亡语</b>。
    class Sim_GVG_026 : SimTemplate //* 假死 Feign Death
    // Trigger all <b>Deathrattles</b> on your minions.
    // 触发所有友方随从的<b>亡语</b>。 
    {
        // 重写卡牌打出时的效果方法，这是假死卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 检查是否是己方打出此卡牌
            if (ownplay)
            {
                // 创建己方随从列表的副本（避免在执行亡语过程中修改原列表）
                List<Minion> ownMinionsCopy = new List<Minion>(p.ownMinions);

                // 触发所有己方随从的亡语效果
                // doDeathrattles方法会遍历列表并执行每个随从的亡语
                p.doDeathrattles(ownMinionsCopy);
            }
            else // 如果是敌方打出此卡牌
            {
                // 创建敌方随从列表的副本
                List<Minion> enemyMinionsCopy = new List<Minion>(p.enemyMinions);

                // 触发所有敌方随从的亡语效果
                p.doDeathrattles(enemyMinionsCopy);
            }
        }
    }
}