using System.ComponentModel;
using Exiled.API.Interfaces;

namespace Scp096Nerf
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Whether 096 should calm down after all his targets are killed.")]
        public bool CalmDownAfterTargetsDead { get; set; } = true;

        [Description("The damage 098 will take per tick per target.")]
        public float DamagePerTarget { get; set; } = 7.5f;

        [Description("The amount by which the damage taken per tick will increase (leave at 0 for no linear increase in damage taken).")]
        public float DamagePerSecondEnraged { get; set; } = 0.5f;

        [Description("The amount of resistance 098 has when it's taking non-AHP damage (for the default value: 3, 096 will only take 1/3 of the set damage in regular HP).")]
        public float RegularHpResistance { get; set; } = 3;

        [Description("The amount of shield to be recharged per second while enraged.")]
        public float ShieldRechargeRateEnraged { get; set; } = 0;

        [Description("The amount of shield to be recharged per second while not enraged.")]
        public float ShieldRechargeRate { get; set; } = 5;

        [Description("Whether the target should get a message when targeted by 096.")]
        public bool SendTargetMessage { get; set; } = true;

        [Description("The message a player sees when it becomes a target for 096.")]
        public string TargetMessage { get; set; } = "<color=red>You've become a target for SCP-096!</color>";

        [Description("The tickrate of 096 taking damage.")]
        public int Tickrate { get; set; } = 25;
    }
}
