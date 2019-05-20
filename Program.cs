using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace SpeachSit232
{
    class Program
    {
        public static async Task RecognizeSpeechAsync()
        {
            var config = SpeechConfig.FromSubscription("2836bad665af44e6b2dfb0bebb51e186", "eastus");
            //creates a Text to speech for replies
            SpeechSynthesizer synth = new SpeechSynthesizer(config);
            // Creates a speech recognizer.
            using (var recognizer = new SpeechRecognizer(config))
            {
                Console.WriteLine("Say something...");

                // Starts speech recognition, and returns after a single utterance is recognized. 
                var result = await recognizer.RecognizeOnceAsync();
                string ConvertedResult = Convert.ToString(result.Text);
                // Checks result.
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    Console.WriteLine($"We recognized: {result.Text}");
                    switch (ConvertedResult)
                    {
                        case "Hello.":
                            await synth.SpeakTextAsync("Hello to you to");
                            break;
                        case "How are you?":
                            await synth.SpeakTextAsync("Im good how about yourself?");
                            break;
                        default:
                            break;
                            }
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                }
            }
        }

        static void Main()
        {
            Console.WriteLine("Welcome to Dominic Voice Recongation");
            int option = 0;
            do
            {
                Console.WriteLine("Please pick an option");
                Console.WriteLine("Press 1 to talk. Enter 2 to Quit");
                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                     RecognizeSpeechAsync().Wait();
                        break;
                }

            }
            while (option != 2);
            Console.WriteLine("Thank you, Goodbye");
            System.Threading.Thread.Sleep(1000);
        }
    }
}
