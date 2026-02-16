using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 齿轮大师卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为1点，攻击力1，生命值2
    // 卡牌效果：如果你控制任何机械，便获得+2攻击力。
    class Sim_GVG_013 : SimTemplate //* 齿轮大师 Cogmaster
    // Has +2 Attack while you have a Mech.
    // 如果你控制任何机械，便获得+2攻击力。 
    {
        // 重写随从被召唤时的触发方法，当有新的随从被召唤到场上时调用
        public override void onMinionIsSummoned(Playfield p, Minion triggerEffectMinion, Minion summonedMinion)
        {
            // 检查被召唤的随从是否是机械种族
            if ((TAG_RACE)summonedMinion.handcard.card.race == TAG_RACE.MECHANICAL)
            {
                // 根据触发效果的随从归属，确定要检查的随从列表
                // 如果是己方齿轮大师，则检查己方随从；如果是敌方齿轮大师，则检查敌方随从
                List<Minion> temp = (triggerEffectMinion.own) ? p.ownMinions : p.enemyMinions;

                // 遍历对应的随从列表，检查是否已经存在其他机械随从
                foreach (Minion m in temp)
                {
                    // 如果已经存在机械随从，则直接返回，不应用增益效果
                    // 这是因为齿轮大师的效果应该在有机械时就已激活，不需要重复添加
                    if ((TAG_RACE)m.handcard.card.race == TAG_RACE.MECHANICAL) return;
                }

                // 如果之前没有机械随从，现在召唤了机械，那么给齿轮大师+2攻击力
                // 调用游戏场地的minionGetBuffed方法来修改随从的属性
                // 参数说明：- 目标随从(triggerEffectMinion)，- +2攻击力，- 0生命值变化
                p.minionGetBuffed(triggerEffectMinion, 2, 0);
            }
        }

        // 重写随从死亡时的触发方法，当有随从死亡时调用
        public override void onMinionDiedTrigger(Playfield p, Minion m, Minion diedMinion)
        {
            // 获取本回合死亡的机械随从数量
            // 根据随从m的归属，获取己方或敌方死亡的机械数量
            int diedMinions = (m.own) ? p.tempTrigger.ownMechanicDied : p.tempTrigger.enemyMechanicDied;

            // 如果没有机械死亡，直接返回
            if (diedMinions == 0) return;

            // 计算剩余的死亡机械数量（避免重复处理）
            int residual = (p.pID == m.pID) ? diedMinions - m.extraParam2 : diedMinions;

            // 更新随从的处理状态，标记为已处理
            m.pID = p.pID;
            m.extraParam2 = diedMinions;

            // 如果有机械死亡需要处理
            if (residual >= 1)
            {
                // 根据随从m的归属，确定要检查的随从列表
                List<Minion> temp = (m.own) ? p.ownMinions : p.enemyMinions;

                // 标记是否存在机械随从
                bool hasmechanics = false;

                // 遍历对应的随从列表，检查是否还有存活的机械随从
                foreach (Minion mTmp in temp)
                {
                    // 检查随从是否存活（Hp >= 1）且是机械种族
                    if (mTmp.Hp >= 1 && (TAG_RACE)mTmp.handcard.card.race == TAG_RACE.MECHANICAL)
                        hasmechanics = true;
                }

                // 如果没有存活的机械随从，则移除齿轮大师的+2攻击力增益
                if (!hasmechanics)
                {
                    // 给齿轮大师-2攻击力，抵消之前的增益效果
                    p.minionGetBuffed(m, -2, 0);
                }
            }
        }
    }
}