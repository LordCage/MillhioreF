using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Broadcaster
//[ ] 
//U.D.S.
using Discord;
using Discord.Commands;
using Discord.Modules;
using Discord.Logging;


namespace MillhioreF
{
    class Broadcast
    {
        DiscordClient broadcaster;
        CommandService commands;

        public Broadcast()
        {
            broadcaster = new DiscordClient(x =>
            {
                x.AppName = "MillhioreF";
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });
            broadcaster.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
            });
            {
                commands = broadcaster.GetService<CommandService>();
                RegisterBroadcast();
                RegisterShutoffCommand();


                broadcaster.ExecuteAndWait(async () =>
                {
                    await broadcaster.Connect("MjU3MjM4ODg3ODU5MDkzNTA0.Cy3z5A.MCGCO2i8iS7H-W3eUv54hvP2cus", TokenType.Bot); // Millie
                });
            }
        }
        private void RegisterBroadcast()
        {
            commands.CreateCommand("say")
                .Description("Bot = Talk")
                .Alias("s")
                .Parameter("text", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    string text = e.Args[0];
                    var logChannel = e.Server.FindChannels("announcements").FirstOrDefault();

                    await e.Channel.SendMessage(text);
                    
                });
        }
        public void RegisterShutoffCommand()
        {
            commands.CreateCommand("leave")
                .Do(async (e) =>
                {
                    await e.Channel.Client.Disconnect();
                });
        }

           class Support
        {
            DiscordClient supporter;
        }


        
        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

