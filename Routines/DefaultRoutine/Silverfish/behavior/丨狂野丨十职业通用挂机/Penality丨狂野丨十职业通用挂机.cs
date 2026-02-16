using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HREngine.Bots
{
    //每个策略的 Penality{策略名}文件里面放 三个函数：打牌评分，随从进攻评分；英雄进攻评分 以及他们需要用到的private函数
    //这三个函数用于单动作评估，如果返回值超过500，则被剪枝，不列入候选动作
    public partial class Behavior丨狂野丨十职业通用挂机 : Behavior
    {
        public override int getPlayCardPenality(CardDB.Card card, Minion target, Playfield p, Handmanager.Handcard nowHandcard)
        {
            return getComboPenality(card, target, p, nowHandcard);
        }

       /* public override int getAttackWithMininonPenality(Minion m, Playfield p, Minion target)
        {
            if (!m.silenced && m.handcard.card.CantAttack || target.untouchable)
                return 1000;
            
            // 禁止商品卖家攻击
            if (m.handcard.card.nameCN == CardDB.cardNameCN.商品卖家)
                return 1000;
                
            int pen = -10;
            if (target.isHero && p.calTotalAngr() >= p.enemyHero.Hp)
            {
                return -1000;
            }
            return pen;
        }


        //英雄攻击惩罚
        public override int getAttackWithHeroPenality(Minion target, Playfield p) 
        {
            if (target.untouchable)
                return 1000;
            // 如果拥有武器，则返回极高惩罚值，禁止武器攻击任何目标
            //if (p.ownWeapon.Durability > 0)
            //    return 2000;
            int pen = -10;
            if (target.isHero && p.calTotalAngr() >= p.enemyHero.Hp) return -1000;
            return pen;
        }*/

        private int getSecretPenality(Playfield p)  // 牌序和防奥秘的影响
        {
            return 0;
        }
    }
}
