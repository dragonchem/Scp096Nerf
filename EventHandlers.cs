using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System.Collections;
using System.Collections.Generic;
using Handlers = Exiled.Events.Handlers;
using PlayableScps;
using MEC;
using System.Threading.Tasks;
using System;

namespace Scp096Nerf
{
    public class EventHandlers
    {
        private bool bTimerEnabled = false;
        private int iTime = 0;
        private float dDamageToBeDone = 0;
        
        public static Scp096Nerf plugin;

        public EventHandlers(Scp096Nerf pl)
        {
            plugin = pl;
        }

        public void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (plugin.Config.SendTargetMessage)
            {
                ev.Target.Broadcast(5, plugin.Config.TargetMessage);
            }
            dDamageToBeDone += plugin.Config.DamagePerTarget;
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Killer.Role == RoleType.Scp096)
            {
                dDamageToBeDone -= plugin.Config.DamagePerTarget;
            }
        }

        public void OnCalmingDown(CalmingDownEventArgs ev)
        {
            StopDamageTimer();
            ev.Scp096.ShieldRechargeRate = plugin.Config.ShieldRechargeRate;
        }

        public void OnEnraging (EnragingEventArgs ev)
        {
            ev.Scp096.ShieldRechargeRate = plugin.Config.ShieldRechargeRateEnraged;
            StartDamageTimer(ev);
        }

        private async void DamageTimer(EnragingEventArgs ev)
        {
            iTime = plugin.Config.Tickrate;
            dDamageToBeDone = 0;
            while (bTimerEnabled)
            {
                if (ev.Scp096._targets.Count < 1 && plugin.Config.CalmDownAfterTargetsDead)
                {
                    ev.Scp096.EndEnrage();
                }
                else if (iTime == plugin.Config.Tickrate)
                {
                    if (ev.Player.AdrenalineHealth > dDamageToBeDone)
                    {
                        ev.Player.AdrenalineHealth -= dDamageToBeDone;
                    }
                    else
                    {
                        ev.Player.Health -= (dDamageToBeDone - ev.Player.AdrenalineHealth) / plugin.Config.RegularHpResistance;
                        ev.Player.AdrenalineHealth = 0;
                    }
                    dDamageToBeDone += plugin.Config.DamagePerSecondEnraged;
                    iTime = 0;
                }
                await Task.Delay(TimeSpan.FromMilliseconds(1));
                iTime++;
            }
        }

        private void StartDamageTimer(EnragingEventArgs ev)
        {
            bTimerEnabled = true;
            DamageTimer(ev);
        }

        private void StopDamageTimer()
        {
            bTimerEnabled = false;
        }
    }
}
