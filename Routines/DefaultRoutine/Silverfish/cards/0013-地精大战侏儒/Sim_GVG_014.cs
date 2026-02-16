using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 沃金卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力6，生命值2
    // 卡牌效果：<b>战吼：</b>与另一个随从交换生命值。
    class Sim_GVG_014 : SimTemplate //* 沃金 Vol'jin
    //<b>Battlecry:</b> Swap Health with another minion.
    //<b>战吼：</b>与另一个随从交换生命值。 
    {
        // 重写战吼效果方法，这是沃金卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 检查目标随从是否有效（不为null），如果无效则直接返回
            if (target == null) return;

            // 保存目标随从的当前生命值到沃金的最大生命值属性
            // 这样做是为了在后续步骤中正确设置沃金的生命值
            own.maxHp = target.Hp;

            // 保存沃金的当前生命值到目标随从的最大生命值属性
            // 这样做是为了在后续步骤中正确设置目标随从的生命值
            target.maxHp = own.Hp;

            // 将沃金的生命值设置为其新的最大生命值（即原来目标的生命值）
            own.Hp = own.maxHp;

            // 将目标随从的生命值设置为其新的最大生命值（即原来沃金的生命值）
            target.Hp = target.maxHp;

            // 检查目标随从是否处于受伤状态（wounded标记为true）
            if (target.wounded)
            {
                // 如果目标随从处于受伤状态，则清除其受伤标记
                target.wounded = false;

                // 调用目标随从卡牌的激怒结束回调方法
                // 这是因为生命值变化可能导致激怒状态的改变
                target.handcard.card.sim_card.onEnrageStop(p, target);
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