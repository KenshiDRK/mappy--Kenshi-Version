using System;

namespace EntityEnums
{
    [Flags]
    public enum EntityType
    {
        PC = 0x01,
        NPC = 0x02,
        GroupMember = 0x04, //YOU the player and your group members set both group and ally.
        AllianceMember = 0x08, //YOUR alliance members not in your group set only this. 
        MOB = 0x10,//Also includes event npcs that can be healed (ie besieged/assault/missions)
        Door = 0x20, //could be more than just doors...
        Self = 0x0D,
    }

    public enum FFXIModelType : byte
    {
        Player = 0,
        NPCRacial = 1,
        NPCUnique = 2,
        NPCObject = 3, //so far only doors...
        Boat = 5, //Airship and Ferries
        //                = 7  //i know this exists, but ive only seen it once.
    }

    [Flags]
    public enum RenderFlags1
    {
        Spawned = 0x00000200,
        Enemy = 0x00002000,
        Hidden = 0x00004000,
        Dead = 0x00400000,
    }

    [Flags]
    public enum RenderFlags2
    {
        Invisible = 0x00001000,
        Seeking = 0x00100000,
        Autogroup = 0x00200000,
        Away = 0x00400000,
        Anonymous = 0x00800000,
        Help = 0x01000000,
        TempLogged = 0x04000000,
        Linkshell = 0x08000000,
        ConnectionLost = 0x10000000,
    }

    [Flags]
    public enum RenderFlags3
    {
        Object = 0x00000008,
        InvisibleEffect = 0x00000080,
        Bazaar = 0x00000200,
        Promotion = 0x00000800, // 0x00000008 triggers this...
        Promotion2 = 0x00001000, // as does 0x00000010, individually, however...
        TempLogged2 = 0x00001800, // both together result in TempLogged, same as 0x00040000 in Field2
        GM = 0x00002000,
        Maintenance = 0x00004000,
    }

    [Flags]
    public enum RenderFlags4
    {
        NameDeletion = 0x00000080,
        Charmed = 0x00002000,
        Attackable = 0x10000000,
    }

    [Flags]
    public enum RenderFlags5
    {
        NameHidden = 0x00000080,
        Mentor = 0x00001000,
        NewPlayer = 0x00002000,
        TrialAccount = 0x00004000,
        VisibleDistance = 0x00008000,
        Transparent = 0x00040000,
        HPCloak = 0x00100000,
        LevelSync = 0x00800000,
    }

    public enum FFXICombatFlags : byte
    {
        InCombat = 1,  //100% confirmed: player pulls out weapon if set. WARNING: /heal /sit & some town NPCs sets this as well
        Dead = 2,  //100% confirmed: things get the death animation if set. WARNING: /sit sets this as well
        //              4     /sit sets this
        Chocobo = 5,
        //              8     /sit sets this. cant move if set.
        //              16    cant move if set
        //              32    /heal sets this. cant move if set.
        //              64    cant move if set
        Mount = 85,
        //              128   cant move if set
        // this *IS* a bitfield: if you one-shot something the corpse will be 2 instead of 3 (died out of combat)
    }
}