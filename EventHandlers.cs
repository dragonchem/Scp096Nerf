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
        public List<Player> Scp096Targets = new List<Player>();

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
            ev.Target.Broadcast(5, plugin.Config.TargetMessage);
            dDamageToBeDone += plugin.Config.DamagePerTarget;
            Scp096Targets.Add(ev.Target);
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Killer.Role == RoleType.Scp096)
            {
                Scp096Targets.Remove(ev.Target);
                dDamageToBeDone -= plugin.Config.DamagePerTarget;
            }
        }

        public void OnCalmingDown(CalmingDownEventArgs ev)
        {
            Scp096Targets.Clear();
            StopTimer();
            ev.Scp096.ShieldRechargeRate = plugin.Config.ShieldRechargeRate;
        }

        public void OnEnraging (EnragingEventArgs ev)
        {
            ev.Scp096.ShieldRechargeRate = plugin.Config.ShieldRechargeRateEnraged;
            StartTimer(ev);
        }

        private async void Timer(EnragingEventArgs ev)
        {
            iTime = plugin.Config.Tickrate;
            dDamageToBeDone = 0;
            while (bTimerEnabled)
            {
                if (Scp096Targets.Count < 1)
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
                        ev.Player.Health -= (dDamageToBeDone - ev.Player.AdrenalineHealth) / plugin.Config.RegularHPResistance;
                        ev.Player.AdrenalineHealth = 0;
                    }
                    dDamageToBeDone += plugin.Config.DamagePerSecondEnraged;
                    iTime = 0;
                }
                await Task.Delay(TimeSpan.FromMilliseconds(1));
                iTime++;
            }
        }

        private void StartTimer(EnragingEventArgs ev)
        {
            bTimerEnabled = true;
            Timer(ev);
        }

        private void StopTimer()
        {
            bTimerEnabled = false;
        }
    }
}
