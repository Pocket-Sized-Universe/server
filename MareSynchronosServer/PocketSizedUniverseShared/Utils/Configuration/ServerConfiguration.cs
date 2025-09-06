using System.Text;

namespace PocketSizedUniverseShared.Utils.Configuration;

public class ServerConfiguration : MareConfigurationBase
{
    public Uri CdnFullUrl { get; set; } = null;

    public Version ExpectedClientVersion { get; set; } = new Version(0, 0, 0);

    public int MaxExistingGroupsByUser { get; set; } = 3;

    public int MaxGroupUserCount { get; set; } = 100;

    public int MaxJoinedGroupsByUser { get; set; } = 6;

    public bool PurgeUnusedAccounts { get; set; } = false;

    public int PurgeUnusedAccountsPeriodInDays { get; set; } = 14;

    public int MaxCharaDataByUser { get; set; } = 10;

    public int MaxCharaDataByUserVanity { get; set; } = 50;
    public bool RunPermissionCleanupOnStartup { get; set; } = true;
    public int HubExecutionConcurrencyFilter { get; set; } = 50;

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine(base.ToString());
        sb.AppendLine($"{nameof(CdnFullUrl)} => {CdnFullUrl}");
        sb.AppendLine($"{nameof(RedisConnectionString)} => {RedisConnectionString}");
        sb.AppendLine($"{nameof(ExpectedClientVersion)} => {ExpectedClientVersion}");
        sb.AppendLine($"{nameof(MaxExistingGroupsByUser)} => {MaxExistingGroupsByUser}");
        sb.AppendLine($"{nameof(MaxJoinedGroupsByUser)} => {MaxJoinedGroupsByUser}");
        sb.AppendLine($"{nameof(MaxGroupUserCount)} => {MaxGroupUserCount}");
        sb.AppendLine($"{nameof(PurgeUnusedAccounts)} => {PurgeUnusedAccounts}");
        sb.AppendLine($"{nameof(PurgeUnusedAccountsPeriodInDays)} => {PurgeUnusedAccountsPeriodInDays}");
        sb.AppendLine($"{nameof(RunPermissionCleanupOnStartup)} => {RunPermissionCleanupOnStartup}");
        sb.AppendLine($"{nameof(HubExecutionConcurrencyFilter)} => {HubExecutionConcurrencyFilter}");
        return sb.ToString();
    }
}