using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 废旧螺栓机甲卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力2，生命值5
    // 卡牌效果：<b>战吼：</b>使一个友方机械获得+2/+2。
    class Sim_GVG_055 : SimTemplate //* 废旧螺栓机甲 Screwjank Clunker
    // <b>Battlecry:</b> Give a friendly Mech +2/+2.
    // <b>战吼：</b>使一个友方机械获得+2/+2。 
    {
        // 重写战吼效果方法，这是废旧螺栓机甲卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 检查目标是否有效（不为null）
            if (target == null) return;

            // 给目标机械随从增加+2攻击力和+2生命值
            p.minionGetBuffed(target, 2, 2);
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含三个条件：
            // 1. REQ_TARGET_WITH_RACE, 17 - 目标必须是机械种族（17代表机械）
            // 2. REQ_FRIENDLY_TARGET - 目标必须是友方角色
            // 3. REQ_TARGET_IF_AVAILABLE - 如果有可用目标，则必须选择一个目标
            // 这意味着必须指定一个友方机械随从作为目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_WITH_RACE, 17),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
            };
        }
    }
}