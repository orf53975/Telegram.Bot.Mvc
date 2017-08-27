﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Mvc.Core.Messages;
using Telegram.Bot.Mvc.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Mvc.Extensions
{
    public static class BotExtensions {

        public static Task ScheduleTextMessageAync(this ITelegramBotClient bot, ChatId chatId, string text, IReplyMarkup replyMarkup = null, ParseMode parseMode = ParseMode.Default, bool disableWebPagePreview = true, int replyToMessageId = 0) {
            return new Task(async () => {
                await bot.SendTextMessageAsync(
                    chatId,
                    text,
                    parseMode: parseMode,
                    disableWebPagePreview: disableWebPagePreview,
                    replyMarkup: replyMarkup, 
                    replyToMessageId: replyToMessageId, 
                    disableNotification: false);
            });
        }

        public static IMessage CreateTextMessage(
            this BotContext context, 
            ChatId chatId, 
            string text, 
            IReplyMarkup replyMarkup = null, 
            ParseMode parseMode = ParseMode.Default, 
            bool disableWebPagePreview = true, 
            int replyToMessageId = 0)
        {
            {
                return new TextMessage(context.BotSession.Token)
                {
                    ChatId = chatId,
                    Text = text,
                    ReplyMarkup = replyMarkup,
                    ParseMode = parseMode,
                    DisableWebPagePreview = disableWebPagePreview,
                    ReplyToMessageId = replyToMessageId,
                    DisableNotification = false
                };
            }
        }
    }
}
