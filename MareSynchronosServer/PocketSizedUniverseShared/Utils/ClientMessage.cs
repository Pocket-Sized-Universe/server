using PocketSizedUniverse.API.Data.Enum;

namespace PocketSizedUniverseShared.Utils;
public record ClientMessage(MessageSeverity Severity, string Message, string UID);
