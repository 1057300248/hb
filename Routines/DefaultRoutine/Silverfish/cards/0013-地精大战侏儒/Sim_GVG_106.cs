using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 回收机器人卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力1，生命值5
    // 卡牌效果：每当一个友方机械死亡，便获得+2/+2。
    class Sim_GVG_106 : SimTemplate //* 回收机器人 Junkbot
    // Whenever a friendly Mech dies, gain +2/+2.
    // 每当一个友方机械死亡，便获得+2/+2。 
    {
        // 重写随从死亡触发方法，这是回收机器人卡牌效果的核心实现
        public override void onMinionDiedTrigger(Playfield p, Minion m, Minion diedMinion)
        {
            // 获取死亡的机械随从数量
            int diedMechs = (m.own) ? p.tempTrigger.ownMechanicDied : p.tempTrigger.enemyMechanicDied;

            // 如果没有机械死亡，则直接返回
            if (diedMechs == 0) return;

            // 计算本次需要处理的机械死亡数量
            int residual = (p.pID == m.pID) ? diedMechs - m.extraParam2 : diedMechs;

            // 更新随从的状态标识
            m.pID = p.pID;
            m.extraParam2 = diedMechs;

            // 根据死亡的机械数量给回收机器人增加攻击力和生命值
            for (int i = 0; i < residual; i++)
            {
                // 每个死亡的机械给予+2/+2
                p.minionGetBuffed(m, 2 * residual, 2 * residual);
            }
        }
    }
}