using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class Monster
    {
        public string Name { get; }  // 이름
        public int MaxHP { get; }  // 최대 체력
        public int HP { get; set; }  // 현재 체력
        protected int SkillDamage; // 스킬 데미지
        public int MonsterSkillCount { get; set; }  // 스킬 사용 가능 횟수
        public int MonsterGuardCount { get; set; }  // 방어 사용 가능 횟수

        private Random rand = new Random();

        public Monster(string name, int maxHP, int skillDamage)
        {
            Name = name;
            MaxHP = maxHP;
            HP = maxHP;
            SkillDamage = skillDamage;
            MonsterSkillCount = 3;
            MonsterGuardCount = 3;
        }

        public void Attack()
        {
            Console.WriteLine($"{Name}이(가) 공격했습니다. (1 Damage)");
        }

        public abstract void Skill();
        
        public void Guard()
        {
            Console.WriteLine($"{Name}이(가) 방어했습니다.");
        }

        // 스킬/방어 사용 가능 횟수 감소
        public void ReduceMonsterCount(BattleAction action)
        {
            if (action == BattleAction.Skill && MonsterSkillCount > 0) MonsterSkillCount--;
            if (action == BattleAction.Guard && MonsterGuardCount > 0) MonsterGuardCount--;
        }

        // 몬스터 행동 랜덤 선택
        public BattleAction SelectAction()
        {
            // 몬스터 행동 가능 리스트
            List<BattleAction> actions = new List<BattleAction> {BattleAction.Attack};

            if (MonsterSkillCount > 0) actions.Add(BattleAction.Skill);
            if (MonsterGuardCount > 0) actions.Add(BattleAction.Guard);

            int index = rand.Next(actions.Count);
            return actions[index];
        }

        // 플레이어가 받는 데미지 계산
        public int GetDamage(BattleAction action)
        {
            int damage;
            if (action == BattleAction.Attack) damage = 1;
            else if (action == BattleAction.Skill) damage = SkillDamage;
            else damage = 0;

            return damage;
        }
    }
}