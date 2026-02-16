using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 布林顿3000型卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力3，生命值4
    // 卡牌效果：<b>战吼：</b>为每个玩家装备一把武器。
    class Sim_GVG_119 : SimTemplate //* 布林顿3000型 Blingtron 3000
    // <b>Battlecry:</b> Equip a random weapon for each player.
    // <b>战吼：</b>为每个玩家装备一把武器。
    {
        // 定义要装备的武器卡牌（这里以刺客之刃为例）
        CardDB.Card weapon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_080);

        // 重写战吼效果方法，这是布林顿3000型卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 为己方玩家装备武器
            // 参数说明：- 要装备的武器卡牌，- true表示为己方装备
            p.equipWeapon(weapon, true);

            // 为敌方玩家装备武器
            // 参数说明：- 要装备的武器卡牌，- false表示为敌方装备
            p.equipWeapon(weapon, false);
        }
    }
}