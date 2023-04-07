using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITsimple_telegram_bot
{
    public class TelegramBot
    {
        string token;
        public Action<TelegramMessageModel> action;
        HttpClient hc;
        Thread thread;

        public TelegramBot(string token)
        {
            this.token = token;
            hc = new HttpClient();
            hc.BaseAddress = new Uri($"https://api.telegram.org/bot{token}/");
            thread = new Thread(GetUpdates);
        }

        private void GetUpdates()
        {
            long offset = 0;
            while (true)
            {
                string content = hc.GetStringAsync($"getUpdates?offset={offset}").Result;
                try
                {
                    TelegramMessageModel[] ms = new JsonParser().GetMessage(content);
                    if (ms.Length != 0)
                    {
                        foreach (var item in ms)
                        {
                            Console.WriteLine(item);
                            action(item);
                        }
                        offset = ms[ms.Length - 1].updateId + 1;
                    }
                }
                catch
                {
                    // Console.WriteLine("Error");
                }
                Thread.Sleep(1000);
            }
        }

        public void SendMessage(long userId, string text)
        {
            string answer = hc.GetStringAsync($"sendMessage?chat_id={userId}&text={text}").Result;
            Console.WriteLine($"{answer}");
        }

        public void Start()
        {
            thread.Start();
        }
    }
}