using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 分裂软泥怪卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力1，生命值2
    // 卡牌效果：<b>战吼：</b>在回合结束时召唤一个该随从的复制。
    class Sim_FP1_003 : SimTemplate //* 分裂软泥怪 Echoing Ooze
    // <b>Battlecry:</b> Summon an exact copy of this minion at the end of the turn.
    // <b>战吼：</b>在回合结束时召唤一个该随从的复制。 
    {
        // 重写回合结束触发方法，这是分裂软泥怪卡牌效果的核心实现
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            // 检查分裂软泥怪是否在当前回合被打出且属于当前回合结束的一方
            if (triggerEffectMinion.playedThisTurn && triggerEffectMinion.own == turnEndOfOwner)
            {
                // 在分裂软泥怪的位置召唤一个复制
                // 参数说明：- 要召唤的卡牌（分裂软泥怪本身），- 召唤位置，- 是否为己方召唤
                p.callKid(triggerEffectMinion.handcard.card, triggerEffectMinion.zonepos, turnEndOfOwner);

                // 获取当前回合结束一方的所有随从
                List<Minion> minions = (turnEndOfOwner) ? p.ownMinions : p.enemyMinions;

                // 遍历所有随从，找到刚召唤的分裂软泥怪复制并设置属性
                foreach (Minion minion in minions)
                {
                    // 检查是否为分裂软泥怪且不是原始的那个
                    if (minion.name == CardDB.cardNameEN.echoingooze && triggerEffectMinion.entitiyID != minion.entitiyID)
                    {
                        // 将复制的属性设置为与原始分裂软泥怪相同
                        minion.setMinionToMinion(triggerEffectMinion);
                        break;
                    }
                }
            }
        }
    }
}