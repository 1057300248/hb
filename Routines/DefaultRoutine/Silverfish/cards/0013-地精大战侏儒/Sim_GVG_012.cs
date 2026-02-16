using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 纳鲁之光卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为1点
    // 卡牌效果：恢复#3点生命值。如果该目标仍处于受伤状态，则召唤一个圣光护卫者。
    class Sim_GVG_012 : SimTemplate //* 纳鲁之光 Light of the Naaru
    // Restore #3 Health. If the target is still damaged, summon a Lightwarden.
    // 恢复#3点生命值。如果该目标仍处于受伤状态，则召唤一个圣光护卫者。 
    {
        // 在类级别定义圣光护卫者卡牌对象，作为要召唤的随从
        // 通过CardDB获取EX1_001（圣光护卫者）的卡牌数据
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EX1_001);

        // 重写卡牌打出时的效果方法，这是纳鲁之光卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 根据是否是己方打出，计算实际的治疗量（考虑法术治疗加成）
            // 如果是己方打出，使用己方的法术治疗加成；如果是敌方打出，使用敌方的法术治疗加成
            int heal = (ownplay) ? p.getSpellHeal(3) : p.getEnemySpellHeal(3);

            // 对目标进行治疗，通过传递负的伤害值来实现治疗效果
            // p.minionGetDamageOrHeal方法中，负值表示治疗，正值表示伤害
            p.minionGetDamageOrHeal(target, -heal);

            // 检查治疗后目标是否仍然处于受伤状态（当前生命值小于最大生命值）
            if (target.Hp < target.maxHp)
            {
                // 确定召唤位置：如果是己方打出，则在己方随从列表末尾召唤；如果是敌方打出，则在敌方随从列表末尾召唤
                int posi = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;

                // 调用callKid方法召唤圣光护卫者随从
                // 参数说明：
                // - kid: 要召唤的卡牌对象（圣光护卫者）
                // - posi: 召唤位置（在随从列表的末尾）
                // - ownplay: 指示召唤到哪一方的场上（true为己方，false为敌方）
                p.callKid(kid, posi, ownplay);
            }
        }

        // 重写获取卡牌打出条件的方法，定义此卡牌的使用要求
        public override PlayReq[] GetPlayReqs()
        {
            // 返回卡牌的打出条件数组，包含一个条件：
            // REQ_TARGET_TO_PLAY - 需要选择一个目标才能打出此卡牌
            // 这意味着必须指定一个角色（英雄或随从）作为治疗目标
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
            };
        }
    }
}