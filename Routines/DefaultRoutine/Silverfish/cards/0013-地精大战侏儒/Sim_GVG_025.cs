using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 独眼欺诈者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力4，生命值1
    // 卡牌效果：每当你召唤一个海盗，便获得<b>潜行</b>。
    class Sim_GVG_025 : SimTemplate //* 独眼欺诈者 One-eyed Cheat
    // Whenever you summon a Pirate, gain <b>Stealth</b>.
    // 每当你召唤一个海盗，便获得<b>潜行</b>。 
    {
        // 重写随从被召唤时的触发方法，当有新的随从被召唤到场上时调用
        public override void onMinionIsSummoned(Playfield p, Minion triggerEffectMinion, Minion summonedMinion)
        {
            // 检查被召唤的随从是否是海盗种族
            if ((TAG_RACE)summonedMinion.handcard.card.race == TAG_RACE.PIRATE)
            {
                // 如果召唤的是海盗，则给独眼欺诈者赋予潜行效果
                // 设置stealth属性为true，使随从获得潜行状态
                triggerEffectMinion.stealth = true;
            }
        }
    }
}