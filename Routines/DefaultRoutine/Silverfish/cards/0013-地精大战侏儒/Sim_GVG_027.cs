using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 钢铁武道家卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力2，生命值2
    // 卡牌效果：在你的回合结束时，使另一个友方机械获得+2/+2。
    class Sim_GVG_027 : SimTemplate //* 钢铁武道家 Iron Sensei
    // At the end of your turn, give another friendly Mech +2/+2.
    // 在你的回合结束时，使另一个友方机械获得+2/+2。 
    {
        // 重写回合结束触发方法，这是钢铁武道家卡牌效果的核心实现
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            // 检查是否是钢铁武道家拥有者的回合结束
            if (triggerEffectMinion.own == turnEndOfOwner)
            {
                // 根据回合拥有者确定要检查的随从列表（己方或敌方）
                List<Minion> tmp = turnEndOfOwner ? p.ownMinions : p.enemyMinions;
                int count = tmp.Count;

                // 只有当场上随从数量大于1时才执行效果（需要有其他随从）
                if (count > 1)
                {
                    Minion mnn = null;

                    // 初始化目标随从为第一个非钢铁武道家的随从
                    if (triggerEffectMinion.entitiyID != tmp[0].entitiyID)
                        mnn = tmp[0];
                    else
                        mnn = tmp[1];

                    // 遍历所有随从，寻找血量最低的机械随从（排除钢铁武道家自身）
                    for (int i = 1; i < count; i++)
                    {
                        // 跳过钢铁武道家自身
                        if (triggerEffectMinion.entitiyID == tmp[i].entitiyID)
                            continue;

                        // 检查是否为机械种族且血量更低
                        if ((TAG_RACE)tmp[i].handcard.card.race == TAG_RACE.MECHANICAL)
                        {
                            if (mnn == null || tmp[i].Hp < mnn.Hp)
                                mnn = tmp[i];
                        }
                    }

                    // 如果找到符合条件的机械随从，则给予+2/+2增益
                    if (mnn != null)
                        p.minionGetBuffed(mnn, 2, 2);
                }
            }
        }
    }
}