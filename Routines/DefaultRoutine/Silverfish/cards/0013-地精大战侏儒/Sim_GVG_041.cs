using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 黑暗私语卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为6点
    // 卡牌效果：<b>抉择：</b>召唤5个小精灵；或者使一个随从获得+5/+5和<b>嘲讽</b>。
    class Sim_GVG_041 : SimTemplate //* 黑暗私语 Dark Wispers
    // <b>Choose One -</b> Summon 5 Wisps; or Give_a minion +5/+5 and <b>Taunt</b>.
    // <b>抉择：</b>召唤5个小精灵；或者使一个随从获得+5/+5和<b>嘲讽</b>。 
    {
        // 在类级别定义小精灵卡牌对象，用于召唤
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_231);

        // 重写卡牌打出时的效果方法，这是黑暗私语卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 处理抉择选项1：召唤5个小精灵
            // 如果玩家选择选项1（choice == 1）或者场上存在范达尔·鹿盔（使抉择同时获得两种效果）
            if (choice == 1 || (p.ownFandralStaghelm > 0 && ownplay))
            {
                // 循环5次召唤小精灵
                for (int i = 0; i < 5; i++)
                {
                    // 确定召唤位置：己方随从数量或敌方随从数量
                    int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;

                    // 召唤小精灵到指定位置
                    p.callKid(kid, pos, ownplay);
                }
            }

            // 处理抉择选项2：给随从+5/+5和嘲讽
            // 如果玩家选择选项2（choice == 2）或者场上存在范达尔·鹿盔（使抉择同时获得两种效果）
            if (choice == 2 || (p.ownFandralStaghelm > 0 && ownplay))
            {
                // 检查目标是否有效
                if (target != null)
                {
                    // 给目标随从增加+5攻击力和+5生命值
                    p.minionGetBuffed(target, 5, 5);

                    // 如果目标随从原本没有嘲讽，则添加嘲讽效果
                    if (!target.taunt)
                    {
                        // 设置嘲讽标志
                        target.taunt = true;

                        // 更新嘲讽随从计数器
                        if (target.own) p.anzOwnTaunt++;
                        else p.anzEnemyTaunt++;
                    }
                }
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含两个条件：
            // 1. REQ_MINION_TARGET - 目标必须是一个随从
            // 2. REQ_TARGET_IF_AVAILABLE - 如果有可用目标，则必须选择一个目标
            // 这意味着如果选择第二个选项，必须指定一个随从作为目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
            };
        }
    }
}