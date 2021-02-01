# Scp096Nerf
**An EXILED plugin to make a customizable nerf for SCP-096 for use in smaller lobbies**

# Config
| Key | Value type | Default value |  Description |
| :-- | :-- | :--: | :--: |
| is_enabled | boolean | true | Whether the plugin should be enabled. |
| damage_per_target | float | 7.5 | The damage 098 will take per tick per target. |
| damage_per_second_enraged | float | 0.5 | The amount by which the damage taken per tick will increase (leave at 0 for no linear increase in damage taken). |
| regular_hp_resistance | float | 3 | The amount of resistance 098 has when it's taking non-AHP damage (for the default value: 3, 096 will only take 1/3 of the set damage in regular HP). |
| shield_recharge_rate_enraged | float | 0 | The amount of shield to be recharged per second while enraged. |
| shield_recharge_rate | float | 5 | The amount of shield to be recharged per second while not enraged. |
| send_target_message | boolean | true | Whether the target should get a message when targeted by 096. |
| target_message | string | <color=red>You've become a target for SCP-096!</color> | The message a player sees when it becomes a target for 096. |
| tickrate | int | 25 | The tickrate of 096 taking damage. |
