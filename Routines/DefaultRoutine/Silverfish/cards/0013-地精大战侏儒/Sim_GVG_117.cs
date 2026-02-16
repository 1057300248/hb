using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 加兹鲁维卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为6点，攻击力3，生命值6
    // 卡牌效果：每当你施放一个法力值消耗为（1）的法术，随机将一张机械牌置入你的手牌。
    class Sim_GVG_117 : SimTemplate //* 加兹鲁维 Gazlowe
    // Whenever you cast a 1-Cost spell, add a random Mech to your hand.
    // 每当你施放一个法力值消耗为（1）的法术，随机将一张机械牌置入你的手牌。 
    {
        // 重写卡牌即将使用触发方法，这是加兹鲁维卡牌效果的核心实现
        public override void onCardIsGoingToBePlayed(Playfield p, Handmanager.Handcard hc, bool wasOwnCard, Minion triggerEffectMinion)
        {
            // 检查加兹鲁维是否属于施放法术的一方
            if (triggerEffectMinion.own == wasOwnCard)
            {
                // 检查使用的卡牌是否为1费法术
                if (hc.card.type == CardDB.cardtype.SPELL && hc.manacost == 1)
                {
                    // 为施放法术的一方添加一张随机机械牌到手牌
                    // 参数说明：- CardDB.cardNameEN.shieldedminibot：机械牌名称（持盾机器人）
                    // - wasOwnCard：指示为哪一方添加手牌
                    // - true：表示这是特殊效果抽卡
                    p.drawACard(CardDB.cardNameEN.shieldedminibot, wasOwnCard, true);
                }
            }
        }
    }
}