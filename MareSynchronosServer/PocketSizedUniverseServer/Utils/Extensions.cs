using PocketSizedUniverse.API.Data;
using PocketSizedUniverse.API.Data.Enum;
using PocketSizedUniverse.API.Data.Extensions;
using PocketSizedUniverseShared.Models;
using PocketSizedUniverseServer.Hubs;
using static PocketSizedUniverseServer.Hubs.MareHub;

namespace PocketSizedUniverseServer.Utils;

public static class Extensions
{
    public static GroupData ToGroupData(this Group group)
    {
        return new GroupData(group.GID, group.Alias);
    }

    public static UserData ToUserData(this GroupPair pair)
    {
        return new UserData(pair.GroupUser.UID, pair.GroupUser.Alias);
    }

    public static UserData ToUserData(this User user)
    {
        return new UserData(user.UID, user.Alias);
    }

    public static IndividualPairStatus ToIndividualPairStatus(this MareHub.UserInfo userInfo)
    {
        if (userInfo.IndividuallyPaired) return IndividualPairStatus.Bidirectional;
        if (!userInfo.IndividuallyPaired && userInfo.GIDs.Contains(Constants.IndividualKeyword, StringComparer.Ordinal)) return IndividualPairStatus.OneSided;
        return IndividualPairStatus.None;
    }

    public static GroupPermissions ToEnum(this Group group)
    {
        var permissions = GroupPermissions.NoneSet;
        permissions.SetPreferDisableAnimations(group.PreferDisableAnimations);
        permissions.SetPreferDisableSounds(group.PreferDisableSounds);
        permissions.SetPreferDisableVFX(group.PreferDisableVFX);
        permissions.SetDisableInvites(!group.InvitesEnabled);
        return permissions;
    }

    public static GroupUserPreferredPermissions ToEnum(this GroupPairPreferredPermission groupPair)
    {
        var permissions = GroupUserPreferredPermissions.NoneSet;
        permissions.SetDisableAnimations(groupPair.DisableAnimations);
        permissions.SetDisableSounds(groupPair.DisableSounds);
        permissions.SetPaused(groupPair.IsPaused);
        permissions.SetDisableVFX(groupPair.DisableVFX);
        return permissions;
    }

    public static GroupPairUserInfo ToEnum(this GroupPair groupPair)
    {
        var groupUserInfo = GroupPairUserInfo.None;
        groupUserInfo.SetPinned(groupPair.IsPinned);
        groupUserInfo.SetModerator(groupPair.IsModerator);
        return groupUserInfo;
    }

    public static UserPermissions ToUserPermissions(this UserPermissionSet? permissions, bool setSticky = false)
    {
        if (permissions == null) return UserPermissions.NoneSet;

        UserPermissions perm = UserPermissions.NoneSet;
        perm.SetPaused(permissions.IsPaused);
        perm.SetDisableAnimations(permissions.DisableAnimations);
        perm.SetDisableSounds(permissions.DisableSounds);
        perm.SetDisableVFX(permissions.DisableVFX);
        if (setSticky)
            perm.SetSticky(permissions.Sticky);
        return perm;
    }
}
