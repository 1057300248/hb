using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 林地树妖卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力2，生命值4
    // 卡牌效果：<b>抉择：</b>使每个玩家获得一个法力水晶；或每个玩家抽一张牌。
    class Sim_GVG_032 : SimTemplate //* 林地树妖 Grove Tender
    // <b>Choose One -</b> Give each player a Mana Crystal; or Each player draws a card.
    // <b>抉择：</b>使每个玩家获得一个法力水晶；或每个玩家抽一张牌。 
    {
        // 重写战吼效果方法，这是林地树妖卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 处理抉择选项1：给每个玩家一个法力水晶
            // 如果玩家选择选项1（choice == 1）或者场上存在范达尔·鹿盔（使抉择同时获得两种效果）
            if (choice == 1 || (p.ownFandralStaghelm > 0 && own.own))
            {
                // 增加当前法力值（上限为10）
                p.mana = Math.Min(10, p.mana + 1);

                // 增加己方法力水晶上限（上限为10）
                p.ownMaxMana = Math.Min(10, p.ownMaxMana + 1);

                // 增加敌方法力水晶上限（上限为10）
                p.enemyMaxMana = Math.Min(10, p.enemyMaxMana + 1);
            }

            // 处理抉择选项2：每个玩家抽一张牌
            // 如果玩家选择选项2（choice == 2）或者场上存在范达尔·鹿盔（使抉择同时获得两种效果）
            if (choice == 2 || (p.ownFandralStaghelm > 0 && own.own))
            {
                // 己方玩家抽一张牌
                p.drawACard(CardDB.cardIDEnum.None, true);

                // 敌方玩家抽一张牌
                p.drawACard(CardDB.cardIDEnum.None, false);
            }
        }
    }
}