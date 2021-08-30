using Grpc.Core;
using gRPCServer.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace gRPCServer.Services
{
    public class QuizService : Maths.MathsBase
    {
        private readonly ILogger<QuizService> _logger;
        public QuizService(ILogger<QuizService> logger)
        {
            _logger = logger;
        }

        public override async Task AskQuestion(QuestionRequest questions, IServerStreamWriter<AnswerReply> responseStream, ServerCallContext context)
        {
            foreach (var question in questions.Texts.ToList())
            {
                try
                {
                    if (!string.IsNullOrEmpty(question))
                    {
                        var dt = new DataTable();
                        var answer = Convert.ToDouble(dt.Compute(question, string.Empty));

                        await Task.Delay(800);

                        await responseStream.WriteAsync(new AnswerReply { Answer = answer, Question = question });
                    }
                }
                catch (Exception)
                {
                    await responseStream.WriteAsync(new AnswerReply { Answer = 0, Question = "It seems that something is wrong.!!!" });
                }
            }
        }

    }
}
