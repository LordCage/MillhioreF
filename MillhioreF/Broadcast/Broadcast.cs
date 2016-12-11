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
            private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }

        class Support
        {
            DiscordClient supporter;
            CommandService service;

            public Support()
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

                //

                //
                supporter.ExecuteAndWait(async () =>
                {
                    await supporter.Connect("", TokenType.Bot);
                });
            }

            private void RegisterModeratorCommand()
            {
                service.CreateCommand("kick")
                .Description("")
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
                          await e.Channel.SendMessage($"**appears out of bush** *Sorry Master, i was unable to find `{FindingTheUser}`");
                          return;
                      }
                      await e.Channel.SendMessage($"*cya {u.Mention} :lllyastare:");
                      await u.Kick();
                  });
            }

            private void lllya(object sender, LogMessageEventArgs e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }

