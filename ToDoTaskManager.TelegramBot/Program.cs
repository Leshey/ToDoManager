using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using ToDoTaskManager.TelegramBot.Integration;
using ToDoTaskManager.TelegramBot.Requests;
using ToDoTaskManager.TelegramBot.UI;

var client = new ToDoTaskManagerClient("https://localhost:7023");
var service = new ToDoTaskManagerService(client);

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json", optional: false)
    .Build();

var token = config.GetValue<string>("tgbot_token");
if (string.IsNullOrEmpty(token)) 
{
    throw new InvalidOperationException("Token is null");
}

var botClient = new TelegramBotClient(config.GetValue<string>("tgbot_token"));

using var cts = new CancellationTokenSource();

var options = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

botClient.StartReceiving(updateHandler: HandleUpdateAsync, pollingErrorHandler: HandlePollingErrorAsync, options, cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();
async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
{
    if (update.CallbackQuery != null)
    {
        Console.WriteLine($"Received some crucial callback query.");

        if (update.CallbackQuery.Data == "nodders")
        {
            await client.SendStickerAsync(
                    chatId: update.CallbackQuery.From.Id,
                    sticker: InputFile.FromFileId("CAACAgQAAxkBAAPQZPNNcygobqs0RTQHY_o6xi9vciYAAnMIAAKbSlBSWBd8zRLDrkswBA"),
                    cancellationToken: cancellationToken);
        }

        if (update.CallbackQuery.Data == "ugh")
        {
            await client.SendStickerAsync(
                chatId: update.CallbackQuery.From.Id,
                sticker: InputFile.FromFileId("CAACAgQAAxkBAAPNZPNNb0XbIeg6HXwK7_FbInEpfDQAAh4PAAL8LNlSC7dOQUbOBoswBA"),
                cancellationToken: cancellationToken);
        }

        return;
    }

    if (update.Message == null)
    {
        return;
    }

    var message = update.Message;
    var chatId = message.Chat.Id;

    var createCommand = "/Create";
    var deleteCommand = "/Delete";
    var closeCommand = "/Close";
    //var testKeyboard = "/TestKeyboard";

    if (message.Text.StartsWith(createCommand))
    {
        var name = new string(message.Text.Skip(createCommand.Length + 1).ToArray());

        var response = await service.CreateNewToDo(new CreateNewToDoRequest(name), cancellationToken);

        await client.SendTextMessageAsync(chatId, response.Id.ToString(), cancellationToken: cancellationToken);

        return;
    }

    if (message.Text.StartsWith(deleteCommand))
    {
        var id = new string(message.Text.Skip(deleteCommand.Length + 1).ToArray());

        var response = await service.RemoveById(new RemoveByIdRequest(Guid.Parse(id)), cancellationToken);

        await client.SendTextMessageAsync(chatId, text: "Task is no more. \U0001F525", cancellationToken: cancellationToken);

        return;
    }

    if (message.Text.StartsWith(closeCommand))
    {
        var id = new string(message.Text.Skip(closeCommand.Length + 1).ToArray());

        var response = await service.CloseById(new CloseByIdRequest(Guid.Parse(id)), cancellationToken);

        await client.SendTextMessageAsync(chatId, text: "Task is closed.", cancellationToken: cancellationToken);

        return;
    }

    //if (message.Text.StartsWith(testKeyboard))
    //{
    //    var keyboard = new MessageKeyboard();
    //    keyboard.AddButton(Buttons.CreateNewToDo);
    //    keyboard.AddButton(Buttons.DeleteToDo);
    //    keyboard.AddLine();
    //    keyboard.AddButton(Buttons.ReopenToDo);
    //    keyboard.AddButton(Buttons.Back);

    //    await client.SendTextMessageAsync(
    //         chatId: chatId,
    //         text: "Testing keyboard",
    //         replyMarkup: keyboard.RenderKeyboard(),
    //         cancellationToken: cancellationToken
    //         );

    //    return;
    //}

    if (update.Message.Text == "Do some stuff")
    {
        await DoSomeStuff(client, update, cancellationToken);
        return;
    }

    if (update.Message.Text == "Let's move!")
    {
        await client.SendStickerAsync(
            chatId: chatId,
            sticker: InputFile.FromFileId("CAACAgQAAxkBAAPQZPNNcygobqs0RTQHY_o6xi9vciYAAnMIAAKbSlBSWBd8zRLDrkswBA"),
            cancellationToken: cancellationToken);

        await client.SendTextMessageAsync(
            chatId: chatId,
            text: "Remove keyboard",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken
            );
        return;
    }

    if (update.Message.Text == "No way!")
    {
        await client.SendStickerAsync(
            chatId: chatId,
            sticker: InputFile.FromFileId("CAACAgQAAxkBAAPNZPNNb0XbIeg6HXwK7_FbInEpfDQAAh4PAAL8LNlSC7dOQUbOBoswBA"),
            cancellationToken: cancellationToken);

        await client.SendTextMessageAsync(
            chatId: chatId,
            text: "Remove keyboard",
            replyMarkup: new ReplyKeyboardRemove(),
            cancellationToken: cancellationToken
            );
        return;
    }

    if (update.Message.Text == "Destiny")
    {
        InlineKeyboardMarkup inlineKeyboard = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "nodders", callbackData: "nodders"),
            InlineKeyboardButton.WithCallbackData(text: "ugh", callbackData: "ugh"),
        }
    });
        await client.SendTextMessageAsync(
            chatId: chatId,
            text: "Choose your destiny!",
            replyMarkup: inlineKeyboard,
            cancellationToken: cancellationToken
            );

        return;
    }

    if (update.Message.Text != null)
    {
        Console.WriteLine($"Received a message: {message.Text}");
    }
    else
    {
        Console.WriteLine($"Received message with some media or sticker.");
    }

    if (update.Message.Text != null)
    {
        Message replyMessage = await client.SendTextMessageAsync(
            chatId: chatId,
            text: "This is text. I can send it back.",
            replyToMessageId: update.Message.MessageId,
            cancellationToken: cancellationToken);

        await client.SendTextMessageAsync(
            chatId: chatId,
            text: update.Message.Text,
            replyToMessageId: replyMessage.MessageId,
            cancellationToken: cancellationToken);
    }

    if (update.Message.Sticker != null)
    {
        Message replyMessage = await client.SendTextMessageAsync(
            chatId: chatId,
            text: $"This is sticker. I can send it back. id:{message.Sticker.FileId}",
            replyToMessageId: update.Message.MessageId,
            cancellationToken: cancellationToken);

        await client.SendStickerAsync(
            chatId: chatId,
            sticker: InputFile.FromFileId(update.Message.Sticker.FileId),
            replyToMessageId: replyMessage.MessageId,
            cancellationToken: cancellationToken);
    }

    if (update.Message.Photo != null)
    {
        Message replyMessage = await client.SendTextMessageAsync(
            chatId: chatId,
            text: "This is picture. I can send it back.",
            replyToMessageId: update.Message.MessageId,
            cancellationToken: cancellationToken);

        await client.SendPhotoAsync
            (
            chatId: chatId,
            photo: InputFile.FromFileId(update.Message.Photo[0].FileId),
            replyToMessageId: replyMessage.MessageId,
            cancellationToken: cancellationToken
            );
    }

    if (update.Message.Video != null)
    {
        Message replyMessage = await client.SendTextMessageAsync(
            chatId: chatId,
            text: "This is video. I can send it back.",
            replyToMessageId: update.Message.MessageId,
            cancellationToken: cancellationToken);

        await client.SendVideoAsync
            (
            chatId: chatId,
            video: InputFile.FromFileId(update.Message.Video.FileId),
            replyToMessageId: replyMessage.MessageId,
            cancellationToken: cancellationToken
            );
    }

    if (update.Message.VideoNote != null)
    {
        Message replyMessage = await client.SendTextMessageAsync(
            chatId: chatId,
            text: "This is videonote. I can send it back.",
            replyToMessageId: update.Message.MessageId,
            cancellationToken: cancellationToken);

        await client.SendVideoNoteAsync
            (
            chatId: chatId,
            videoNote: InputFile.FromFileId(update.Message.VideoNote.FileId),
            replyToMessageId: replyMessage.MessageId,
            cancellationToken: cancellationToken
            );
    }

    if (update.Message.Animation != null)
    {
        Message replyMessage = await client.SendTextMessageAsync(
            chatId: chatId,
            text: "This is gif. I can send it back.",
            replyToMessageId: update.Message.MessageId,
            cancellationToken: cancellationToken);

        await client.SendAnimationAsync
            (
            chatId: chatId,
            animation: InputFile.FromFileId(update.Message.Animation.FileId),
            replyToMessageId: replyMessage.MessageId,
            cancellationToken: cancellationToken
            );
    }

    if (update.Message.Document != null)
    {
        Message replyMessage = await client.SendTextMessageAsync(
            chatId: chatId,
            text: "This is some document or I'm not sure what it is. I can send it back, I hope.",
            replyToMessageId: update.Message.MessageId,
            cancellationToken: cancellationToken);

        await client.SendDocumentAsync
            (
            chatId: chatId,
            document: InputFile.FromFileId(update.Message.Document.FileId),
            replyToMessageId: replyMessage.MessageId,
            cancellationToken: cancellationToken
            );
    }

    if (update.Message.Poll != null)
    {
        Message replyMessage = await client.SendTextMessageAsync(
            chatId: chatId,
            text: "This is poll.",
            replyToMessageId: update.Message.MessageId,
            cancellationToken: cancellationToken);

        //var mostAnsweredOption = String.Empty;
        //var totalAnswers = 0;

        //if (update.Message.Poll.TotalVoterCount > 1) 
        //{
        //    await client.SendTextMessageAsync
        //        (
        //        chatId: chatId,
        //        text: "There are no answers :(",
        //        replyToMessageId: replyMessage.MessageId,
        //        cancellationToken: cancellationToken
        //        );
        //    return;
        //}

        //foreach (var option in update.Poll.Options) 
        //{
        //    if (option.VoterCount > totalAnswers) 
        //    {
        //        mostAnsweredOption = option.Text;
        //        totalAnswers = option.VoterCount;
        //    }
        //}

        //await client.SendTextMessageAsync
        //    (
        //    chatId: chatId,
        //    text: mostAnsweredOption,
        //    replyToMessageId: replyMessage.MessageId,
        //    cancellationToken: cancellationToken
        //    );
    }
}

