﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Mvc.Core;

namespace Telegram.Bot.Mvc.Framework
{
    public class BotListener  : IDisposable{

        private ILogger _logger;
        private IBotRouter _router;
        private BotSession _session;

        public User BotInfo { get; protected set; }
        public ITelegramBotClient Bot { get; protected set; }



        public BotListener(string token, IBotRouter router, ILogger logger) {
            _router = router;
            _logger = logger;
            Bot = new TelegramBotClient(token);
            BotInfo = Bot.GetMeAsync().Result;
            Bot.OnReceiveError += Bot_OnReceiveError;
            Bot.OnReceiveGeneralError += Bot_OnReceiveGeneralError;
            Bot.OnUpdate += _bot_OnUpdate;
        }

        public BotListener(string token, ILogger logger) 
            : this(token, new BotRouter(new BotControllerFactory()), logger)
        {

        }

        private async void _bot_OnUpdate(object sender, Bot.Args.UpdateEventArgs e) {
            var context = new BotContext(null, _session, e.Update);
            try {
                await _router.Route(context);
            }
            catch (Exception ex) {
                _logger.Log(ex, context.RouteData);
            }
        }

        private void Bot_OnReceiveGeneralError(object sender, Telegram.Bot.Args.ReceiveGeneralErrorEventArgs e) {
            _logger.Log(e.Exception);
        }

        private void Bot_OnReceiveError(object sender, Telegram.Bot.Args.ReceiveErrorEventArgs e) {
            _logger.Log(e.ApiRequestException);
        }

        public BotSession Start() {
            Bot.StartReceiving();
            if(_session == null) _session = new BotSession(Bot, _router);
            return _session;
        }

        public void Stop() {
            Bot.StopReceiving();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                Bot = null;
                _router = null;
                _session = null;
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
