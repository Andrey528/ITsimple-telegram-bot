using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


namespace ITsimple_telegram_bot
{
    public class JsonParser
    {
        string content;

        public TelegramMessageModel[] GetMessage(string content)
        {
            List<TelegramMessageModel> msgs = new();

            JToken[] data = JObject.Parse(content)["result"].ToArray();

            foreach (dynamic item in data)
            {
                long id = item.message.from.id;
                string text = item.message.text;
                long updateId = item.update_id;
                string firstName = item.message.from.first_name;
                msgs.Add(new(id, firstName, text, updateId));
            }
            return msgs.ToArray();
        }
    }
}