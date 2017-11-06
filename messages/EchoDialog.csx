using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

// For more information about this template visit http://aka.ms/azurebots-csharp-basic
[Serializable]
public class EchoDialog : IDialog<object> {
    protected int count = 1;
    private Random _rand = new Random();
    public Task StartAsync(IDialogContext context) {
        try {
            context.Wait(MessageReceivedAsync);
        } catch (OperationCanceledException error) {
            return Task.FromCanceled(error.CancellationToken);
        } catch (Exception error) {
            return Task.FromException(error);
        }

        return Task.CompletedTask;
    }

    public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument) {
        var message = await argument;
        if (message.Text == "reset") {
            PromptDialog.Confirm(
                context,
                AfterResetAsync,
                "Are you sure you want to reset the count?",
                "Didn't get that!",
                promptStyle: PromptStyle.Auto);
        } else {
            await context.PostAsync($"Your {GetRelative()} is a {GetAdjective()} {GetAnimal()}");
            context.Wait(MessageReceivedAsync);
        }
    }
    private string[] _adjectives = new[] {
        "spavined",
        "cock-eyed",
        "flatulent",
        "horny",
        "callous",
        "lame",
        "spotted",
        "snoring",
        "big-eared",
        "hairy",
        "dog-faced",
        "lame-brained",
        "rabid",
        "insane",
        "moon-faced",
        "purple",
        "lustful",
        "gluttonous",
        "greedy",
        "blasphemous",
        "wild",
        "alcoholic",
        "overall-wearing",
        "hayseed",
        "sloppy",
        "lazy",
        "poxed",
        "debauched",
        "drunken",
        "layabout",
        "slack-jawed",
        "pus-filled",
        "rotten",
        "hirsute",
        "stinky",
        "odiferous",
        "broke-ass",
        "lying",
        "slobby",
        "knock-knee'd",
        "gunky",
        "funky",
        "goddamned",
        "fat",
        "sleepy",
        "grumpy",
        "psychotic",
        "useless",
        "wobbly",
        "demonic",
        "jug-eared",
        "vermin-infested"
    };
    private string GetAdjective() => _adjectives[_rand.Next(_adjectives.Length)];

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
    private string GetRelative() => _relatives[_rand.Next(_relatives.Length)];

    private string[] _animals = new[] {
        "hampster",
        "raccoon",
        "tapir",
        "dingo",
        "hog",
        "woodcock",
        "tarsir",
        "platypus",
        "caribou",
        "sloth",
        "gerbil",
        "rat",
        "beaver",
        "ferret",
        "monkey",
        "lemur",
        "goat",
        "sheep",
        "heifer",
        "donkey",
        "llama",
        "alpaca",
        "swallow",
        "camel",
        "titmouse",
        "buzzard",
        "turkey",
        "capon",
        "alligator",
        "mule",
        "buffalo",
        "foosa",
        "water buffalo",
        "yak",
        "moose",
        "pronghorn",
        "wildebeast",
        "warthog",
        "hippo",
        "elephant",
        "rhinocerous",
        "crocodile",
        "iguana",
        "lizard",
        "skink",
        "newt",
        "salamander",
        "cuttlefish",
        "crab",
        "shrimp",
        "flounder",
        "salmon",
        "tuna",
        "hake",
        "haddock",
        "cod",
        "squirrel",
        "chipmunk",
        "chimpanzee",
        "gorrilla",
        "snake",
        "cusk",
        "woodpecker",
        "aurochs",
        "zebra",
        "giraffe",
        "shrew",
        "roadrunner",
        "duck",
        "goose",
        "blue-footed boobie",
        "turtle",
        "snapping turtle"
    };
    private string GetAnimal() => _animals[_rand.Next(_animals.Length)];

    public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument) {
        var confirm = await argument;
        if (confirm) {
            this.count = 1;
            await context.PostAsync("Reset count.");
        } else {
            await context.PostAsync("Did not reset count.");
        }
        context.Wait(MessageReceivedAsync);
    }
}