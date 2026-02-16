using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 颤地者特罗格佐尔卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个中立法师职业随从卡牌，费用为7点，攻击力6，生命值6
    // 卡牌效果：每当你的对手施放一个法术，召唤一个石腭穴居人壮汉。
    class Sim_GVG_118 : SimTemplate //* 颤地者特罗格佐尔 Troggzor the Earthinator
    // Whenever your opponent casts a spell, summon a Burly Rockjaw Trogg.
    // 每当你的对手施放一个法术，召唤一个石腭穴居人壮汉。
    {
        // 定义要召唤的石腭穴居人壮汉卡牌
        CardDB.Card trogg = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_068);

        // 重写卡牌即将使用触发方法，这是颤地者特罗格佐尔卡牌效果的核心实现
        public override void onCardIsGoingToBePlayed(Playfield p, Handmanager.Handcard hc, bool wasOwnCard, Minion triggerEffectMinion)
        {
            // 检查施放法术的一方是否为对手（即不是特罗格佐尔的拥有者）
            if (triggerEffectMinion.own != wasOwnCard)
            {
                // 检查使用的卡牌是否为法术
                if (hc.card.type == CardDB.cardtype.SPELL)
                {
                    // 确定召唤位置（在特罗格佐尔旁边）
                    int summonPosition = triggerEffectMinion.zonepos;

                    // 召唤石腭穴居人壮汉
                    // 参数说明：- 要召唤的卡牌，- 召唤位置，- 是否为己方召唤（这里为false，因为是对手施放法术触发）
                    p.callKid(trogg, summonPosition, triggerEffectMinion.own);
                }
            }
        }
    }
}