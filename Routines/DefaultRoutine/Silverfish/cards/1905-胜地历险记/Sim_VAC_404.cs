using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //法术 潜行者 费用：1
    //Nightshade Tea
    //夜影花茶
    //Deal $2 damage to a minion. Deal $2 damage to your hero.<i>(3 Drinks left!)</i>
    //对一个随从造成$2点伤害。对你的英雄造成$2点伤害。<i>（还剩3杯！）</i>
    class Sim_VAC_404 : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 安全检查
            if (p == null)
            {
                Helpfunctions.Instance.ErrorLog("Sim_VAC_404错误: Playfield为null");
                return;
            }

            try
            {
                int dmg = (ownplay) ? p.getSpellDamageDamage(2) : p.getEnemySpellDamageDamage(2);

                // 对一个随从造成2点伤害
                if (target != null)
                {
                    p.minionGetDamageOrHeal(target, dmg);
                }
                else
                {
                    Helpfunctions.Instance.ErrorLog("Sim_VAC_404警告: 目标随从为null");
                }

                // 获取英雄对象
                Minion hero = ownplay ? p.ownHero : p.enemyHero;
                
                // 对你的英雄造成2点伤害
                if (hero != null)
                {
                    p.minionGetDamageOrHeal(hero, dmg);
                }
                else
                {
                    Helpfunctions.Instance.ErrorLog("Sim_VAC_404错误: 英雄对象为null");
                }

                // 抽一张 "VAC_404t1" 卡牌，表示"还剩2杯"
                if (CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.VAC_404t1) != null)
                {
                    p.drawACard(CardDB.cardIDEnum.VAC_404t1, ownplay, true);
                }
                else
                {
                    Helpfunctions.Instance.ErrorLog("Sim_VAC_404警告: 找不到后续茶牌");
                }
                
                // 记录日志
                if (p.logging)
                {
                    Helpfunctions.Instance.logg("夜影花茶: 造成" + dmg + "点伤害，抽取了后续茶牌");
                }
            }
            catch (Exception ex)
            {
                // 捕获并记录任何可能的异常
                Helpfunctions.Instance.ErrorLog("Sim_VAC_404异常: " + ex.ToString());
            }
        }
        
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET)
            };
        }
    }
}