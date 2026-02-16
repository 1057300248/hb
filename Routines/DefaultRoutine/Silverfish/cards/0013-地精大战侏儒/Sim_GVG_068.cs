using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 石腭穴居人壮汉卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力3，生命值5
    // 卡牌效果：每当你的对手施放一个法术，获得+2攻击力。
    class Sim_GVG_068 : SimTemplate //* 石腭穴居人壮汉 Burly Rockjaw Trogg
    // Whenever your opponent casts a spell, gain +2 Attack.
    // 每当你的对手施放一个法术，获得+2攻击力。 
    {
        // 重写卡牌即将被打出时的触发方法，当有卡牌即将被打出时调用
        public override void onCardIsGoingToBePlayed(Playfield p, Handmanager.Handcard hc, bool wasOwnCard, Minion triggerEffectMinion)
        {
            // 检查三个条件：
            // 1. 打出的卡牌是法术类型
            // 2. 打出卡牌的玩家与石腭穴居人壮汉的拥有者不同（即对手在打牌）
            if (hc.card.type == CardDB.cardtype.SPELL && wasOwnCard != triggerEffectMinion.own)
            {
                // 给石腭穴居人壮汉增加+2攻击力
                p.minionGetBuffed(triggerEffectMinion, 2, 0);
            }
        }
    }
}