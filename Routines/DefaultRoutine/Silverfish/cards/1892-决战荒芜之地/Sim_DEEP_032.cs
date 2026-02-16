using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 术士 费用：1
	//Soulfreeze
	//灵魂冻结
	//<b>Freeze</b> a minionand its neighbors. Deal damage to your hero equal to the number <b>Frozen</b>.
	//<b>冻结</b>一个随从及其相邻随从，对你的英雄造成等同于所<b>冻结</b>随从数量的伤害。
	class Sim_DEEP_032 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
		 // 获取目标随从的相邻随从
            List<Minion> adjacentMinions = getAdjacentMinions(p, target);

            // 冻结相邻的随从
            foreach (Minion adjacent in adjacentMinions)
            {
                if (adjacent != null)
                {
                    adjacent.frozen = true; // 冻结相邻随从
                }
            }
		}

		/// <summary>
        /// 获取目标随从的相邻随从。
        /// </summary>
        /// <param name="p">场地对象</param>
        /// <param name="target">目标随从</param>
        /// <returns>相邻随从列表</returns>
        private List<Minion> getAdjacentMinions(Playfield p, Minion target)
        {
            List<Minion> adjacentMinions = new List<Minion>();

            if (target.own)
            {
                if (target.zonepos - 1 >= 0)
                {
                    adjacentMinions.Add(p.ownMinions[target.zonepos - 1]); // 左边相邻随从
                }
                if (target.zonepos < p.ownMinions.Count - 1)
                {
                    adjacentMinions.Add(p.ownMinions[target.zonepos + 1]); // 右边相邻随从
                }
            }
            else
            {
                if (target.zonepos - 1 >= 0)
                {
                    adjacentMinions.Add(p.enemyMinions[target.zonepos - 1]); // 左边相邻随从
                }
                if (target.zonepos < p.enemyMinions.Count - 1)
                {
                    adjacentMinions.Add(p.enemyMinions[target.zonepos + 1]); // 右边相邻随从
                }
            }

            return adjacentMinions;
        }
		public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),   // 目标必须是随从
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),    // 目标必须是敌方
            };
        }
		
	}
}
