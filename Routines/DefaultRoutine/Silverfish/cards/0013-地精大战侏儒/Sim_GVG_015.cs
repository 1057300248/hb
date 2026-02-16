using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 暗色炸弹卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为2点
    // 卡牌效果：造成$3点伤害。
    class Sim_GVG_015
        : SimTemplate //* 暗色炸弹 Darkbomb
    // Deal $3 damage.
    // 造成$3点伤害。 
    {
        // 重写卡牌打出时的效果方法，这是暗色炸弹卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 根据是否是己方打出，计算实际造成的伤害值（考虑法术伤害加成）
            // 如果是己方打出，使用己方的法术伤害加成；如果是敌方打出，使用敌方的法术伤害加成
            int dmg = (ownplay) ? p.getSpellDamageDamage(3) : p.getEnemySpellDamageDamage(3);

            // 对目标造成指定伤害
            // 调用游戏场地的minionGetDamageOrHeal方法，传递正的伤害值表示造成伤害
            p.minionGetDamageOrHeal(target, dmg);
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含一个条件：
            // REQ_TARGET_TO_PLAY - 需要选择一个目标才能打出此卡牌
            // 这意味着必须指定一个角色（英雄或随从）作为伤害目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
    }
}