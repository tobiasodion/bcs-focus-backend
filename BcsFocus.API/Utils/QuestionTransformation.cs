using BcsFocus.API.Models;

namespace BcsFocus.API.Utils
{
    public static class QuestionTransformation{
        public static List<Question> transform(List<Question> question)
        {

            List<Question> transformedQuestions = new List<Question>();

            foreach (var q in question)
            {
                if (q.QuestionPoints != null)
                {
                    foreach (var qp in q.QuestionPoints)
                    {
                        Question temp = new Question()
                        {
                            Id = q.Id,
                            QuestionDefinitions = q.QuestionDefinitions,
                            Figure = q.Figure,
                            Meta = q.Meta,
                            ModifyDate = q.ModifyDate,
                            NotaBene = q.NotaBene,
                            UploadDate = q.UploadDate,
                            Topics = q.Topics,
                            QuestionPoints = new List<QuestionPoint> { qp }
                    };
     
                        transformedQuestions.Add(temp);
                    }
                }
                else
                {
                    transformedQuestions.Add(q);
                }
            }
            return transformedQuestions;
        }
    }
}
