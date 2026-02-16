using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 黑翼腐蚀者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张中立职业随从卡牌，费用为5点，攻击力5，生命值4
    // 卡牌效果：<b>战吼：</b>如果你的手牌中有龙牌，则造成3点伤害。
    class Sim_BRM_034 : SimTemplate //* 黑翼腐蚀者 Blackwing Corruptor
    // <b>Battlecry:</b> If you're holding a Dragon, deal 3 damage.
    // <b>战吼：</b>如果你的手牌中有龙牌，则造成3点伤害。 
    {
        // 重写战吼效果方法，这是黑翼腐蚀者卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 检查己方手牌中是否有龙牌
            bool dragonInHand = false;
            foreach (Handmanager.Handcard hc in p.owncards)
            {
                // 检查卡牌种族是否为龙
                if ((TAG_RACE)hc.card.race == TAG_RACE.DRAGON)
                {
                    dragonInHand = true;
                    break;
                }
            }
            // 检查目标是否存在和手牌中有龙牌
            if (dragonInHand && target != null)
            {
                // 对目标造成3点伤害
                p.minionGetDamageOrHeal(target, 3);
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含一个条件：
            // 1. REQ_TARGET_IF_AVAILABLE_AND_DRAGON_IN_HAND - 如果手牌中有龙牌则必须指定目标
            // 这意味着只有当手牌中有龙牌时，才需要指定一个目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE_AND_DRAGON_IN_HAND),
            };
        }
    }
}