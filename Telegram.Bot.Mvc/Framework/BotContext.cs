﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.BotMVC.Core;

namespace Telegram.BotMVC.Framework {
    public class BotContext {

        public BotContext(
            BotRouteData routeData,
            BotSession session, Update update) {
            BotSession = session;
            Update = update;
            RouteData = routeData;
        }

        public ITelegramBotClient Bot => BotSession.Bot;

        public Update Update { get; protected set; }

        public BotSession BotSession { get; protected set; }

        public BotRouteData RouteData { get; internal set; }
    }
}