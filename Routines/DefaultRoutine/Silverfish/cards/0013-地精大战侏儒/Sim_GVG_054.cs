using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 食人魔战槌卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业武器卡牌，费用为3点，攻击力4，耐久度2
    // 卡牌效果：50%几率攻击错误的敌人。
    class Sim_GVG_054 : SimTemplate //* 食人魔战槌 Ogre Warmaul
    // 50% chance to attack the wrong enemy.
    // 50%几率攻击错误的敌人。 
    {
        // 在类级别定义食人魔战槌卡牌对象，用于装备武器
        CardDB.Card w = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_054);

        // 重写卡牌打出时的效果方法，这是食人魔战槌卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 装备食人魔战槌武器
            p.equipWeapon(w, ownplay);
        }
    }
}