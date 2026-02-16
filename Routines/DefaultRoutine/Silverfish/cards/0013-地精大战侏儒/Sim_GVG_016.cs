using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 魔能机甲卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个中立随从卡牌，费用为5点，攻击力8，生命值8
    // 卡牌效果：每当你的对手使用一张卡牌时，便移除你的牌库顶的三张牌。
    class Sim_GVG_016 : SimTemplate //* 魔能机甲 Fel Reaver
    // Whenever your opponent plays a card, remove the top 3 cards of your deck.
    // 每当你的对手使用一张卡牌时，便移除你的牌库顶的三张牌。 
    {
        // 重写卡牌即将被打出时的触发方法，当有卡牌即将被打出时调用
        public override void onCardIsGoingToBePlayed(Playfield p, Handmanager.Handcard hc, bool wasOwnCard, Minion triggerEffectMinion)
        {
            // 检查是否是对手在打牌
            // wasOwnCard表示打出的卡牌是否属于己方
            // triggerEffectMinion.own表示触发效果的魔能机甲是否属于己方
            // 如果两者相等（都是己方或都是敌方），说明不是对手打牌，直接返回
            if (wasOwnCard == triggerEffectMinion.own) return;

            // 根据魔能机甲的归属，减少对应方的牌库数量
            if (triggerEffectMinion.own)
            {
                // 如果魔能机甲属于己方，当对手打牌时，减少己方牌库顶3张牌
                // 使用Math.Max确保牌库数量不会变成负数
                p.ownDeckSize = Math.Max(0, p.ownDeckSize - 3);
            }
            else
            {
                // 如果魔能机甲属于敌方，当己方打牌时，减少敌方牌库顶3张牌
                // 使用Math.Max确保牌库数量不会变成负数
                p.enemyDeckSize = Math.Max(0, p.enemyDeckSize - 3);
            }
        }
    }
}