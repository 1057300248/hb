using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 缩小射线工程师卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力3，生命值2
    // 卡牌效果：<b>战吼：</b>在本回合中，使一个随从获得-2攻击力。
    class Sim_GVG_011 : SimTemplate //* 缩小射线工程师 Shrinkmeister
    //<b>Battlecry:</b> Give a minion -2_Attack this turn.
    //<b>战吼：</b>在本回合中，使一个随从获得-2攻击力。 
    {
        // 重写战吼效果方法，这是缩小射线工程师卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 检查目标随从是否有效（不为null）
            if (target != null)
            {
                // 对目标随从应用临时的-2攻击力和0生命值变化的增益效果
                // 这个效果只在当前回合生效，下回合会自动消失
                // 参数说明：
                // - target: 目标随从对象
                // - -2: 攻击力减少2点
                // - 0: 生命值不变
                p.minionGetTempBuff(target, -2, 0);
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含两个条件：
            // 1. REQ_MINION_TARGET - 目标必须是一个随从
            // 2. REQ_TARGET_IF_AVAILABLE - 如果有可用目标，则必须选择一个目标
            // 这意味着如果场上没有随从，这张卡可以无目标打出；如果有随从，则必须选择一个目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
            };
        }
    }
}