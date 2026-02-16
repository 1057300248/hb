using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 钴制卫士卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力6，生命值3
    // 卡牌效果：每当你召唤一个机械，便获得<b>圣盾</b>。
    class Sim_GVG_062 : SimTemplate //* 钴制卫士 Cobalt Guardian
    // Whenever you summon a Mech, gain <b>Divine Shield</b>.
    // 每当你召唤一个机械，便获得<b>圣盾</b>。 
    {
        // 重写随从被召唤时的触发方法，当有新的随从被召唤到场上时调用
        public override void onMinionIsSummoned(Playfield p, Minion triggerEffectMinion, Minion summonedMinion)
        {
            // 检查三个条件：
            // 1. 触发效果的钴制卫士与被召唤的随从属于同一方
            // 2. 被召唤的随从是机械种族
            if (triggerEffectMinion.own == summonedMinion.own &&
                (TAG_RACE)summonedMinion.handcard.card.race == TAG_RACE.MECHANICAL)
            {
                // 给钴制卫士添加圣盾效果
                triggerEffectMinion.divineshild = true;
            }
        }
    }
}