using System.Text;
using Discord;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using PocketSizedUniverseShared.Data;

namespace PocketSizedUniverseServices.Discord;

public partial class MareWizardModule
{
    [ComponentInteraction("wizard-seeder")]
    public async Task ComponentSeeder()
    {
        if (!(await ValidateInteraction().ConfigureAwait(false))) return;

        _logger.LogInformation("{method}:{userId}", nameof(ComponentSeeder), Context.Interaction.User.Id);

        StringBuilder sb = new();
        var user = await Context.Guild.GetUserAsync(Context.User.Id).ConfigureAwait(false);
        using var mareDb = await GetDbContext().ConfigureAwait(false);

        bool isSeeder = await mareDb.LodeStoneAuth.AnyAsync(u => u.DiscordId == user.Id && u.User.IsSuperSeeder)
            .ConfigureAwait(false);
        if (!isSeeder)
        {
            sb.AppendLine("Become a super seeder today!");
            sb.Append(Environment.NewLine);
            sb.AppendLine(
                "By becoming a super seeder you'll help the network as a whole by contributing your computer as a file server whenever you're online");
        }
        else
        {
            sb.AppendLine("You are already a super seeder!");
            sb.AppendLine("Enjoy your free Vanity benefits!");
        }

        EmbedBuilder eb = new();
        eb.WithTitle("Super Seeder");
        eb.WithDescription(sb.ToString());
        eb.WithColor(Color.Blue);
        ComponentBuilder cb = new();
        AddHome(cb);
        var label = isSeeder ? "Disable Super Seeder Mode" : "Enable Super Seeder Mode";
        // Send the desired new state in the button custom ID (toggle current state)
        var buttonId = isSeeder ? "wizard-seeder-button:false" : "wizard-seeder-button:true";
        var emoji = isSeeder ? new Emoji("🌱") : new Emoji("⬆️");
        cb.WithButton(label, buttonId, ButtonStyle.Primary, emote: emoji);

        await ModifyInteraction(eb, cb).ConfigureAwait(false);
    }

    [ComponentInteraction("wizard-seeder-button:*")]
    private async Task SeederButton(bool isSeeder = false)
    {
        var user = await Context.Guild.GetUserAsync(Context.User.Id).ConfigureAwait(false);
        using var mareDb = await GetDbContext().ConfigureAwait(false);
        var userAuth = mareDb.LodeStoneAuth.Include(lodeStoneAuth => lodeStoneAuth.User).FirstOrDefault(a => a.DiscordId == user.Id);
        if (userAuth is { User: not null })
        {
            userAuth.User.IsSuperSeeder = isSeeder;
            await mareDb.SaveChangesAsync().ConfigureAwait(false);
        }
        if (isSeeder)
        {
            await _botServices.AddSeederRoleAsync(Context.Interaction.User).ConfigureAwait(false);
        }
        else
        {
            await _botServices.RemoveSeederRoleAsync(Context.Interaction.User).ConfigureAwait(false);
        }

        // Refresh the UI to reflect the new state by re-rendering the seeder component
        await ComponentSeeder().ConfigureAwait(false);
    }
}
