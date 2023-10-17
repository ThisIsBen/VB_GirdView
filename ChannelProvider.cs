using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace Inline_Runtime_System
{
    class ChannelProvider
    {
        //Try SingleProduceMultipleConsumers
        public async void TrySingleP_MultiC()
        {
            
            await SingleProduceMultipleConsumers();

        }

        static async Task SingleProduceMultipleConsumers()
        {
            var channel = Channel.CreateUnbounded<string>();

            // In this example, multiple consumers are needed to keep up with a fast producer

            var producer1 = new Producer(channel.Writer, 1, 1300);
            var consumer1 = new Consumer(channel.Reader, 1, 1500);
            var consumer2 = new Consumer(channel.Reader, 2, 1500);
            var consumer3 = new Consumer(channel.Reader, 3, 1500);

            Task consumerTask1 = consumer1.ConsumeData(); // begin consuming
            Task consumerTask2 = consumer2.ConsumeData(); // begin consuming
            Task consumerTask3 = consumer3.ConsumeData(); // begin consuming

            Task producerTask1 = producer1.BeginProducing();

            //Without this command, consumer's reading while loop will not end.
            await producerTask1.ContinueWith(_ => channel.Writer.Complete());

            await Task.WhenAll(consumerTask1, consumerTask2, consumerTask3);
        }



        internal class Consumer
        {
            private readonly ChannelReader<string> _reader;
            private readonly int _identifier;
            private readonly int _delay;

            public Consumer(ChannelReader<string> reader, int identifier, int delay)
            {
                _reader = reader;
                _identifier = identifier;
                _delay = delay;
            }

            public async Task ConsumeData()
            {
                Console.WriteLine($"CONSUMER ({_identifier}): Starting");

                while (await _reader.WaitToReadAsync())
                {
                    string timeString="";
                    if (_reader.TryRead(out  timeString))
                    {
                        await Task.Delay(_delay); // simulate processing time

                        Console.WriteLine($"CONSUMER ({_identifier}): Consuming {timeString}");
                    }
                }

                Console.WriteLine($"CONSUMER ({_identifier}): Completed");
            }
        }
        internal class Producer
        {
            private readonly ChannelWriter<string> _writer;
            private readonly int _identifier;
            private readonly int _delay;

            public Producer(ChannelWriter<string> writer, int identifier, int delay)
            {
                _writer = writer;
                _identifier = identifier;
                _delay = delay;
            }

            public async Task BeginProducing()
            {
                Console.WriteLine($"PRODUCER ({_identifier}): Starting");

                for (var i = 0; i < 10; i++)
                {
                    await Task.Delay(_delay); // simulate producer building/fetching some data

                    var msg = $"P{_identifier} - {DateTime.UtcNow:G}";

                    Console.WriteLine($"PRODUCER ({_identifier}): Creating {msg}");

                    await _writer.WriteAsync(msg);
                }

                Console.WriteLine($"PRODUCER ({_identifier}): Completed");
            }
        }

    }
}
