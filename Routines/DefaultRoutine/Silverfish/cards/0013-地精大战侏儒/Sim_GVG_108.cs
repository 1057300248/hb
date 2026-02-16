using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 侏儒变形师卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力3，生命值2
    // 卡牌效果：<b>战吼：</b>将一个友方随从随机变形成为一个法力值消耗相同的随从。
    class Sim_GVG_108 : SimTemplate //* 侏儒变形师 Recombobulator
    // <b>Battlecry:</b> Transform a friendly minion into a random minion with the same Cost.
    // <b>战吼：</b>将一个友方随从随机变形成为一个法力值消耗相同的随从。 
    {
        // 重写战吼效果方法，这是侏儒变形师卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 如果没有目标，则直接返回
            if (target == null) return;

            // 将目标随从变形为相同费用的随机随从
            // 参数说明：- 目标随从，- 目标随从的卡牌信息（用于获取费用）
            p.minionTransform(target, target.handcard.card);
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含三个条件：
            // 1. REQ_MINION_TARGET - 目标必须是一个随从
            // 2. REQ_FRIENDLY_TARGET - 目标必须是友方角色
            // 3. REQ_TARGET_IF_AVAILABLE - 如果有可用目标，则必须选择一个目标
            // 这意味着必须指定一个友方随从作为目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
            };
        }
    }
}