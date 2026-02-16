using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 尖牙德鲁伊卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力4，生命值4
    // 卡牌效果：<b>战吼：</b>如果你控制任何野兽，将该随从变形成为7/7。
    class Sim_GVG_080 : SimTemplate //* 尖牙德鲁伊 Druid of the Fang
    // <b>Battlecry:</b> If you have a Beast, transform this minion into a 7/7.
    // <b>战吼：</b>如果你控制任何野兽，将该随从变形成为7/7。 
    {
        // 在类级别定义变形后的卡牌对象（7/7的尖牙德鲁伊）
        CardDB.Card betterguy = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_080t);

        // 重写战吼效果方法，这是尖牙德鲁伊卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 根据随从的归属确定要检查的随从列表（己方或敌方）
            List<Minion> temp = (own.own) ? p.ownMinions : p.enemyMinions;

            // 标记是否存在野兽随从
            bool 自己场上存在野兽随从 = false;

            // 遍历所有随从，检查是否存在野兽
            foreach (Minion m in temp)
            {
                // 检查随从种族是否为野兽（使用PET表示野兽）
                if ((TAG_RACE)m.handcard.card.race == TAG_RACE.PET)
                {
                    自己场上存在野兽随从 = true;
                    break;
                }
            }

            // 如果存在野兽，则将尖牙德鲁伊变形为7/7版本
            if (自己场上存在野兽随从)
            {
                p.minionTransform(own, betterguy);
            }
        }
    }
}