using ITsimple_telegram_bot;

string token = File.ReadAllText("token.config");

TelegramBot bot = new TelegramBot(token);

void Updates(TelegramMessageModel msg)
{
    bot.SendMessage(msg.chatId, $"{msg.text}: получено");
}

bot.action = Updates;

bot.Start();

System.Console.WriteLine("++++");
// HttpClient hc = new();
// hc.BaseAddress = new Uri($"https://api.telegram.org/bot{token}/");

// int offset = 0;
// string rawData = hc.GetStringAsync($"getUpdates?offset={offset}").Result;

// JObject obj = JObject.Parse(rawData);
// JArray messages = JArray.Parse(obj["result"].ToString());

// for (int i = 0; i < messages.Count; i++)
// {
//     Console.Write($"{messages[i]["message"]["from"]["first_name"]} -> ");
//     Console.WriteLine($"{messages[i]["message"]["text"]}");
// }