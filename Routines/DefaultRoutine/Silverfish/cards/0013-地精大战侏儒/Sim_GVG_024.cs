using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 齿轮大师的扳手卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业武器卡牌，费用为1点，攻击力1，耐久度3
    // 卡牌效果：如果你控制任何机械，便获得+2攻击力。
    class Sim_GVG_024 : SimTemplate //* 齿轮大师的扳手 Cogmaster's Wrench
    // Has +2 Attack while you have a Mech.
    // 如果你控制任何机械，便获得+2攻击力。 
    {
        // 在类级别定义齿轮大师的扳手卡牌对象，用于装备武器
        CardDB.Card w = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_024);

        // 重写卡牌打出时的效果方法，这是齿轮大师的扳手卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 装备齿轮大师的扳手武器
            p.equipWeapon(w, ownplay);

            // 根据是否是己方打出，确定要检查的随从列表
            List<Minion> temp = (ownplay) ? p.ownMinions : p.enemyMinions;

            // 遍历对应的随从列表，检查是否存在机械随从
            foreach (Minion m in temp)
            {
                // 检查随从种族是否为机械
                if ((TAG_RACE)m.handcard.card.race == TAG_RACE.MECHANICAL)
                {
                    // 如果存在机械随从，则给对应方的武器增加+2攻击力
                    if (ownplay)
                    {
                        // 增加己方武器攻击力+2
                        p.ownWeapon.Angr += 2;
                        // 同时给己方英雄增加+2攻击力（因为武器攻击力会反映在英雄攻击力上）
                        p.minionGetBuffed(p.ownHero, 2, 0);
                    }
                    else
                    {
                        // 增加敌方武器攻击力+2
                        p.enemyWeapon.Angr += 2;
                        // 同时给敌方英雄增加+2攻击力
                        p.minionGetBuffed(p.enemyHero, 2, 0);
                    }
                    // 找到第一个机械随从后立即跳出循环
                    break;
                }
            }
        }

        // 重写随从被召唤时的触发方法，当有新的随从被召唤到场上时调用
        public override void onMinionIsSummoned(Playfield p, Minion triggerEffectMinion, Minion summonedMinion)
        {
            // 检查被召唤的随从是否是机械种族
            if ((TAG_RACE)summonedMinion.handcard.card.race == TAG_RACE.MECHANICAL)
            {
                // 根据触发效果的随从归属，确定要检查的随从列表
                List<Minion> temp = (triggerEffectMinion.own) ? p.ownMinions : p.enemyMinions;

                // 遍历对应的随从列表，检查是否已经存在其他机械随从
                foreach (Minion m in temp)
                {
                    // 如果已经存在机械随从，则直接返回，不应用增益效果
                    // 这是因为齿轮大师的扳手的效果应该在有机械时就已激活，不需要重复添加
                    if ((TAG_RACE)m.handcard.card.race == TAG_RACE.MECHANICAL) return;
                }

                // 如果之前没有机械随从，现在召唤了机械，那么给对应方的武器+2攻击力
                if (triggerEffectMinion.own)
                {
                    // 增加己方武器攻击力+2
                    p.ownWeapon.Angr += 2;
                    // 同时给己方英雄增加+2攻击力
                    p.minionGetBuffed(p.ownHero, 2, 0);
                }
                else
                {
                    // 增加敌方武器攻击力+2
                    p.enemyWeapon.Angr += 2;
                    // 同时给敌方英雄增加+2攻击力
                    p.minionGetBuffed(p.enemyHero, 2, 0);
                }
            }
        }

        // 重写随从死亡时的触发方法，当有随从死亡时调用
        public override void onMinionDiedTrigger(Playfield p, Minion m, Minion diedMinion)
        {
            // 获取本回合死亡的机械随从数量
            int diedMinions = (m.own) ? p.tempTrigger.ownMechanicDied : p.tempTrigger.enemyMechanicDied;

            // 如果没有机械死亡，直接返回
            if (diedMinions == 0) return;

            // 计算剩余的死亡机械数量（避免重复处理）
            int residual = (p.pID == m.pID) ? diedMinions - m.extraParam2 : diedMinions;

            // 更新随从的处理状态，标记为已处理
            m.pID = p.pID;
            m.extraParam2 = diedMinions;

            // 如果有机械死亡需要处理
            if (residual >= 1)
            {
                // 根据随从m的归属，确定要检查的随从列表
                List<Minion> temp = (m.own) ? p.ownMinions : p.enemyMinions;

                // 标记是否存在机械随从
                bool hasmechanics = false;

                // 遍历对应的随从列表，检查是否还有存活的机械随从
                foreach (Minion mTmp in temp)
                {
                    // 检查随从是否存活（Hp >= 1）且是机械种族
                    if (mTmp.Hp >= 1 && (TAG_RACE)mTmp.handcard.card.race == TAG_RACE.MECHANICAL)
                        hasmechanics = true;
                }

                // 如果没有存活的机械随从，则移除齿轮大师的扳手的+2攻击力增益
                if (!hasmechanics)
                {
                    if (m.own)
                    {
                        // 减少己方武器攻击力-2
                        p.ownWeapon.Angr -= 2;
                        // 同时给己方英雄减少-2攻击力
                        p.minionGetBuffed(p.ownHero, -2, 0);
                    }
                    else
                    {
                        // 减少敌方武器攻击力-2
                        p.enemyWeapon.Angr -= 2;
                        // 同时给敌方英雄减少-2攻击力
                        p.minionGetBuffed(p.enemyHero, -2, 0);
                    }
                }
            }
        }
    }
}