using System;
using System.IO;
using Futbal.Mng.Infrastructure.Interfaces.EventBus;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Futbal.Mng.Infrastructure.EventBus
{
    public class DefaultRabbitMqPersistentConnection : IRabbitMqPersistentConnection
    {
        private readonly IConnectionFactory _factory;
        private IConnection _connection;
        private bool _disposed;

        private readonly object sync_root = new object();

        public DefaultRabbitMqPersistentConnection(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }


        public IModel CreateModel()
        {
            if(!IsConnected) 
            {
                throw new InvalidOperationException("Not connected to rabbitmq");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public bool TryConnect()
        {
            Console.WriteLine("RabbitMQ Client is trying to connect");

            lock (sync_root)
            {
                    _connection = _factory.CreateConnection();

                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;

                    Console.WriteLine($"RabbitMQ Client acquired a persistent connection to '{_connection.Endpoint.HostName}' and is subscribed to failure events");

                    return true;
                }
                else
                {
                    Console.WriteLine("FATAL ERROR: RabbitMQ connections could not be created and opened");

                    return false;
                }
            }
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            Console.WriteLine("A RabbitMQ connection is shutdown. Trying to re-connect...");

            TryConnect();
        }

        void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;

            Console.WriteLine("A RabbitMQ connection throw exception. Trying to re-connect...");

            TryConnect();
        }

        void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;

            Console.WriteLine("A RabbitMQ connection is on shutdown. Trying to re-connect...");

            TryConnect();
        }
    }
}