async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    string? ErrorMessage;

    switch (exception)
    {
        case ApiRequestException apiRequestException:
            ErrorMessage = $"Telegram API Error:\n [{apiRequestException.ErrorCode}]\n{apiRequestException.Message}";
            break;
        case InvalidDataException invalidDataException:
            goto default;
        default:
            ErrorMessage = exception.ToString();
            break;
    }

    Console.WriteLine(ErrorMessage);
}

async Task DoSomeStuff(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
{
    await client.SendContactAsync(
        chatId: update.Message.Chat.Id,
        phoneNumber: "+79991112233",
        firstName: "Reimu",
        lastName: "Hakurei",
        disableNotification: true,
        cancellationToken: cancellationToken);

    await client.SendVenueAsync(
        chatId: update.Message.Chat.Id,
        latitude: 52.125830f,
        longitude: 23.710771f,
        title: "Place to live",
        address: "Kanava 2/4, 222 444, Brzesc",
        cancellationToken: cancellationToken);

    ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
    {
        new KeyboardButton[] { "Let's move!", "No way!"  },
        //new KeyboardButton[] { "No way!" }
    })
    {
        ResizeKeyboard = true
    };

    await client.SendTextMessageAsync(
        chatId: update.Message.Chat.Id,
        text: "Kanava is free to move there.",
        replyMarkup: replyKeyboardMarkup,
        cancellationToken: cancellationToken);


}