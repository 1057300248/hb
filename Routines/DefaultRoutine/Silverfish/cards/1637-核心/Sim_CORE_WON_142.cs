using Microsoft.Scripting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HREngine.Bots
{
    //随从 中立 费用：5 攻击力：3 生命值：3
    //Menagerie Jug
    //展馆茶壶
    //[x]<b>Battlecry:</b> Give 3 randomfriendly minions of differentminion types +3/+3.
    //<b>战吼：</b>随机使三个不同类型的友方随从获得+3/+3。
    class Sim_CORE_WON_142 : SimTemplate
    {
        private static Random _rnd = new Random();

        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            List<Minion> temp = own.own ? p.ownMinions : p.enemyMinions;
            var raceGroups = new Dictionary<TAG_RACE, List<Minion>>();

            // 收集所有有效种族的随从
            foreach (Minion m in temp)
            {
                TAG_RACE race = (TAG_RACE)m.handcard.card.race;
                if (race == TAG_RACE.INVALID) continue;

                if (!raceGroups.ContainsKey(race))
                    raceGroups[race] = new List<Minion>();
                raceGroups[race].Add(m);
            }

            List<TAG_RACE> availableRaces = new List<TAG_RACE>(raceGroups.Keys);
            HashSet<Minion> buffed = new HashSet<Minion>();
            int buffedCount = 0;

            // 确保最多选3个不同种族
            while (buffedCount < 3 && availableRaces.Count > 0)
            {
                int raceIdx = _rnd.Next(availableRaces.Count);
                TAG_RACE selectedRace = availableRaces[raceIdx];
                List<Minion> candidates = raceGroups[selectedRace].Where(m => !buffed.Contains(m)).ToList();

                if (candidates.Count == 0)
                {
                    availableRaces.RemoveAt(raceIdx);
                    continue;
                }

                Minion chosen = candidates[_rnd.Next(candidates.Count)];
                p.minionGetBuffed(chosen, 3, 3);
                buffed.Add(chosen);
                availableRaces.RemoveAt(raceIdx); // 确保每个种族只选一次
                buffedCount++;
            }
        }
    }
}