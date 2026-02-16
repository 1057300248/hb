using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 烈焰巨兽卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为7点，攻击力7，生命值7
    // 卡牌效果：<b>突袭</b>。当你抽到该牌时，对所有除机械外的角色造成2点伤害。
    class Sim_GVG_007 : SimTemplate
    {
        // 随从 法师 费用：7 攻击力：7 生命值：7
        // Flame Leviathan
        // 烈焰巨兽
        // [x]<b>Rush</b>When you draw this, deal2 damage to all charactersexcept Mechs.
        // <b>突袭</b>。当你抽到该牌时，对所有除机械外的角色造成2点伤害。

        // 重写卡牌被抽到时的触发方法（当卡牌进入手牌时）
        public override void onCardIsDrawn(Playfield p, bool ownplay, Minion triggerEffectMinion)
        {
            // 检查是否是己方抽到这张卡牌
            if (ownplay)
            {
                // 对所有己方非机械随从造成2点伤害
                foreach (Minion m in p.ownMinions)
                {
                    // 检查随从种族是否不是机械
                    if ((TAG_RACE)m.handcard.card.race != TAG_RACE.MECHANICAL)
                    {
                        // 对非机械随从造成2点伤害
                        p.minionGetDamageOrHeal(m, 2);
                    }
                }

                // 对己方英雄造成2点伤害（英雄不是机械）
                p.minionGetDamageOrHeal(p.ownHero, 2);

                // 对所有敌方非机械随从造成2点伤害
                foreach (Minion m in p.enemyMinions)
                {
                    // 检查随从种族是否不是机械
                    if ((TAG_RACE)m.handcard.card.race != TAG_RACE.MECHANICAL)
                    {
                        // 对非机械随从造成2点伤害
                        p.minionGetDamageOrHeal(m, 2);
                    }
                }

                // 对敌方英雄造成2点伤害（英雄不是机械）
                p.minionGetDamageOrHeal(p.enemyHero, 2);
            }
            else
            {
                // 如果是敌方抽到这张卡牌，同样对所有非机械角色造成伤害
                // 对所有敌方非机械随从造成2点伤害
                foreach (Minion m in p.enemyMinions)
                {
                    if ((TAG_RACE)m.handcard.card.race != TAG_RACE.MECHANICAL)
                    {
                        p.minionGetDamageOrHeal(m, 2);
                    }
                }

                // 对敌方英雄造成2点伤害
                p.minionGetDamageOrHeal(p.enemyHero, 2);

                // 对所有己方非机械随从造成2点伤害
                foreach (Minion m in p.ownMinions)
                {
                    if ((TAG_RACE)m.handcard.card.race != TAG_RACE.MECHANICAL)
                    {
                        p.minionGetDamageOrHeal(m, 2);
                    }
                }

                // 对己方英雄造成2点伤害
                p.minionGetDamageOrHeal(p.ownHero, 2);
            }
        }
    }
}
