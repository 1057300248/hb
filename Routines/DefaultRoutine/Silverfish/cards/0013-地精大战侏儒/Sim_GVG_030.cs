using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 电镀机械熊仔卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力1/2，生命值2/3
    // 卡牌效果：<b>嘲讽，抉择：</b>+1攻击力；或者+1生命值。
    class Sim_GVG_030 : SimTemplate //* 电镀机械熊仔 Anodized Robo Cub
                                    //<b>Taunt</b>. <b>Choose One -</b>+1 Attack; or +1 Health.
                                    //<b>嘲讽，抉择：</b>+1攻击力；或者+1生命值。 
    {
        // 重写战吼效果方法，这是电镀机械熊仔卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 处理抉择选项1：+1攻击力
            // 如果玩家选择选项1（choice == 1）或者场上存在范达尔·鹿盔（使抉择同时获得两种效果）
            if (choice == 1 || (p.ownFandralStaghelm > 0 && own.own))
            {
                // 给电镀机械熊仔增加+1攻击力
                p.minionGetBuffed(own, 1, 0);
            }

            // 处理抉择选项2：+1生命值
            // 如果玩家选择选项2（choice == 2）或者场上存在范达尔·鹿盔（使抉择同时获得两种效果）
            if (choice == 2 || (p.ownFandralStaghelm > 0 && own.own))
            {
                // 给电镀机械熊仔增加+1生命值
                p.minionGetBuffed(own, 0, 1);
            }
        }
    }
}