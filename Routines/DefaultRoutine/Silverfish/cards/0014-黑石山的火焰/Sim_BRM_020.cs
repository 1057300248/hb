using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 龙人巫师卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张中立职业随从卡牌，费用为4点，攻击力3，生命值5
    // 卡牌效果：每当你以本随从为目标施放一个法术时，获得+1/+1。
    class Sim_BRM_020 : SimTemplate //* 龙人巫师 Dragonkin Sorcerer
    // Whenever <b>you</b> target this minion with a spell, gain +1/+1.
    // 每当<b>你</b>以本随从为目标施放一个法术时，获得+1/+1。
    {
        // 使用onCardIsGoingToBePlayed方法，因为它包含了手牌信息
        public override void onCardIsGoingToBePlayed(Playfield p, Handmanager.Handcard hc, bool wasOwnCard, Minion triggerEffectMinion)
        {
            // 检查使用法术的玩家是否与龙人巫师属于同一方
            // 并且检查卡牌类型是否为法术
            if (wasOwnCard == triggerEffectMinion.own && hc.card.type == CardDB.cardtype.SPELL)
            {
                // 给龙人巫师增加+1/+1
                p.minionGetBuffed(triggerEffectMinion, 1, 1);
            }
        }
    }
}