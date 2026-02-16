using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 死吧，虫子！英雄技能卡牌的模拟实现类，继承自SimTemplate基类
    // 这是炎魔之王拉格纳罗斯的英雄技能，费用为2点
    // 卡牌效果：<b>英雄技能</b>随机对一个敌人造成8点伤害。
    class Sim_BRM_027p : SimTemplate //* 死吧，虫子！ DIE, INSECT!
    // <b>Hero Power</b>Deal $8 damage to a random enemy.
    // <b>英雄技能</b>随机对一个敌人造成$8点伤害。 
    {
        // 重写卡牌使用效果方法，这是死吧，虫子！英雄技能的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 计算伤害值，考虑英雄技能伤害加成
            int damage = (ownplay) ? p.getHeroPowerDamage(8) : p.getEnemyHeroPowerDamage(8);

            if (ownplay)
            {
                // 己方使用：随机选择一个敌方目标
                target = p.getEnemyCharTargetForRandomSingleDamage(damage);
            }
            else
            {
                // 敌方使用：选择一个目标（通常选择攻击力最高的己方随从，如果没有随从则选择己方英雄）
                target = p.searchRandomMinion(p.ownMinions, searchmode.searchHighestAttack);
                if (target == null)
                {
                    target = p.ownHero; // 如果没有随从，则选择己方英雄
                }
            }

            // 对目标造成伤害
            p.minionGetDamageOrHeal(target, damage, true); // 最后一个参数true表示这是英雄技能造成的伤害
        }
    }
}