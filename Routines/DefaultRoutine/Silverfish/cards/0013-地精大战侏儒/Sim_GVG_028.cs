using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 加里维克斯卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为6点，攻击力3，生命值8
    // 卡牌效果：每当你的对手施放一个法术，获得该法术的复制，并使其获得一个幸运币。
    class Sim_GVG_028 : SimTemplate //* 加里维克斯 Trade Prince Gallywix
    // Whenever your opponent casts a spell, gain a copy of it and give them a Coin.
    // 每当你的对手施放一个法术，获得该法术的复制，并使其获得一个幸运币。 
    {
        // 重写卡牌即将被打出时的触发方法，当有卡牌即将被打出时调用
        public override void onCardIsGoingToBePlayed(Playfield p, Handmanager.Handcard hc, bool wasOwnCard, Minion triggerEffectMinion)
        {
            // 获取即将打出的卡牌对象
            CardDB.Card c = hc.card;

            // 检查三个条件：
            // 1. 打出的卡牌是法术类型
            // 2. 打出的卡牌不是加里维克斯的幸运币（避免无限循环）
            // 3. 打出卡牌的玩家与加里维克斯的拥有者不同（即对手在打牌）
            if (c.type == CardDB.cardtype.SPELL &&
                c.nameEN != CardDB.cardNameEN.gallywixscoin &&
                wasOwnCard != triggerEffectMinion.own)
            {
                // 给加里维克斯的拥有者添加一张该法术的复制到手牌
                // 参数说明：- 法术卡牌ID，- 接收方（加里维克斯拥有者），- true表示特殊效果抽卡
                p.drawACard(c.cardIDenum, triggerEffectMinion.own, true);

                // 给打出法术的对手添加一张幸运币到手牌
                // 参数说明：- 幸运币卡牌名称，- 接收方（对手），- true表示特殊效果抽卡
                p.drawACard(CardDB.cardNameEN.gallywixscoin, wasOwnCard, true);
            }
        }
    }
}