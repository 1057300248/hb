using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 碎石穴居人卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力2，生命值3
    // 卡牌效果：每当你的对手施放一个法术，便获得+1攻击力。
    class Sim_GVG_067 : SimTemplate //* 碎石穴居人 Stonesplinter Trogg
    // Whenever your opponent casts a spell, gain +1 Attack.
    // 每当你的对手施放一个法术，便获得+1攻击力。 
    {
        // 重写卡牌即将被打出时的触发方法，当有卡牌即将被打出时调用
        public override void onCardIsGoingToBePlayed(Playfield p, Handmanager.Handcard hc, bool wasOwnCard, Minion triggerEffectMinion)
        {
            // 检查三个条件：
            // 1. 打出的卡牌是法术类型
            // 2. 打出卡牌的玩家与碎石穴居人的拥有者不同（即对手在打牌）
            if (hc.card.type == CardDB.cardtype.SPELL && wasOwnCard != triggerEffectMinion.own)
            {
                // 给碎石穴居人增加+1攻击力
                p.minionGetBuffed(triggerEffectMinion, 1, 0);
            }
        }
    }
}