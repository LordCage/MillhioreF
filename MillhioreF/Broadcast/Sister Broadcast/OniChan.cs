using Discord;
using Discord.Commands;
using Discord.Modules;
//
//
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillhioreF
{
    class OniChan
    {
        DiscordClient supporter;
        CommandService service;

        public OniChan()
        {
            supporter = new DiscordClient(x =>
            {
                x.AppName = "Supporting";
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = lllya;
            });
            supporter.UsingCommands(x =>
            {
                x.PrefixChar = '@';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Private;
            });

            service = supporter.GetService<CommandService>();
            RegisterModeratorCommand();
            RegisterShutdownCommand();
            RegisterSeperatorCommand();

            //

            //
            supporter.ExecuteAndWait(async () =>
            {
                await supporter.Connect("MjU3MjQ5MTc1MjAzMjE3NDM5.Cy5HfA.fLNCuIi1WxQqQR7k4EYaVawM7tI", TokenType.Bot);
            });
        }

        private void RegisterModeratorCommand()
        {
            service.CreateCommand("kick")
            .Description("`Kicks member from server.` ```EX : @kick <@someone>```")
            .Parameter("user", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    ulong id;
                    User u = null;
                    string FindingTheUser = e.Args[0];
                    if (!string.IsNullOrWhiteSpace(FindingTheUser))
                    {
                        if (e.Message.MentionedUsers.Count() == 1)
                            u = e.Message.MentionedUsers.FirstOrDefault();
                        else if (e.Server.FindUsers(FindingTheUser).Any())
                            u = e.Server.FindUsers(FindingTheUser).FirstOrDefault();
                        else if (ulong.TryParse(FindingTheUser, out id))
                            u = e.Server.GetUser(id);
                    }
                    if (u == null)
                    {

                        await e.Channel.SendMessage($"**appears out of bush** *Sorry Master, i was unable to find* `{FindingTheUser}`");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("MillhioreF : Kick Command Usage Stats"); // 3 spaces.
                        Console.WriteLine("---");
                        Console.WriteLine("ID" + " " + e.Channel.Id);
                        Console.WriteLine("CHANNEL NAME" + " " + e.Channel.Name);
                        Console.WriteLine("NAME" + " " + e.User.Name);
                        Console.WriteLine("ACTIVITY" + " " + e.User.LastOnlineAt);
                        Console.WriteLine("NICK" + " " + e.User.NicknameMention);
                        Console.WriteLine("---");
                        return;

                    }
                    await e.Channel.SendMessage($"*cya* {u.Mention} :dango:");
                    await u.Kick();
                });
        }
        private void RegisterSeperatorCommand()
        {
            service.CreateCommand("Seperator")
            .Description("`Seperator Line. Do i say more?..` ```EX : @Seperator```")
            .Do(async (e) =>
            {
                await e.Channel.SendFile("Seperator/seperator.png");
            });
        } 
        private void RegisterShutdownCommand()
        {
            service.CreateCommand("eury")
               .Description("`Shuts down MillhioreF bot` ```EX : Currently no documentation for this command.```")
              .Do(async (e) =>
              {
                  Console.ForegroundColor = ConsoleColor.Yellow;
                  await e.Channel.Client.Disconnect();
              });
                
             
             
    
        }

        private void lllya(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
            
        }

    }
}
