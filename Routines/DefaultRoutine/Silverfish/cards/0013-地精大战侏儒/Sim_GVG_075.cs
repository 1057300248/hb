using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 船载火炮卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力2，生命值3
    // 卡牌效果：在你召唤一个海盗后，随机对一个敌人造成2点伤害。
    class Sim_GVG_075 : SimTemplate //* 船载火炮 Ship's Cannon
    // [x]After you summon aPirate, deal 2 damageto a random enemy.
    // 在你召唤一个海盗后，随机对一个敌人造成2点伤害。 
    {
        // 重写随从被召唤时的触发方法，当有新的随从被召唤到场上时调用
        public override void onMinionIsSummoned(Playfield p, Minion triggerEffectMinion, Minion summonedMinion)
        {
            // 检查三个条件：
            // 1. 被召唤的随从是海盗种族
            // 2. 触发效果的船载火炮与被召唤的随从属于同一方
            if ((TAG_RACE)summonedMinion.handcard.card.race == TAG_RACE.PIRATE &&
                triggerEffectMinion.own == summonedMinion.own)
            {
                // 创建随机数生成器
                Random rand = new Random();

                // 收集所有可能的目标（敌方随从和英雄）
                List<Minion> targets = new List<Minion>();

                // 添加敌方随从到目标列表
                if (triggerEffectMinion.own)
                {
                    targets.AddRange(p.enemyMinions);
                }
                else
                {
                    targets.AddRange(p.ownMinions);
                }

                // 如果有随从目标，则随机选择一个
                if (targets.Count > 0)
                {
                    Minion target = targets[rand.Next(targets.Count)];
                    p.minionGetDamageOrHeal(target, 2, true);
                }
                else
                {
                    // 如果没有随从，则攻击敌方英雄
                    if (triggerEffectMinion.own)
                    {
                        p.minionGetDamageOrHeal(p.enemyHero, 2, true);
                    }
                    else
                    {
                        p.minionGetDamageOrHeal(p.ownHero, 2, true);
                    }
                }
            }
        }
    }
}