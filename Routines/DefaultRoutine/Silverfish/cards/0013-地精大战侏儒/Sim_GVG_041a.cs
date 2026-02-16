using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 守护者的呼唤卡牌的模拟实现类，继承自SimTemplate基类
    // 这是黑暗私语的抉择选项之一，法师职业法术卡牌，费用为0点
    // 卡牌效果：+5/+5并具有<b>嘲讽</b>。
    class Sim_GVG_041a : SimTemplate //* 守护者的呼唤 Call the Guardians
    // +5/+5 and <b>Taunt</b>.
    // +5/+5并具有<b>嘲讽</b>。 
    {
        // 重写卡牌打出时的效果方法，这是守护者的呼唤卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 检查目标是否有效（不为null）
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

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含两个条件：
            // 1. REQ_TARGET_TO_PLAY - 需要选择一个目标才能打出此卡牌
            // 2. REQ_MINION_TARGET - 目标必须是一个随从
            // 这意味着必须指定一个随从作为目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
            };
        }
    }
}