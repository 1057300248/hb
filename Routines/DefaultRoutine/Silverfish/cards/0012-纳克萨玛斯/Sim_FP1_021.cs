using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 死亡之咬卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业武器卡牌，费用为4点，攻击力2，耐久度3
    // 卡牌效果：<b>亡语：</b>对所有随从造成1点伤害。
    class Sim_FP1_021 : SimTemplate //* 死亡之咬 Death's Bite
    // <b>Deathrattle:</b> Deal 1 damage to all minions.
    // <b>亡语：</b>对所有随从造成1点伤害。
    {
        // 定义死亡之咬武器卡牌
        CardDB.Card weapon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.FP1_021);

        // 重写卡牌使用效果方法，这是死亡之咬卡牌的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 为指定玩家装备死亡之咬武器
            // 参数说明：- 要装备的武器卡牌，- 是否为己方装备
            p.equipWeapon(weapon, ownplay);
        }

        // 重写亡语效果方法，这是死亡之咬武器的亡语效果实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 对所有随从造成1点伤害
            p.allMinionsGetDamage(1);

            // 触发伤害相关的扳机效果
            p.doDmgTriggers();
        }
    }
}