namespace Wasit.Core.Entities.SettingTables
{
    public class QuestionsClient
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }
        public string QuestionEn { get; set; }
        public string AnswerEn { get; set; }
        public bool IsActive { get; set; }
        public string ChangeLangQuestion(string lang = "ar")
        {

            return AAITHelper.HelperMsg.creatMessage(lang, Question, QuestionEn);
        }
        public string ChangeLangAnswer(string lang = "ar")
        {

            return AAITHelper.HelperMsg.creatMessage(lang, Answer, AnswerEn);
        }
    }
}
