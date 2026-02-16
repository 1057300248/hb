using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 送葬者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为1点，攻击力1，生命值2
    // 卡牌效果：每当你召唤一个具有<b>亡语</b>的随从，便获得+1/+1。
    class Sim_FP1_028 : SimTemplate //* 送葬者 Undertaker
    // Whenever you summon a minion with <b>Deathrattle</b>, gain +1/+1.
    // 每当你召唤一个具有<b>亡语</b>的随从，便获得+1/+1。
    {
        // 重写随从被召唤触发方法，这是送葬者卡牌效果的核心实现
        public override void onMinionIsSummoned(Playfield p, Minion triggerEffectMinion, Minion summonedMinion)
        {
            // 检查召唤的随从是否与送葬者同属一方
            if (triggerEffectMinion.own == summonedMinion.own)
            {
                // 检查被召唤的随从是否具有亡语效果
                if (summonedMinion.handcard.card.deathrattle)
                {
                    // 给送葬者增加+1/+1
                    p.minionGetBuffed(triggerEffectMinion, 1, 1);
                }
            }
        }
    }
}