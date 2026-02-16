using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 玛尔加尼斯卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为9点，攻击力9，生命值7
    // 卡牌效果：你的其他恶魔获得+2/+2。你的英雄获得<b>免疫</b>。
    class Sim_GVG_021 : SimTemplate //* 玛尔加尼斯 Mal'Ganis
    // Your other Demons have +2/+2.Your hero is <b>Immune</b>.
    // 你的其他恶魔获得+2/+2。你的英雄获得<b>免疫</b>。 
    {
        // 重写光环效果开始时的触发方法，当玛尔加尼斯进入战场时调用
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 检查玛尔加尼斯是否属于己方
            if (own.own)
            {
                // 增加己方玛尔加尼斯计数器
                p.anzOwnMalGanis++;

                // 使己方英雄获得免疫效果
                p.ownHero.immune = true;

                // 遍历己方所有随从，为其他恶魔随从添加+2/+2增益
                foreach (Minion m in p.ownMinions)
                {
                    // 排除玛尔加尼斯自身，并检查是否为恶魔种族
                    if (own.entitiyID != m.entitiyID && (TAG_RACE)m.handcard.card.race == TAG_RACE.DEMON)
                        p.minionGetBuffed(m, 2, 2);
                }
            }
            else
            {
                // 增加敌方玛尔加尼斯计数器
                p.anzEnemyMalGanis++;

                // 使敌方英雄获得免疫效果
                p.enemyHero.immune = true;

                // 遍历敌方所有随从，为其他恶魔随从添加+2/+2增益
                foreach (Minion m in p.enemyMinions)
                {
                    // 排除玛尔加尼斯自身，并检查是否为恶魔种族
                    if (own.entitiyID != m.entitiyID && (TAG_RACE)m.handcard.card.race == TAG_RACE.DEMON)
                        p.minionGetBuffed(m, 2, 2);
                }
            }
        }

        // 重写光环效果结束时的触发方法，当玛尔加尼斯离开战场时调用
        public override void onAuraEnds(Playfield p, Minion own)
        {
            // 检查玛尔加尼斯是否属于己方
            if (own.own)
            {
                // 减少己方玛尔加尼斯计数器
                p.anzOwnMalGanis--;

                // 移除己方英雄的免疫效果
                p.ownHero.immune = false;

                // 遍历己方所有随从，移除其他恶魔随从的+2/+2增益
                foreach (Minion m in p.ownMinions)
                {
                    // 排除玛尔加尼斯自身，并检查是否为恶魔种族
                    if (own.entitiyID != m.entitiyID && (TAG_RACE)m.handcard.card.race == TAG_RACE.DEMON)
                        p.minionGetBuffed(m, -2, -2);
                }
            }
            else
            {
                // 减少敌方玛尔加尼斯计数器
                p.anzEnemyMalGanis--;

                // 移除敌方英雄的免疫效果
                p.enemyHero.immune = false;

                // 遍历敌方所有随从，移除其他恶魔随从的+2/+2增益
                foreach (Minion m in p.enemyMinions)
                {
                    // 排除玛尔加尼斯自身，并检查是否为恶魔种族
                    if (own.entitiyID != m.entitiyID && (TAG_RACE)m.handcard.card.race == TAG_RACE.DEMON)
                        p.minionGetBuffed(m, -2, -2);
                }
            }
        }
    }
}