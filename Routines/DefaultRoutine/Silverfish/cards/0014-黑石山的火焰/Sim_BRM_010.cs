using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 烈焰德鲁伊卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张德鲁伊职业随从卡牌，费用为3点，攻击力2，生命值2
    // 卡牌效果：<b>抉择：</b>将该随从变形成为5/2；或者将该随从变形成为2/5。
    class Sim_BRM_010 : SimTemplate //* 烈焰德鲁伊 Druid of the Flame
    // <b>Choose One -</b> Transform into a 5/2 minion; or a 2/5 minion.
    // <b>抉择：</b>将该随从变形成为5/2；或者将该随从变形成为2/5。 
    {
        // 定义各种变形后的卡牌
        CardDB.Card fireCat52 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.BRM_010t);    // 5/2 火猫
        CardDB.Card fireHawk25 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.BRM_010t2); // 2/5 火鹰
        CardDB.Card CatHawk55 = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.OG_044b);   // 5/5 变形选项（当范达尔·鹿盔在场时）

        // 重写战吼效果方法，这是烈焰德鲁伊卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 检查是否有范达尔·鹿盔效果（使所有抉择效果同时触发）
            if (p.ownFandralStaghelm > 0)
            {
                // 如果有范达尔·鹿盔，则将随从变形为5/5（同时获得两个形态的效果）
                p.minionTransform(own, CatHawk55);
            }
            else
            {
                // 根据选择的抉择效果进行变形
                if (choice == 1)
                {
                    // 选择第一个选项：变成5/2的火猫
                    p.minionTransform(own, fireCat52);
                }
                else if (choice == 2)
                {
                    // 选择第二个选项：变成2/5的火鹰
                    p.minionTransform(own, fireHawk25);
                }
                // 如果没有选择（choice为0），则不进行变形
            }
        }
    }
}