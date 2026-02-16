using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 赫米特·奈辛瓦里卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力6，生命值3
    // 卡牌效果：<b>战吼：</b>消灭一个野兽。
    class Sim_GVG_120 : SimTemplate //* 赫米特·奈辛瓦里 Hemet Nesingwary
    // <b>Battlecry:</b> Destroy a Beast.
    // <b>战吼：</b>消灭一个野兽。
    {
        // 重写战吼效果方法，这是赫米特·奈辛瓦里卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 如果没有指定目标，则直接返回
            if (target == null) return;

            // 消灭指定的目标野兽随从
            p.minionGetDestroyed(target);
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含两个条件：
            // 1. REQ_TARGET_WITH_RACE, 20 - 目标必须是指定种族（20代表野兽）
            // 2. REQ_TARGET_IF_AVAILABLE - 如果有可用目标，则必须选择一个目标
            // 这意味着必须指定一个野兽随从作为目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_WITH_RACE, 20),
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),
            };
        }
    }
}