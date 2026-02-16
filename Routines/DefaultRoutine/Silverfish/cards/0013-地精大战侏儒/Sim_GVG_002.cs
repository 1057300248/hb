using System; // 引入.NET基础系统命名空间
using System.Collections.Generic; // 引入泛型集合相关类
using System.Text; // 引入文本处理相关类

namespace HREngine.Bots // 定义HREngine.Bots命名空间
{
    // 碎雪机器人卡牌的模拟实现类，继承自SimTemplate基类
    // 随从 法师 费用：2 攻击力：2 生命值：3
    // Snowchugger
    // 碎雪机器人
    // <b>Freeze</b> any character damaged by this minion.
    // <b>冻结</b>任何受到本随从伤害的角色。
    class Sim_GVG_002 : SimTemplate
    {
        // 重写随从造成伤害时的触发方法
        public override void onDamageDealtByMinion(Playfield p, Minion attacker, int damageDone, bool ownplay)
        {
            // 检查造成伤害的随从是否是碎雪机器人
            if (attacker.name == CardDB.cardNameEN.snowchugger && damageDone > 0)
            {
                // 获取被攻击的目标（需要通过其他方式确定目标，这里简化处理）
                // 在实际实现中，可能需要通过其他机制来跟踪攻击目标

                // 由于SimTemplate中没有直接提供获取攻击目标的方法，
                // 我们可以通过检查最近受到伤害的随从来推断目标
                List<Minion> potentialTargets = new List<Minion>();

                if (ownplay) // 如果是己方碎雪机器人攻击
                {
                    // 检查敌方随从是否有刚受到伤害的
                    foreach (Minion enemyMinion in p.enemyMinions)
                    {
                        if (enemyMinion.Hp <= enemyMinion.maxHp - damageDone)
                        {
                            potentialTargets.Add(enemyMinion);
                        }
                    }

                    // 如果没有找到敌方随从，检查敌方英雄
                    if (potentialTargets.Count == 0 && p.enemyHero.Hp <= p.enemyHero.maxHp - damageDone)
                    {
                        // 冻结敌方英雄（如果游戏支持英雄冻结）
                        p.enemyHero.frozen = true;
                    }
                }
                else // 如果是敌方碎雪机器人攻击
                {
                    // 检查己方随从是否有刚受到伤害的
                    foreach (Minion ownMinion in p.ownMinions)
                    {
                        if (ownMinion.Hp <= ownMinion.maxHp - damageDone)
                        {
                            potentialTargets.Add(ownMinion);
                        }
                    }

                    // 如果没有找到己方随从，检查己方英雄
                    if (potentialTargets.Count == 0 && p.ownHero.Hp <= p.ownHero.maxHp - damageDone)
                    {
                        // 冻结己方英雄（如果游戏支持英雄冻结）
                        p.ownHero.frozen = true;
                    }
                }

                // 对找到的潜在目标进行冻结
                foreach (Minion target in potentialTargets)
                {
                    target.frozen = true;
                }
            }
        }
    }
}