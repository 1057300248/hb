using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 邪能火炮卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力3，生命值5
    // 卡牌效果：在你的回合结束时，对一个非机械随从造成2点伤害。
    class Sim_GVG_020 : SimTemplate //* 邪能火炮 Fel Cannon
    // At the end of your turn, deal 2 damage to a non-Mech minion.
    // 在你的回合结束时，对一个非机械随从造成2点伤害。 
    {
        // 重写回合结束触发方法，这是邪能火炮卡牌效果的核心实现
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            // 检查是否是邪能火炮拥有者的回合结束
            if (triggerEffectMinion.own == turnEndOfOwner)
            {
                // 创建随机数生成器
                Random rand = new Random();

                // 收集所有非机械随从到一个列表中
                List<Minion> nonMechMinions = new List<Minion>();

                // 添加己方非机械随从
                foreach (Minion m in p.ownMinions)
                {
                    if ((TAG_RACE)m.handcard.card.race != TAG_RACE.MECHANICAL)
                    {
                        nonMechMinions.Add(m);
                    }
                }

                // 添加敌方非机械随从
                foreach (Minion m in p.enemyMinions)
                {
                    if ((TAG_RACE)m.handcard.card.race != TAG_RACE.MECHANICAL)
                    {
                        nonMechMinions.Add(m);
                    }
                }

                // 如果存在非机械随从，则随机选择一个造成2点伤害
                if (nonMechMinions.Count >= 1)
                {
                    // 随机选择一个非机械随从
                    Minion randomTarget = nonMechMinions[rand.Next(nonMechMinions.Count)];

                    // 对随机选择的目标造成2点伤害
                    p.minionGetDamageOrHeal(randomTarget, 2, true);
                }
                // 如果没有非机械随从，则不执行任何操作
            }
        }
    }
}