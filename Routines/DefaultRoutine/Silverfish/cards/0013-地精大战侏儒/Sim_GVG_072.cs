using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 暗影打击装甲卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力2，生命值3
    // 卡牌效果：每当一个随从获得治疗，便随机对一个敌人造成1点伤害。
    class Sim_GVG_072 : SimTemplate //* 暗影打击装甲 Shadowboxer
    // Whenever a minion is healed, deal 1 damage to a random enemy.
    // 每当一个随从获得治疗，便随机对一个敌人造成1点伤害。 
    {
        // 重写随从获得治疗时的触发方法，当有随从获得治疗时调用
        public override void onACharGotHealed(Playfield p, Minion triggerEffectMinion, int charsGotHealed)
        {
            Minion target = null;

            // 检查暗影打击装甲是否属于己方
            if (triggerEffectMinion.own)
            {
                // 己方暗影打击装甲：随机选择敌方目标造成伤害
                target = p.getEnemyCharTargetForRandomSingleDamage(charsGotHealed);
            }
            else
            {
                // 敌方暗影打击装甲：选择己方攻击力最高的随从作为目标
                target = p.searchRandomMinion(p.ownMinions, searchmode.searchHighestAttack);
                // 如果没有随从，则选择己方英雄作为目标
                if (target == null) target = p.ownHero;
            }

            // 对选中的目标造成相应伤害
            // 参数说明：- 目标角色，- 伤害值（等于被治疗的随从数量），- true表示这是伤害操作
            p.minionGetDamageOrHeal(target, charsGotHealed, true);
        }
    }
}