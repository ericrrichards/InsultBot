using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

// For more information about this template visit http://aka.ms/azurebots-csharp-basic
[Serializable]
public class EchoDialog : IDialog<object>
{
    protected int count = 1;
    private Random _rand = new Random();
    public Task StartAsync(IDialogContext context)
    {
        try
        {
            context.Wait(MessageReceivedAsync);
        }
        catch (OperationCanceledException error)
        {
            return Task.FromCanceled(error.CancellationToken);
        }
        catch (Exception error)
        {
            return Task.FromException(error);
        }

        return Task.CompletedTask;
    }

    public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
    {
        var message = await argument;
        if (message.Text == "reset")
        {
            PromptDialog.Confirm(
                context,
                AfterResetAsync,
                "Are you sure you want to reset the count?",
                "Didn't get that!",
                promptStyle: PromptStyle.Auto);
        }
        else
        {
            await context.PostAsync($"Your {GetRelative()} is a {GetAdjective()} {GetAnimal()}");
            context.Wait(MessageReceivedAsync);
        }
    }
    private string GetAdjective(){
        return "purple";
    }

    private static string[] _relatives = new[] {
        "mother",
        "father",
        "sister",
        "brother",
        "grandmother",
        "cousin",
        "grandfather",
        "uncle",
        "aunt",
        "wife",
        "son",
        "daughter",
        "niece",
        "nephew",
        "girlfriend",
        "wife",
        "husband",
        "boyfriend",
        "mother-in-law",
        "father-in-law",
        "brother-in-law",
        "sister-in-law",
        "stepson",
        "stepdaughter"
    };
    private string GetRelative(){
        return _relatives[_rand.Next(_relatives.Length)];
    }
    private string GetAnimal(){
        return "hampster";
    }

    public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
    {
        var confirm = await argument;
        if (confirm)
        {
            this.count = 1;
            await context.PostAsync("Reset count.");
        }
        else
        {
            await context.PostAsync("Did not reset count.");
        }
        context.Wait(MessageReceivedAsync);
    }
}