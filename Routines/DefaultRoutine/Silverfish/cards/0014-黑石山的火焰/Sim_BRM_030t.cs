using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 扫尾卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业法术卡牌，费用为4点
    // 卡牌效果：造成4点伤害。
    class Sim_BRM_030t : SimTemplate //* 扫尾 Tail Swipe
    // Deal $4 damage.
    // 造成$4点伤害。 
    {
        // 重写卡牌使用效果方法，这是扫尾卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 计算伤害值，考虑法术伤害加成
            int damage = (ownplay) ? p.getSpellDamageDamage(4) : p.getEnemySpellDamageDamage(4);

            // 对目标造成伤害
            p.minionGetDamageOrHeal(target, damage);
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含一个条件：
            // 1. REQ_TARGET_TO_PLAY - 需要指定一个目标
            // 这意味着必须指定一个目标才能使用这张法术
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
    }
}