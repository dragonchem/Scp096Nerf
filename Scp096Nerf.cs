using Exiled.API.Features;
using Exiled.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers = Exiled.Events.Handlers;
using Player = Exiled.Events.Handlers.Player;
using Scp096 = Exiled.Events.Handlers.Scp096;

namespace Scp096Nerf
{
    public class Scp096Nerf : Plugin<Config>
    {
        public EventHandlers Handlers { get; private set; }
        public override string Name => nameof(Scp096Nerf);
        public override string Author => "Written by TheLazyKitten";
        public override Version Version { get; } = new Version(1, 0, 0);
        
        public Scp096Nerf() { }

        public override void OnEnabled()
        {
            Handlers = new EventHandlers(this);
            Scp096.AddingTarget += Handlers.OnAddingTarget;
            Scp096.CalmingDown += Handlers.OnCalmingDown;
            Scp096.Enraging += Handlers.OnEnraging;
            Player.Dying += Handlers.OnDying;
        }

        public override void OnDisabled()
        {
            Scp096.AddingTarget -= Handlers.OnAddingTarget;
            Scp096.Enraging -= Handlers.OnEnraging;
            Scp096.CalmingDown -= Handlers.OnCalmingDown;
            Player.Dying -= Handlers.OnDying;
            Handlers = null;
        }
    }
}
