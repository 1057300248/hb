using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_EDR_941 : SimTemplate
    {
        // 基础伤害值
        private const int BaseDamage = 1;

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 获取友方随从死亡计数（通过自定义计数器）
            int friendlyDeaths = ownplay ? p.ownMinionsDied : p.enemyMinionsDied;

            // 计算总伤害 = 基础伤害 + 死亡计数
            int totalDamage = BaseDamage + friendlyDeaths;

            // 应用伤害
            p.minionGetDamageOrHeal(target, totalDamage);
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),
            };
        }
    }
}